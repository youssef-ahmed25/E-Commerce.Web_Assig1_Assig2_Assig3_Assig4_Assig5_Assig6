﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int id):NotFoundException( $"Delivery method with id {id} was not found.")
    {
    }
}
