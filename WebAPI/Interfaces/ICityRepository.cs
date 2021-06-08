using System.Threading.Tasks;
using System.Collections.Generic;
using WebAPI.Models;

namespace WebAPI.interfaces
{   
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int cityId);
    }
}