using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DsK.ApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiGatewayController : ControllerBase
    {
        private readonly ILogger<ApiGatewayController> _logger;
        private readonly HttpClient _httpClient;

        public ApiGatewayController(ILogger<ApiGatewayController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [HttpGet(Name = "GetWebApi1EndPoint")]
        [Route("WebApi1")]
        public async Task<IEnumerable<WeatherForecast>> GetWebApi1()
        {
            var result = await WebApi1GetService();
            return result;
        }

        [HttpGet(Name = "GetWebApi2EndPoint")]
        [Route("WebApi2")]
        public async Task<IEnumerable<WeatherForecast>> GetWebApi2()
        {
            var result = await WebApi2GetService();
            return result;
        }

        private async Task<IEnumerable<WeatherForecast>> WebApi1GetService()
        {
            var response = await _httpClient.GetAsync("https://localhost:7238/WeatherForecast");
            if (!response.IsSuccessStatusCode)
                return null;

            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            });

            return responseObject;
        }

        private async Task<IEnumerable<WeatherForecast>> WebApi2GetService()
        {
            var response = await _httpClient.GetAsync("https://localhost:7125/WeatherForecast");
            if (!response.IsSuccessStatusCode)
                return null;

            var responseAsString = await response.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            });

            return responseObject;
        }
    }
}