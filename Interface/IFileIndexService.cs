using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDirectCMD.Interface
{
    public interface IFileIndexService
    {
        void BuildIndex();
        void Save();

        void Load();

        bool TryFindExact(string filename, out List<string> paths);
        bool TryFindPartial(string filename, out List<string> paths);

        int GetCount();
    }
}
