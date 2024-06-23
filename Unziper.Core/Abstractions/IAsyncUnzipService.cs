using System;
using System.Threading.Tasks;

namespace Unziper.Core.Abstractions
{
    public interface IAsyncUnzipService : IUnzipService
    {
        public Task<string> BruteUnzip(string filePath, string directory, Action progressAction = null);
    }
}
