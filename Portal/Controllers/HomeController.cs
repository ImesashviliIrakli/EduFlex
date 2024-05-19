using Application.Interfaces.Services;
using Application.Models;
using Domain.Auth;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using System.Diagnostics;

namespace Portal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITokenService _service;
        private readonly IConfiguration _config;
        private readonly HttpClient httpClient;
        public HomeController(ITokenService service, IConfiguration config)
        {
            httpClient = new HttpClient();
            _service = service;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var tokenResult = await _service.GetToken("api1");
            httpClient.SetBearerToken(tokenResult.AccessToken);

            var result = await httpClient.GetAsync(_config["apiUrl"] + "/api/User");

            if (result.IsSuccessStatusCode)
            {
                var user = await result.Content.ReadFromJsonAsync<List<UserDto>>();
                return View(user);
            }

            return View();
        }
    }
}
