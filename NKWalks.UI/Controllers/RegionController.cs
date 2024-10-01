using Microsoft.AspNetCore.Mvc;
using NKWalks.UI.Models;
using NKWalks.UI.Models.DTO;
using System.Text;
using System.Text.Json;

namespace NKWalks.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7164/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());

              //  ViewBag.Response = stringResponseBody;
            }
            catch (Exception)
            {

            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegion)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7164/api/regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegion), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response != null)
            { 
                return RedirectToAction("Index","Region");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7164/api/regions/{id.ToString()}");

            if (response != null) 
            {
                return View(response);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDTO request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7164/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if (response != null) 
            {
                return RedirectToAction("Edit", "Region");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO request)
        {
            try
            {
                var client = httpClientFactory.CreateClient();

                var httpResposneMessage = await client.DeleteAsync($"https://localhost:7164/api/regions/{request.Id}");

                httpResposneMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Region");
            }
            catch (Exception)
            {

                throw;
            }

            return View();

        }
    }
}
