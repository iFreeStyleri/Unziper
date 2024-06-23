using System.Threading.Tasks;

namespace Unziper.Core.Abstractions
{
    public interface IOptionService
    {
        public bool HasLoadedPassword { get; }
        public Task Load();
        public void Create();
        public Task Save();
        public void SetPasswords(string[] passwords);
    }
}
