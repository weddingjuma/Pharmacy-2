using System;
using System.Collections.Generic;
using System.Linq;

namespace PharmacieLogic
{
    public class OrderDto
    {
        public string PatientFiscalCode { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public IReadOnlyDictionary<string, int> Medicines { get; private set;  }

        public OrderDto(string patientFc, DateTime expire, IDictionary<string, int> medicines )
        {
            if (patientFc == null || expire == null || medicines == null)
                throw new ArgumentNullException();

            if(patientFc == "" || expire < new DateTime(2015, 1, 1) || !medicines.Any())
                throw new ArgumentException();

            PatientFiscalCode = patientFc;
            ExpireDate = expire;
            Medicines = new Dictionary<string, int>(medicines);
        }
    }
}
