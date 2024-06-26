using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Unziper.Core.Abstractions;
using Unziper.Domain.Models;

namespace Unziper.Core.Implementations
{
    public class OptionService : IOptionService
    {
        private readonly Options _options;
        private readonly string _folderPath;
        private readonly string _fileOptionName;
        public string FileOptionPath => Path.Combine(_folderPath, _fileOptionName);

        public bool HasLoadedPassword => _options.Passwords != null && _options.Passwords.Length > 0;

        public OptionService(Options options, string folderPath, string fileOptionName)
        {
            _options = options;
            _folderPath = folderPath;
            _fileOptionName = fileOptionName;
        }
        public void Create()
        {
            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);
            if (!File.Exists(FileOptionPath))
                File.Create(Path.Combine(FileOptionPath)).Close();
        }

        public async Task Load()
        {
            var optJson = await File.ReadAllTextAsync(FileOptionPath);
            var options = JsonConvert.DeserializeObject<Options>(optJson);
            if (options != null)
            {
                _options.Passwords = options.Passwords;
                _options.ExtractDirectory = options.ExtractDirectory;
            }
        }

        public async Task Save()
        {
            var optJson = JsonConvert.SerializeObject(_options);
            await File.WriteAllTextAsync(FileOptionPath, optJson);
        }

        public void SetPasswords(string[] passwords)
        {
            _options.Passwords = passwords;
        }
    }
}
