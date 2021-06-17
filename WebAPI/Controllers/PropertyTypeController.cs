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
    public class PropertyTypeController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public PropertyTypeController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }
        //propertyType/list
        [AllowAnonymous]
        [HttpGet("list/")]
        public async Task<ActionResult> GetPropertyTypes()
        {
            var propertyTypes = await this.uow.PorpertyTypeRepository.GetPropertyTypesAsync();
            var propertyTypesDto = mapper.Map<IEnumerable<KeyValuePairDto>>(propertyTypes);
            return Ok(propertyTypesDto);
        }
    }
}