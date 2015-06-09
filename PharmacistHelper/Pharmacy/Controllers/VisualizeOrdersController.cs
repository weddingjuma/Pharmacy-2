
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
        public ActionResult VisualizeOrders()
        {
            var orders = new OrderModel();
            orders.orders = new List<Order>()
            {
                new Order()
                {
                    ExpireDate = DateTime.Now,
                    PatientFiscalCode = "bananino"
                }
            };

            return View(orders);
        }


        public ActionResult VisualizeOrderDetails()
        {
            return View();
        }
    }
}