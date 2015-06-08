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
            // Arrange
            var controller = new OrdersController();
            {
               // Request = new HttpRequestMessage(),
               // Configuration = new HttpConfiguration()
            };

            var prescriptions  = new Mock<PrescriptionsDTO>();
            prescriptions.SetupGet(p => p.Prescriptions).Returns(new List<PrescriptionDTO>()
            {
                new PrescriptionDTO()
                {
                    DoctorFiscalCode = "docfiscalcode",
                    PatientFiscalCode = "patientFiscalcode",
                    ExpireDate = DateTime.Now,
                    PrescriptioId = Guid.NewGuid(),
                    Visibility = true,
                    Medicines = new Dictionary<string, int>(){{"Idrocortisone", 3}}
                }
            });

            // Act
            var response = controller.PostCheckAvailability(prescriptions.Object);

            // Assert
            var result = response.ExecuteAsync(new CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            var JObjectResult = JsonConvert.DeserializeObject<JObject>(result);
            var result2 = JsonConvert.DeserializeObject<IDictionary<PrescriptionDTO, IDictionary<string, IList<MedicineDTOPharmacy>>>>(JObjectResult["returnValue"].ToString());
        }
    }
}
