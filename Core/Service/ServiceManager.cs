using AutoMapper;
using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IBasketRepository _basketRepository):IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(_unitOfWork,_mapper));
        public IProductService ProductService => _LazyProductService.Value;

        private readonly Lazy<IBasketServices> _LazyBasketServices = new Lazy<IBasketServices>(() => 
                      new BasketService(_basketRepository, _mapper));

        public IBasketServices BasketServices => _LazyBasketServices.Value;
    }
}
