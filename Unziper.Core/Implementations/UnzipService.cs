using Aspose.Zip;
using Aspose.Zip.Rar;
using Aspose.Zip.SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unziper.Core.Abstractions;
using Unziper.Domain.Models;

namespace Unziper.Core.Implementations
{

    public class UnzipService : IUnzipService
    {
        public Options Options { get; set; }

        public UnzipService(Options opt)
        {
            Options = opt;
        }
        public async Task<bool> Unzip(string filePath, string directory, string password)
        {
            if (filePath.Contains(".7z"))
                return await Un7zip(filePath, directory, password);
            else if (filePath.Contains(".rar"))
                return await UnRar(filePath, directory, password);
            else if (filePath.Contains(".zip"))
                return await UnZip(filePath, directory, password);
            return false;
        }

        private async Task<bool> Un7zip(string filePath, string directory, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using var archive = new SevenZipArchive(filePath, !string.IsNullOrWhiteSpace(password) ? password : null);
                    archive.ExtractToDirectory(directory);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private async Task<bool> UnZip(string filePath, string directory, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using var fileStream = new FileStream(filePath, FileMode.Open);
                    using var archive = new Archive(fileStream,
                        new ArchiveLoadOptions { DecryptionPassword = !string.IsNullOrWhiteSpace(password) ? password : null, Encoding=Encoding.GetEncoding(866)});
                    archive.ExtractToDirectory(directory);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        private async Task<bool> UnRar(string filePath, string directory, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using var archive = new RarArchive(filePath,
                        new RarArchiveLoadOptions { DecryptionPassword = !string.IsNullOrWhiteSpace(password) ? password : null } );
                    archive.ExtractToDirectory(directory);
                    return true;
                }
                catch
                {
                    return false;
                }
            });
        }

        public async Task<List<string>> UnzipAll(string[] filePaths, string directory, Action progressAction)
        {
            var outputDirectory = Directory.CreateDirectory(Path.Combine(directory, "output"));
            var notifiers = new List<string>();
            var tasks = new List<Task>();
            foreach(var file in filePaths)
            {
                var hasFileUnzip = false;
                foreach(var password in Options.Passwords)
                {
                    if(await Unzip(file, outputDirectory.FullName, password))
                    {
                        hasFileUnzip = true;
                        progressAction?.Invoke();
                        break;
                    }
                }
                if (!hasFileUnzip)
                    notifiers.Add(Path.GetFileNameWithoutExtension(file));
            }
            if (filePaths.Length == 0)
                notifiers.Add("Файлы не выбраны");
            return notifiers;
        }
    }
}
