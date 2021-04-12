using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using PagedList;
using System.Web;
using System.IO;
using System;
using ShopBridge.Web.Filter;
using System.Net.Http.Headers;

namespace ShopBridge.Web.Controllers
{  
    [CustomExceptionFilter]
    public class InventoryController : Controller
    {
        private string _token = System.Web.HttpContext.Current.Session["token"].ToString();
        //Base url of api
        private readonly string baseUrl = "http://localhost:54076/";
        public async Task<ViewResult> Index( string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            IList<InventoryViewModel> inventoryModels = await GetInventoryModels(searchString);
           
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(inventoryModels.ToPagedList(pageNumber, pageSize));
        }

        private async Task<List<InventoryViewModel>> GetInventoryModels(string searchString)
        {
            List<InventoryViewModel> inventoryViewModels = new List<InventoryViewModel>();
            
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = await client.GetAsync(baseUrl + "api/Inventory");
                if (response.IsSuccessStatusCode) 
                {
                    var InvertoryResponse = response.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Inventory list  
                    inventoryViewModels = JsonConvert.DeserializeObject<List<InventoryViewModel>>(InvertoryResponse);
                    if (!string.IsNullOrEmpty(searchString))
                    {
                        inventoryViewModels = inventoryViewModels.FindAll(x => x.Name.ToLower().Contains(searchString.ToLower()));
                    }
                }
                else
                {
                    HandleError(response.StatusCode, response.Content.ReadAsStringAsync().Result);
                }
            }   
                  
            return inventoryViewModels;
        }    
           

        // GET: Inventory/Details/id
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryViewModel inventoryViewModel = null;
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = await client.GetAsync(baseUrl + "api/Inventory?id="+id);
                if (response.IsSuccessStatusCode)
                {
                    var InvertoryResponse = response.Content.ReadAsStringAsync().Result;
                    inventoryViewModel = JsonConvert.DeserializeObject<InventoryViewModel>(InvertoryResponse);
                }
                else
                {
                    HandleError(response.StatusCode, response.Content.ReadAsStringAsync().Result);
                }
            }
            if (inventoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(inventoryViewModel);
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
           var model = new InventoryViewModel();           
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InventoryViewModel inventoryViewModel, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("ImagePath", "Please upload image.");
            }

            if (ModelState.IsValid)
            {               
                string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                file.SaveAs(path);
                inventoryViewModel.ImagePath = "~/Images/" + Path.GetFileName(file.FileName);
                inventoryViewModel.IsActive = true;
                inventoryViewModel.DateIn = DateTime.Now;
               
                using (HttpClient client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    string stringData = JsonConvert.SerializeObject(inventoryViewModel);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8,"application/json");
                    var response = await client.PostAsync(baseUrl + "api/Inventory", contentData);
                    if (response.IsSuccessStatusCode)
                    {
                        var InvertoryResponse = response.Content.ReadAsStringAsync().Result;
                   }
                    else
                    {
                        HandleError(response.StatusCode, response.Content.ReadAsStringAsync().Result);
                    }

                }
                return RedirectToAction("Index");
            }

            return View(inventoryViewModel);
        }

        // GET: Inventory/Edit/id
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryViewModel inventoryViewModel = null;
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = await client.GetAsync(baseUrl + "api/Inventory?id="+id);
                if (response.IsSuccessStatusCode)
                {
                    var InvertoryResponse = response.Content.ReadAsStringAsync().Result;

                    inventoryViewModel = JsonConvert.DeserializeObject<InventoryViewModel>(InvertoryResponse);
                }
                else
                {
                    HandleError(response.StatusCode, response.Content.ReadAsStringAsync().Result);
                }
            }
            if (inventoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(inventoryViewModel);
        }

        // POST: Inventory/Edit/InventoryID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( InventoryViewModel inventoryViewModel,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(file != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    inventoryViewModel.ImagePath = "~/Images/" + Path.GetFileName(file.FileName);
                }   
               
                using (HttpClient client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                    string stringData = JsonConvert.SerializeObject(inventoryViewModel);
                    var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(baseUrl + "api/Inventory?id="+inventoryViewModel.InventoryID, contentData);
                    if (response.IsSuccessStatusCode)
                    {
                        var InvertoryResponse = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        HandleError(response.StatusCode, response.Content.ReadAsStringAsync().Result);
                    }

                }
                return RedirectToAction("Index");
            }
            return View(inventoryViewModel);
        }

        // GET: Inventory/Delete/id
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InventoryViewModel inventoryViewModel = null;
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await client.GetAsync(baseUrl + "api/Inventory?id="+id);                

                if (response.IsSuccessStatusCode)
                {
                    var InvertoryResponse = response.Content.ReadAsStringAsync().Result;

                    inventoryViewModel = JsonConvert.DeserializeObject<InventoryViewModel>(InvertoryResponse);
                }
                else
                {
                    HandleError(response.StatusCode, response.Content.ReadAsStringAsync().Result);
                }
            }
            if (inventoryViewModel == null)
            {
                return HttpNotFound();
            }
            return View(inventoryViewModel);
        }

        // POST: Inventory/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
                var response = await client.DeleteAsync(baseUrl + "api/Inventory?id="+id);
                if (response.IsSuccessStatusCode)
                {
                    var InvertoryResponse = response.Content.ReadAsStringAsync().Result;                    
                }
                else
                {
                    HandleError(response.StatusCode, response.Content.ReadAsStringAsync().Result);
                }
            }

            return RedirectToAction("Index");
        }

        private void HandleError(HttpStatusCode StatusCode, string errorMessage)
        {
            if (StatusCode == HttpStatusCode.BadRequest)
                throw new BadRequestException(errorMessage);
            else if (StatusCode == HttpStatusCode.InternalServerError)
                throw new InternalServerErrorException(errorMessage);
            else if (StatusCode == HttpStatusCode.NotFound)
                throw new NotFoundException(errorMessage);
            else
                throw new Exception(errorMessage);
        }
    }
}
