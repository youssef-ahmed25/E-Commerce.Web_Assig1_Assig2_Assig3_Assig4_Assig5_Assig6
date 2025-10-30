using Shared.DataTransferObjects.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync( OrderDto orderDto, string email);

        Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync();

        Task<IEnumerable<OrderToReturnDto>> GetALLOrdersAsync(string email);
        Task <OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
