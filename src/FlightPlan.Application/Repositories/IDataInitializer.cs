using System.Threading.Tasks;

namespace FlightPlan.Application.Repositories
{
    public interface IDataInitializer
    {
        Task Initialize();
    }
}
