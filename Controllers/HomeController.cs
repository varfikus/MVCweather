using Microsoft.AspNetCore.Mvc;
using MVCweather.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MVCweather.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string cityName = "Tiraspol")
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid=a20188c6aed44b4e1cf7e12151e7e770&units=metric");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var weatherData = JsonConvert.DeserializeObject<WeatherViewModel>(responseString);

            return View(weatherData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
