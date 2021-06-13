using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.interfaces;
using WebAPI.Models;

namespace WebAPI.Repo
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly DataContext dc;

        public PropertyRepository(DataContext dc)
        {
            this.dc = dc;
        }
        public void AddProperty(Property property)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteProperty(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(int sellRent)
        {
            var properties = await this.dc.Properties
            .Include(p => p.PropertyType)
            .Include(p => p.FurnishingType)
            .Include(p => p.City)
            .Where(p => p.SellRent == sellRent).ToListAsync();
            return properties;
        }

        public async Task<Property> GetPropertyDetailAsync(int id)
        {
            var property = await this.dc.Properties
            .Include(p => p.PropertyType)
            .Include(p => p.FurnishingType)
            .Include(p => p.City)
            .Where(p => p.Id == id).FirstAsync();
            return property;
        }
    }
}