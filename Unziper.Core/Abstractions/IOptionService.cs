using System.Threading.Tasks;

namespace Unziper.Core.Abstractions
{
    public interface IOptionService
    {
        public Task Load();
        public void Create();
        public Task Save();
        public void SetPasswords(string[] passwords);
    }
}
