
using System.Web.Mvc;
using Pharmacy.Models;


namespace Pharmacy.Controllers
{
    [Authorize]
    public class VisualizeOrdersController: Controller
    {
        // GET: Order
        public ActionResult VisualizeOrders()
        {
            var orders = new OrderModel();

            return View(orders);
        }


        public ActionResult VisualizeOrderDetails()
        {
            return View();
        }
    }
}