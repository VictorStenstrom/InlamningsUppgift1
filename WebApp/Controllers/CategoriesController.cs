using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7220/api");

        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> modelList = new List<ProductViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                var response = client.GetAsync(client.BaseAddress + "/Categories").Result;
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
        public async Task<ActionResult> Create(CreateCategoryViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                var response = client.PostAsync(client.BaseAddress + "/Categories", content).Result;
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
            UpdateCategoryViewModel model = new UpdateCategoryViewModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Categories/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<UpdateCategoryViewModel>(data);

                }
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Categories/" + model.Id, content).Result;
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View();
            }
        }

        public async Task<IActionResult> Delete(UpdateCategoryViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/Categories/" + model.Id).Result;
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index");

                return View();
            }
        }
    }
}
