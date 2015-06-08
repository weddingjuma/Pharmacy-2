using System;
using System.Collections.Generic;

namespace Pharmacy.Models
{
    public class Order
    {
        public string PatientFiscalCode { get; set; }
        public IList<Tuple<string, int>> Medicines { get; set; }
        public DateTime ExpireDate { get; set; }
    }

    public class OrderModel
    {
        public IList<Order> orders { get; set; }
    }
}