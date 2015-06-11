using NUnit.Framework;

namespace PharmacyUI.Test
{
    [TestFixture]
    public class OrderControllerTest
    {
        [Test]
        public void PostCheckAvailabilityTest()
        {
            //// Arrange
            //var controller = new OrdersController()
            //{
            //    Request = new HttpRequestMessage(),
            //    Configuration = new HttpConfiguration()
            //};

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

            //// Act
            //var response = controller.PostCheckAvailability(prescriptions.Object);

            //// Assert
            //var result = response.ExecuteAsync(new CancellationToken()).Result.Content.ReadAsStringAsync().Result;
            //var JObjectResult = JsonConvert.DeserializeObject<JObject>(result);
          
            //var result2 = JsonConvert.DeserializeObject<IDictionary<Guid, IDictionary<string, IList<MedicineDTOPharmacy>>>>(JObjectResult["returnValue"].ToString());
        
        }
    }
}
