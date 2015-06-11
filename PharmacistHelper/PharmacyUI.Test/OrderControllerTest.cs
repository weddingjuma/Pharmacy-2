using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using ExternalServices;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pharmacy.Controllers;
using Pharmacy.Models;
using PrescriptionResourceInterface;

namespace PharmacyUI.Test
{
    [TestFixture]
    public class OrderControllerTest
    {
        [Test]
        public void PostCheckAvailabilityTest()
        {
            //// Arrange
            var controller = new OrdersController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            //var prescriptions  = new Mock<PrescriptionsDTO>();
            //prescriptions.SetupGet(p => p.Prescriptions).Returns(new List<PrescriptionDTO>()
            //{
            //    new PrescriptionDTO()
            //    {
            //        DoctorFiscalCode = "docfiscalcode",
            //        PatientFiscalCode = "patientFiscalcode",
            //        ExpireDate = DateTime.Now,
            //        PrescriptionId = Guid.NewGuid(),
            //        Visibility = true,
            //        Medicines = new Dictionary<string, int>(){{"Idrocortisone", 3}}
            //    }
            //});

            var prescriptions  = new Mock<PrescriptionsDTO>();
            prescriptions.SetupGet(p => p.Prescriptions).Returns(JsonConvert.SerializeObject(new List<PrescriptionDTO>()
            {
                new PrescriptionDTO()
                {
                    DoctorFiscalCode = "docfiscalcode",
                    PatientFiscalCode = "patientFiscalcode",
                    ExpireDate = DateTime.Now,
                    PrescriptionId = Guid.NewGuid(),
                    Visibility = true,
                    Medicines = new Dictionary<string, int>(){{"Idrocortisone", 3}}
                }
            }));

            //// Assert
            //var result = response.ExecuteAsync(new CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            //var JObjectResult = JsonConvert.DeserializeObject<JObject>(result);
            var response = controller.PostCheckAvailability(prescriptions.Object);
            // Assert
            var result = response.ExecuteAsync(new CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            var jObjectResult = JsonConvert.DeserializeObject<JObject>(result);
          
            JsonConvert.DeserializeObject<IDictionary<Guid, IDictionary<string, IList<MedicineDTOPharmacy>>>>(jObjectResult["returnValue"].ToString());
        }
    }
}
