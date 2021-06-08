using System.Threading.Tasks;

namespace WebAPI.interfaces
{
    public interface IUnitOfWork
    {   
        ICityRepository CityRepository {get;}
        Task<bool> SaveAsync();
    }
}