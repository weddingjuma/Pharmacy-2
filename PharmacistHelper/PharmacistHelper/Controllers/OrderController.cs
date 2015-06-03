
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PharmacistHelper.Models;

namespace PharmacistHelper.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult VisualizeOrders()
        {
            var orders = new OrderModel
            {
                orders = new List<Order>
                {
                    new Order()
                    {
                        PatientFiscalCode = "TZZVNC5393fhahf",
                        ExpireDate = DateTime.Now,
                        Medicines = new List<Tuple<string, int>>()
                        {
                            new Tuple<string, int>("Moment", 5),
                            new Tuple<string, int>("Benagol",2)
                        }
                    },
                     new Order()
                    {
                        PatientFiscalCode = "CPRDRLfdhsjfhadfhd",
                        ExpireDate = DateTime.Now,
                        Medicines = new List<Tuple<string, int>>()
                        {
                            new Tuple<string, int>("pillolaanticoncezionale", 2),
                            new Tuple<string, int>("froben",2)
                        }
                    },
                }
            };

            return View(orders);
        }

        public ActionResult VisualizeOrderDetails()
        {
            return View(ViewBag.OrderToPass);
        }
    }
}