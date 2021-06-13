using System.Threading.Tasks;
using WebAPI.interfaces;
using WebAPI.Data.Repo;
using WebAPI.Repo;

namespace WebAPI.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public UnitOfWork(DataContext dc)
        {
            this.dc = dc;
        }
        public ICityRepository CityRepository => new CityRepository(dc);
        public IUserRepository UserRepository => new UserRepository(dc);
        public IPropertyRepository PorpertyRepository => new PropertyRepository(dc);
        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() > 0;
        }
    }
}