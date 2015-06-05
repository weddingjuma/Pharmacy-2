
using System.Linq;
using System.Web.Http;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    public class OrdersController : ApiController
    {
        public IHttpActionResult PostCheckAvailability(PrescriptionsDTO p)
        {
            foreach (var pres in p.Prescriptions)
            {
                var principles = pres.Medicines.Keys.ToList();
                //fare la richiesta al resource server passandogli principles 
                //recuperare risposta
                //deserializzarla
                //chiamare il metodo che controlla il magazzino 
                //costruire la risposta
            }
            //inviare l'insieme di risposte 
        }
    }
}
