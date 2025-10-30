using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models;
using DomainLayer.Models.OrderModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModule;
using Shared.DataTransferObjects.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            var address = _mapper.Map<AddressDto,OrderAddress>(orderDto.Address);

            var basket = await _basketRepository.GetBasketAsync(orderDto.BasketId);
            if (basket is null)
            {
                throw new BasketNotFoundException(orderDto.BasketId);
            }
            List<OrderItem> orderItems = new List<OrderItem>();
            var productRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.Items)
            {
                var product =await productRepo.GetByIdAsync(item.Id);
                if (product is null)
                    throw new ProductNotFoundException(item.Id);
                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProducId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    },
                    Price = product.Price,
                    Quantity = item.Quantity

                };
                orderItems.Add(orderItem);
            }
            var deliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);
            if (deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);

            var order = new Order(email, address, deliveryMethod, orderItems, subTotal);

            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(deliveryMethod);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetALLOrdersAsync(string email)
        {
            var spec = new OrderSpecification(email);
            var orders =await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
            var order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(spec);
            return _mapper.Map<Order, OrderToReturnDto>(order);

        }
    }
}
