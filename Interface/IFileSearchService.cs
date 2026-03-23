using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDirectCMD.Interface
{
    public interface IFileSearchService
    {
        List<string>? Search(string filename);
        List<string>? AppSearch(string filename);

    }
}
