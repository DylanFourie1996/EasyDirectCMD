using System;
using System.Threading;
using System.Threading.Tasks;

namespace EasyDirectCMD.Services
{
    public sealed class LoadingAnimation : IDisposable
    {
        private readonly string _message;
        private readonly char[] _sequence = new[] { '|', '/', '-', '\\' };
        private readonly int _delay;
        private CancellationTokenSource? _cts;
        private Task? _task;
        private readonly object _sync = new();

        public LoadingAnimation(string message = "Loading", int delay = 100)
        {
            _message = message ?? string.Empty;
            _delay = Math.Max(10, delay);
        }

        public void Start()
        {
            lock (_sync)
            {
                if (_cts != null)
                    return; 

                _cts = new CancellationTokenSource();
                var token = _cts.Token;
                _task = Task.Run(async () =>
                {
                    int idx = 0;
                    // initial write so we can backspace reliably
                    Console.Write($"{_message} {_sequence[0]}");
                    while (!token.IsCancellationRequested)
                    {
                        await Task.Delay(_delay, token).ContinueWith(_ => { }, TaskContinuationOptions.OnlyOnRanToCompletion);
                        if (token.IsCancellationRequested) break;

                        idx = (idx + 1) % _sequence.Length;
                        // move back one char and write next spinner char
                        try
                        {
                            Console.Write('\b');
                            Console.Write(_sequence[idx]);
                        }
                        catch
                        {
                           
                        }
                    }
                }, token);
            }
        }

        public void Stop(string completionText = " Done")
        {
            lock (_sync)
            {
                if (_cts == null)
                    return; // not started

                try
                {
                    _cts.Cancel();
                    _task?.Wait();
                }
                catch (AggregateException) { /*  */ }
                catch (OperationCanceledException) { /*  */ }
                finally
                {
               
                    try
                    {
                        Console.Write('\b');
                        if (!string.IsNullOrEmpty(completionText))
                            Console.Write(completionText);
                        Console.WriteLine();
                    }
                    catch
                    {
                    }

                    _task?.Dispose();
                    _task = null;
                    _cts.Dispose();
                    _cts = null;
                }
            }
        }

        public void Dispose()
        {
            Stop();
        }

        //run synchronous work while showing spinner
        public static void Run(Action work, string message = "Loading", int delay = 100)
        {
            if (work == null) throw new ArgumentNullException(nameof(work));
            using var spinner = new LoadingAnimation(message, delay);
            spinner.Start();
            try
            {
                work();
            }
            finally
            {
                spinner.Stop();
            }
        }

        // run asynchronous work while showing spinner
        public static async Task RunAsync(Func<Task> workAsync, string message = "Loading", int delay = 100)
        {
            if (workAsync == null) throw new ArgumentNullException(nameof(workAsync));
            using var spinner = new LoadingAnimation(message, delay);
            spinner.Start();
            try
            {
                await workAsync().ConfigureAwait(false);
            }
            finally
            {
                spinner.Stop();
            }
        }
    }
}
