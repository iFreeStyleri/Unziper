using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unziper.Core.Abstractions
{
    public interface IAsyncUnzipService : IUnzipService
    {
        public Task<string> BruteUnzip(string filePath, string directory, Action progressAction = null);
    }
}
