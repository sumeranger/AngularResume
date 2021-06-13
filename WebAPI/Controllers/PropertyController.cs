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
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public PropertyController(IUnitOfWork uow, IMapper mapper)
        {
            this.mapper = mapper;
            this.uow = uow;
        }
        //property/list/1
        [AllowAnonymous]
        [HttpGet("list/{sellRent}")]
        public async Task<ActionResult> GetPropertyList(int sellRent)
        {
            var properties = await this.uow.PorpertyRepository.GetPropertiesAsync(sellRent);
            var propertiesDto = mapper.Map<IEnumerable<PropertyListDto>>(properties);
            return Ok(propertiesDto);
        }
        //property/detail/1
        [AllowAnonymous]
        [HttpGet("detail/{id}")]
        public async Task<ActionResult> GetPropertyDetail(int id)
        {
            var property = await this.uow.PorpertyRepository.GetPropertyDetailAsync(id);
            var propertyDto = mapper.Map<PropertyDetailDto>(property);
            return Ok(propertyDto);
        }
    }
}