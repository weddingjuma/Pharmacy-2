
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using ExternalServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pharmacy.Models;
using PrescriptionResourceInterface;

namespace Pharmacy.Controllers
{
    public class OrdersController : ApiController
    {
        public IHttpActionResult PostCheckAvailability(PrescriptionsDTO p)
        {
            var pharmacy = PharmacyLogic.Pharmacy.GetInstance();
            IDictionary<PrescriptionDTO, IDictionary<string, IList<MedicineDTOPharmacy>>> prescriptionsDictionary =
                new Dictionary<PrescriptionDTO, IDictionary<string, IList<MedicineDTOPharmacy>>>();
            foreach (var pres in p.Prescriptions)
            {
                var principles = pres.Medicines.Keys.ToList();
                var principlesJObject = new JObject();
                var res = JsonConvert.SerializeObject(principles);
                principlesJObject.Add("principlesList", res);

                var result  = QueryResourceServer(principlesJObject).Result;

                var medicines = DeserializeResult<IDictionary<string, IList<MedicineDTO>>>(result);

                var medicineWithQuantity = BuildMedicinesWithQuantity(medicines, pres);
                var availableMedicines = pharmacy.GetMedicinesForPrescription(medicineWithQuantity);
                prescriptionsDictionary.Add(pres, availableMedicines);
            }
            var serialized = JsonConvert.SerializeObject(prescriptionsDictionary);
            return Json( new JObject {{"returnValue", serialized}});
        }

        private IDictionary<string, Tuple<IList<MedicineDTO>, int>> BuildMedicinesWithQuantity(IDictionary<string, IList<MedicineDTO>> medicines, PrescriptionDTO prescription)
        {
            var result = new Dictionary<string, Tuple<IList<MedicineDTO>, int>>(); 
            foreach (var principle in medicines.Keys)
            {
                var quantity = prescription.Medicines[principle];
                var tuple = new Tuple<IList<MedicineDTO>, int>(medicines[principle], quantity);
                result.Add(principle, tuple);
            }
            return result;
        }

        private async Task<JObject> QueryResourceServer(JObject principlesJObject)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4444");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("api/MedicinalByPrinciples", principlesJObject).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadAsAsync<JObject>().Result;
               
                throw new HttpResponseException(response);

            }
        }

        private T DeserializeResult<T>(JObject resultValue)
        {
            return JsonConvert.DeserializeObject<T>(resultValue["returnValue"].ToString());
        }
    }
}
