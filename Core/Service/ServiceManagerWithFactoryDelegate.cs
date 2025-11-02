using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManagerWithFactoryDelegate(Func<IProductService> ProductFactory,
                                         Func<IBasketServices> BasketFactory,
                                         Func<IAuthenticationService> AuthenticationFactory,
                                         Func<IOrderService> OrderFactory) : IServiceManager
    {
        public IProductService ProductService => ProductFactory.Invoke();

        public IBasketServices BasketServices => BasketFactory.Invoke();

        public IAuthenticationService AuthenticationService => AuthenticationFactory.Invoke();

        public IOrderService OrderService => OrderFactory.Invoke();
    }
}
