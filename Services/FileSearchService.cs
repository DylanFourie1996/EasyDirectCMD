using EasyDirectCMD.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDirectCMD.Services
{
    public class FileSearchService : IFileSearchService
    {
        private readonly IFileIndexService _indexService;

        public FileSearchService(IFileIndexService indexService)
        {
            _indexService = indexService;
        }

        public List<string>? AppSearch(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename)) return null;

            filename = filename.Trim().Trim('"');
            if (_indexService.TryFindExact(filename, out var exact))
                return exact;

            if (_indexService.TryFindPartial(filename, out var partial))
                return partial;

            var results = new List<string>();

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
                    var found = Directory.EnumerateFiles(drive, "*.exe", options)
                       .Where(f => string.Equals(Path.GetFileNameWithoutExtension(f), drive, StringComparison.OrdinalIgnoreCase));
           
                    results.AddRange(found);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Drive {drive} inaccessible: {ex.Message}");
                }
            }

            return results.Count > 0 ? results : null;
        }


        public List<string>? Search(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return null;

            filename = filename.Trim().Trim('"');

            if (_indexService.TryFindExact(filename, out var exact))
                return exact;

            if (_indexService.TryFindPartial(filename, out var partial))
                return partial;

            var results = new List<string>();

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
                    var found = Directory.EnumerateFiles(drive, "*.*", options)
                        .Where(f => Path.GetFileName(f)
                        .Contains(filename, StringComparison.OrdinalIgnoreCase));

                    results.AddRange(found);
                }
                catch
                {
                    Console.WriteLine("Drivers not found");
                }
            }

            return results.Count > 0 ? results : null;
        }
    }
}