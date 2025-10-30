using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class OrdersController(IServiceManager _serviceManager) : ApiBaseController
    {
        //[Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            var order =await _serviceManager.OrderService.CreateOrderAsync(orderDto,GetEmailByToken());
            return Ok(order);
        }

        [AllowAnonymous]
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {

            var orders = await _serviceManager.OrderService.GetALLOrdersAsync(GetEmailByToken());
            return Ok(orders);
        }
        //[Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
    }
}
