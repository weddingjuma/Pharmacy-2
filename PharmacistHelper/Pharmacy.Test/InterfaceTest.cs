using System;
using System.Collections.Generic;
using NUnit.Framework;
using PharmacyLogic;
using PrescriptionResourceInterface;

namespace Pharmacy.Test
{
    [TestFixture]
    public class InterfaceTest
    {
        private IPharmacy _pharmacy; 
        [SetUp]
        public void SetUp()
        {
            _pharmacy = PharmacyLogic.Pharmacy.GetInstance();
        }

        [Test]
        public void TestAvailableMedicines()
        {
            var prescriptions = new List<PrescriptionDTO>()
            {
                new PrescriptionDTO()
                {
                    DoctorFiscalCode = "tzzvnacjdiartj32",
                    PatientFiscalCode = "mrfhfoauhfhdghh",
                    ExpireDate = DateTime.Now,
                    Visibility = true
                }
            };
        }
    }
}
