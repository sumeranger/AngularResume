using System.Threading.Tasks;

namespace WebAPI.interfaces
{
    public interface IUnitOfWork
    {   
        ICityRepository CityRepository {get;}
        IUserRepository UserRepository {get;}
        Task<bool> SaveAsync();
    }
}