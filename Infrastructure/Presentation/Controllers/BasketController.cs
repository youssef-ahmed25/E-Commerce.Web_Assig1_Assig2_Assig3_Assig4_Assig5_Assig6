using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController (IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string key)
        {
            var basket = await _serviceManager.BasketServices.GetBasketAsync(key);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket( BasketDto basket)
        {
            var Basket = await _serviceManager.BasketServices.CreateOrUpdateBasketAsync(basket);
            return Ok(Basket);
        }
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string key)
        {
            var result = await _serviceManager.BasketServices.DeleteBasketAsync(key);
            return Ok(result);
        }
    }
}
