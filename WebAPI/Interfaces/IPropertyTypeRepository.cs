using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.interfaces
{
    public interface IPropertyTypeRepository
    {
        Task<IEnumerable<PropertyType>> GetPropertyTypesAsync();
    }
}