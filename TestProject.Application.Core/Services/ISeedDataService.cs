using System.Threading.Tasks;

namespace TestProject.Application.Core.Services
{
    public interface ISeedDataService
    {
        Task Initialize();
        Task Clear();
    }
}