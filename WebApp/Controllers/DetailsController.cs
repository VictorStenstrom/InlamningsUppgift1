using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models.ViewModels;

namespace WebApp.Controllers
{
    public class DetailsController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7220/api");

        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> modelList = new List<ProductViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                var response = client.GetAsync(client.BaseAddress + "/Details").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);

                }
                return View(modelList);
            }
        }

        public async Task<IActionResult> GetDetails(int id)
        {
            List<ProductViewModel> modelList = new List<ProductViewModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = baseAddress;
                var response = client.GetAsync(client.BaseAddress + "/Details/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    modelList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
                }
                return View(modelList);
            }
        }
    }
}
