
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pharmacy.Models;
using PrescriptionResourceInterface;


namespace Pharmacy.Controllers
{
    [Authorize]
    public class VisualizeOrdersController : Controller
    {


        public ActionResult Oauth(string name)
        {
            var getData = "https://dione.disi.unige.it/oauth/v2/oauth/authorize?response_type=code";
            getData += "&client_id=SSD_ClientApp_prova";
            getData += "&redirect_uri=http://localhost:8000/AuthorizationManagement";

            getData += "&scope=GetMyAcceptedOrders+";
            getData += "GetMyPreparedOrders+";
            getData += "GetMyWithdrewOrders";
            getData += "&state=" + name;

            Response.BufferOutput = true;
            Response.Redirect(getData);

            return null;
        }

        public ActionResult VisualizeAcceptedOrders()
        {
            var task = ConfigurationOauthOperation("GetMyAcceptedOrders");
            try
            {
                var result = task.Result;
                var orders = DeserializeResult<IEnumerable<OrderDto>>(result);
                var model = new OrderModel(){Orders = orders};

                return View(model);
            }
            catch (AggregateException e)
            {
                if (e.GetBaseException().GetType() == typeof(InvalidOperationException))
                    return RedirectToAction("Oauth", "VisualizeOrders", new { name = "VisualizeAcceptedOrders" });
                throw;
            }

        }

        public ActionResult VisualizePreparedOrders()
        {
            var task = ConfigurationOauthOperation("GetMyAcceptedOrders");
            try
            {
                var result = task.Result;
                var orders = DeserializeResult<IEnumerable<OrderDto>>(result);
                var model = new OrderModel() { Orders = orders };

                return View(model);
            }
            catch (AggregateException e)
            {
                if (e.GetBaseException().GetType() == typeof(InvalidOperationException))
                    return RedirectToAction("Oauth", "VisualizeOrders", new { name = "VisualizePreparedOrders" });
                throw;
            }
        }

        public ActionResult VisualizeWithdrewOrders()
        {
            var task = ConfigurationOauthOperation("GetMyAcceptedOrders");
            try
            {
                var result = task.Result;
                var orders = DeserializeResult<IEnumerable<OrderDto>>(result);
                var model = new OrderModel() { Orders = orders };
                TempData["OrderList"] = orders; 
                return View(model);
            }
            catch (AggregateException e)
            {
                if (e.GetBaseException().GetType() == typeof(InvalidOperationException))
                    return RedirectToAction("Oauth", "VisualizeOrders", new { name = "VisualizeWithdrewOrders" });
                throw;
            }
        }


        public ActionResult VisualizeOrderDetails(Guid? id)
        {

            var listOrder = TempData["OrderList"] as IEnumerable<OrderDto>;

            if (id == null || listOrder == null)
                return RedirectToAction("Index", "Home");

            var selectedOrder = listOrder.Single(p => p.Id == id);

            return View(new OrderDetailsModel(){SelectedOrder = selectedOrder});
        }

        private async Task<JObject> ConfigurationOauthOperation(string scope)
        {
            var val = AuthorizationManagementController.ReturnVal(User.Identity.GetUserName(), scope);
            string parameters = "";
            var tokenToSend = new JObject { { "token", val.GetToken() }, { "scope", scope }, { "parameters", parameters } };
            return await SendJsonToWebApi(val.GetUri(), tokenToSend);
        }

        private async Task<JObject> SendJsonToWebApi(string uri, JObject token)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync("", token).Result;
                JObject returnValue = null;
                string errorMessage = null;
                if (response.IsSuccessStatusCode)
                    returnValue = response.Content.ReadAsAsync<JObject>().Result;
                else
                {
                    errorMessage = response.Content.ReadAsAsync<string>().Result;
                    if (errorMessage != null && errorMessage.Equals("Token expired"))
                    {
                        ViewBag.ExpiredToken = 1;
                        ViewBag.Message = " La tua autenticazione Oauth è scaduta, ";
                    }
                }
                return returnValue;
            }
        }

        private T DeserializeResult<T>(JObject resultValue)
        {
            return JsonConvert.DeserializeObject<T>(resultValue["returnValue"].ToString());
        }
    }
}