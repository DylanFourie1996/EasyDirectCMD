using EasyDirectCMD.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDirectCMD.Services
{
    public class FileIndexService : IFileIndexService
    {
        private readonly Dictionary<string, List<string>> _fileIndex =
            new(StringComparer.OrdinalIgnoreCase);

        private readonly string _indexPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EasyDirectCMD_Location_Index.txt");

        public void BuildIndex()
        {
            _fileIndex.Clear();

            var drives = Directory.GetLogicalDrives();
            var options = new EnumerationOptions
            {
                RecurseSubdirectories = true,
                IgnoreInaccessible = true
            };

            foreach (var drive in drives)
            {
                try
                {
                    foreach (var file in Directory.EnumerateFiles(drive, "*.*", options))
                    {
                        string name = Path.GetFileName(file);

                        if (!_fileIndex.ContainsKey(name))
                            _fileIndex[name] = new List<string>();

                        _fileIndex[name].Add(file);
                    }
                }
                catch
                {
                    Console.WriteLine("Can't get to the drivers");
                }
            }
        }

        public void Save()
        {
            using var writer = new StreamWriter(_indexPath);

            foreach (var entry in _fileIndex)
            {
                foreach (var path in entry.Value)
                {
                    writer.WriteLine($"{entry.Key}|{path}");
                }
            }
        }

        public void Load()
        {
            if (!File.Exists(_indexPath))
                return;

            _fileIndex.Clear();

            foreach (var line in File.ReadAllLines(_indexPath))
            {
                var parts = line.Split('|');
                if (parts.Length != 2)
                    continue;

                if (!_fileIndex.ContainsKey(parts[0]))
                    _fileIndex[parts[0]] = new List<string>();

                _fileIndex[parts[0]].Add(parts[1]);
            }
        }

        public bool TryFindExact(string filename, out List<string> paths)
        {
            return _fileIndex.TryGetValue(filename, out paths!);
        }

        public bool TryFindPartial(string filename, out List<string> paths)
        {
            foreach (var entry in _fileIndex)
            {
                if (entry.Key.Contains(filename, StringComparison.OrdinalIgnoreCase))
                {
                    paths = entry.Value;
                    return true;
                }
            }

            paths = null!;
            return false;
        }

        public int GetCount() => _fileIndex.Count;
    }
}
