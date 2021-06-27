using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using IdentityModel.Client;
using System.Threading.Tasks;

namespace ApiTwo.Controllers
{
    public class HomeController : Controller
    {
        private IHttpClientFactory _httpClient;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            //retrieve access token
            var serverClient = _httpClient.CreateClient();

            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44326/");
            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address=discoveryDocument.TokenEndpoint,
                ClientId="client_id",
                ClientSecret="client_secret",
                Scope="ApiOne"
            });
            //retrieve secret data

            var apiClient = _httpClient.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var response= await apiClient.GetAsync("https://localhost:44383/secret");
            var content =await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                access_token=tokenResponse.AccessToken,
                message=content
            }); 
        }
    }
}
