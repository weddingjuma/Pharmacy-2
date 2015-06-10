using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ExternalServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pharmacy.Models;
using PrescriptionResourceInterface;


namespace Pharmacy.Controllers
{
    public class TakeFromStocksController : ApiController
    {
        public IHttpActionResult PostCheckStocks(TakeFromStocksModel p)
        {
            var requiredMedicines = JsonConvert.DeserializeObject<IDictionary<Guid, IList<Tuple<string, int>>>>(p.Medicines);
            var pharmacy = PharmacyLogic.Pharmacy.GetInstance();
            
            foreach (var pres in requiredMedicines)
            {
                pharmacy.
            }
            var serialized = JsonConvert.SerializeObject(prescriptionsDictionary);
            return Json(new JObject { { "returnValue", serialized } });
        }
    }
}
