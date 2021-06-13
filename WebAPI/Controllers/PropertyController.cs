using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.interfaces;

namespace WebAPI.Controllers
{
    public class PropertyController : BaseController
    {
        private readonly IUnitOfWork uow;

        public PropertyController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        //property/type/1
        [AllowAnonymous]
        [HttpGet("type/{sellRent}")]
        public async Task<ActionResult> GetPropertyList(int sellRent)
        {
            var properties = await this.uow.PorpertyRepository.GetPropertiesAsync(sellRent);
            return Ok(properties);
        }
    }
}