using System;
using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pharmacy.Models;
using PharmacyServices;

namespace Pharmacy.Controllers
{
    public class TakeFromStocksController : ApiController
    {
        public IHttpActionResult PostCheckStocks(TakeFromStocksModels p)
        {
            var requiredMedicines = JsonConvert.DeserializeObject<IEnumerable<RequestForOrderDTO>>(p.Medicines);
            var pharmacy = PharmacyLogic.Pharmacy.GetInstance();

            var notavailable = new List<Guid>();
            foreach (var pres in requiredMedicines)
            {
                if (!pharmacy.WithdrawFromStocks(pres.Medicines))
                    notavailable.Add(pres.PrescriptionId);       
            }
            var serialized = JsonConvert.SerializeObject(notavailable);
            return Json(new JObject {{"returnValue", serialized}});
        }
    }
}
