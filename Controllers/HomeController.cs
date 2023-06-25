using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WeatherApplication.Models;

namespace WeatherApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        { 
            // Get user's IP address
            // Note: Here i have directly enter IP ADDRESS.

            string userIpAddress = "103.190.40.127"; // HttpContext.Connection.RemoteIpAddress.ToString();

            // Use IP geolocation service to get user's location
            string geolocationApiUrl = $"http://ip-api.com/json/{userIpAddress}";

            HttpClient client = _httpClientFactory.CreateClient();
            HttpResponseMessage geolocationResponse = await client.GetAsync(geolocationApiUrl);

            if (geolocationResponse.IsSuccessStatusCode)
            {
                var geolocationData = await geolocationResponse.Content.ReadFromJsonAsync<GeolocationData>();
                string countryCode = geolocationData.CountryCode;
                string city = geolocationData.City;

                // Use weather API to fetch weather data for the user's location
                string weatherApiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city},{countryCode}&appid=af6acb4cadf8908947450009fb9ba5cb";

                HttpResponseMessage weatherResponse = await client.GetAsync(weatherApiUrl);

                if (weatherResponse.IsSuccessStatusCode)
                {
                    
                    var weatherJson = await weatherResponse.Content.ReadAsStringAsync();
                    var weatherObject = JsonDocument.Parse(weatherJson);
                    var temperature = weatherObject.RootElement.GetProperty("main").GetProperty("temp").GetDecimal();

                    // Convert the temperature to string
                    string temperatureString = temperature.ToString();

                    var weatherData = new WeatherData
                    {
                        Temperature = temperatureString,
                        MinTemperature = weatherObject.RootElement.GetProperty("main").GetProperty("temp_min").GetDecimal().ToString(),
                        MaxTemperature = weatherObject.RootElement.GetProperty("main").GetProperty("temp_max").GetDecimal().ToString(),
                        WindSpeed = weatherObject.RootElement.GetProperty("wind").GetProperty("speed").GetDecimal().ToString(),
                        Humidity = weatherObject.RootElement.GetProperty("main").GetProperty("humidity").GetDecimal().ToString(),
                        Country = weatherObject.RootElement.GetProperty("sys").GetProperty("country").ToString(),
                        City = city,
                        Description = weatherObject.RootElement.GetProperty("weather")[0].GetProperty("description").ToString()
                    };

                    return View(weatherData);


                }
            }

            // Handle the case when API requests fail or data is not available
            return Content("Error");
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