﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    //public class BaseEntity<Tkey>
    //{
    //    public TKey Id { get; set; }
    //}
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
