
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Pharmacy.Models;


namespace Pharmacy.Controllers
{
    [Authorize]
    public class VisualizeOrdersController: Controller
    {
        // GET: Order
        public ActionResult VisualizeApprovedOrders()
        {

            return View();
        }


        public ActionResult VisualizePreparedOrders()
        {
            return View();
        }

        public ActionResult VisualizeWithdrewOrders()
        {
            return View();
        }


        public ActionResult VisualizeOrderDetails(Guid id)
        {
          //  var model=new OrderModel(){Orders = }
            return View();
        }
    }
}