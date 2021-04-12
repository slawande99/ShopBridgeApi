using Newtonsoft.Json;
using ShopBridge.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShopBridge.Web.Controllers
{
    public class HomeController : Controller
    { //Base url of api
        private readonly string baseUrl = "http://localhost:54076/";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {   
            return View();
        }

        [HttpPost]        
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    string stringData = JsonConvert.SerializeObject(model);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(baseUrl + "api/authenticate", contentData);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var token = JsonConvert.DeserializeObject<String>(result);
                        System.Web.HttpContext.Current.Session["token"] = token;                      
                        return RedirectToAction("Index", "Inventory");
                    }
                }
            }           
            return View(model);
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["token"] = "";
            return RedirectToAction("Index","Home");            
        }

        public ActionResult Error(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
    }
}