using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unziper.Core.Abstractions
{
    public interface IUnzipService
    {
        public Task<bool> Unzip(string filePath, string directory, string password);
        public Task<List<string>> UnzipAll(string[] filePaths, string directory, Action progressAction = null);
    }
}
