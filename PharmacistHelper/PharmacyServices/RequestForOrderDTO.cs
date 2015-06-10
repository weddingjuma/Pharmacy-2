using System;
using System.Collections.Generic;

namespace PharmacyServices
{
    public class RequestForOrderDTO
    {
        public class OrderedMedicine
        {
            public string MedicineName { get; set; }
            public bool KnownIfIsNotAvailable { get; set; }
            public int Quantity { get; set; } 
        }
        public Guid PrescriptionId { get; set; }
        public IList<OrderedMedicine> Medicines { get; set; }

        
    }
}
