using EasyDirectCMD.Interface;
using System.Diagnostics;

namespace EasyDirectCMD.Services
{
    public class FileOpener : IFileOpener
    {
        public void Open(string path)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            });
        }
    }
}