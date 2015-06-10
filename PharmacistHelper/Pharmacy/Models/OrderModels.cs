using System;
using System.Collections.Generic;
using PrescriptionResourceInterface;

namespace Pharmacy.Models
{

    public class OrderModel
    {
        public IEnumerable<OrderDto> orders { get; set; }
        public OrderDto SelectedOrder { get; set; }


    }
}