using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Pharmacy.Controllers;
using Pharmacy.Models;
using PharmacyServices;

namespace PharmacyUI.Test
{
    [TestFixture]
    class TakeFromStocksTest
    {
        [Test]
        public void FromStocksTest()
        {
            // Arrange
            var controller = new TakeFromStocksController()
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            var prescriptionGuid = Guid.NewGuid();
            
            var orderFunzionante = new RequestForOrderDTO()
            {
                PrescriptionId =Guid.NewGuid(),
                Medicines = new List<RequestForOrderDTO.OrderedMedicine>()
                {
                    new RequestForOrderDTO.OrderedMedicine()
                    {
                        MedicineName = "Cortison Chemicetina",
                        KnownIfIsNotAvailable = false,
                        Quantity = 4
                    }
                }
            };

            var orderNonFunzionante = new RequestForOrderDTO()
            {
                PrescriptionId = prescriptionGuid,
                Medicines = new List<RequestForOrderDTO.OrderedMedicine>()
                {
                    new RequestForOrderDTO.OrderedMedicine()
                    {
                        MedicineName = "Proctidol",
                        KnownIfIsNotAvailable = false,
                        Quantity = 3
                    }
                }
            };

            var orderConsapevole = new RequestForOrderDTO()
            {
                PrescriptionId = Guid.NewGuid(),
                Medicines = new List<RequestForOrderDTO.OrderedMedicine>()
                {   new RequestForOrderDTO.OrderedMedicine()
                    {
                        MedicineName = "Cortinal Areosol",
                        KnownIfIsNotAvailable = true,
                        Quantity = 4
                    }
                }
            };

            var enumerable = new List<RequestForOrderDTO>() {orderFunzionante, orderConsapevole, orderNonFunzionante};
            var model  = new Mock<TakeFromStocksModels>();
            model.SetupGet(m => m.Medicines).Returns(JsonConvert.SerializeObject(enumerable));

            // Act
            var response = controller.PostCheckStocks(model.Object);

            // Assert
            var result = response.ExecuteAsync(new CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            var jObjectResult = JsonConvert.DeserializeObject<JObject>(result);
          
            var listGuids = JsonConvert.DeserializeObject<IList<Guid>>(jObjectResult["returnValue"].ToString());
            Assert.That( listGuids.Count == 1);
            Assert.That(listGuids[0] == prescriptionGuid);
        }
    }
}
