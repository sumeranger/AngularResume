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
    public class FurnishingTypeRepository : IFurnishingTypeRepository
    {
        private readonly DataContext dc;
        public FurnishingTypeRepository(DataContext dc)
        {
            this.dc = dc;

        }
        public async Task<IEnumerable<FurnishingType>> GetFurnishingTypesAsync()
        {
            return await dc.FurnishingTypes.ToListAsync();
        }
    }
}