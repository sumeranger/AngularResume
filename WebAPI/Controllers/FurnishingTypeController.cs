using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebAPI.Dtos;
using WebAPI.interfaces;

namespace WebAPI.Controllers
{
    public class FurnishingTypeController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public FurnishingTypeController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }
        //furnishingType/list
        [AllowAnonymous]
        [HttpGet("list/")]
        public async Task<ActionResult> GetFurnishingTypes()
        {
            var furnishingTypes = await this.uow.FurnishingTypeRepository.GetFurnishingTypesAsync();
            var furnishingTypesDto = mapper.Map<IEnumerable<KeyValuePairDto>>(furnishingTypes);
            return Ok(furnishingTypesDto);
        }
    }
}