using Microsoft.AspNetCore.Mvc;
using WebApp.Models.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7220/api");

        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> modelList = new List<ProductViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                var response = client.GetAsync(client.BaseAddress + "/Products").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
                }
                return View(modelList);
            }
        }

        public async Task<IActionResult> GetByCategory(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                List<ProductViewModel> modelList = new List<ProductViewModel>();
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Products/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
                }
                return View(modelList);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Products", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            UpdateProductViewModel model = new UpdateProductViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Products/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<UpdateProductViewModel>(data);

                }
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UpdateProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Products/" + model.Id, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
        }

        public async Task<IActionResult> Delete(UpdateProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Products/" + model.Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
        }
    }
}

