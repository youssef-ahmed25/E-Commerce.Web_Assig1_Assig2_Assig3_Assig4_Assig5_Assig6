using DomainLayer.Models.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    internal class OrderSpecification : BaseSpecification<Order, Guid>
    {
        public OrderSpecification(string email) : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderSpecification(Guid id) : base(o => o.Id == id) 
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.Items);
        }
    }
}
