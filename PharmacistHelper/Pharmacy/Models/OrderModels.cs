﻿using System;
using System.Collections.Generic;
using PrescriptionResourceInterface;

namespace Pharmacy.Models
{

    public class OrderModel
    {
        public IEnumerable<OrderDto> Orders { get; set; }

    }
}