using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {

        private readonly string ApiBaseUrl = "https://api.spoonacular.com/";
        private readonly string apiKey = "4a8f3379b304426993dc0262a69cd42d";
        private HttpClient httpClient;

        public RecipeController()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri(ApiBaseUrl),
            };
        }

        [HttpGet("GetListOfRecipe")]
        public async Task<IEnumerable<RecipieInfo>> GetListOfRecipe(string query, int page = 1, int pageSize = 10)
        {
            IEnumerable<RecipieInfo> recipieInfos = new List<RecipieInfo>();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"recipes/complexSearch?apiKey={apiKey}&query={query}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<RecipieInfoList>(data);

                    if (responseData != null &&
                        responseData.results?.Any() == true)
                    {
                        recipieInfos = responseData.results;
                    }
                }
            }
            catch (Exception ex)
            {
                recipieInfos = new List<RecipieInfo>();
            }
            return recipieInfos;
        }

        [HttpGet("GetRecipe/{id}")]
        public async Task<RecipeDetialInfo> GetRecipe(long id)
        {
            RecipeDetialInfo recipieInfos = new RecipeDetialInfo();

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync($"/recipes/{id}/information?apiKey={apiKey}");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    recipieInfos = JsonConvert.DeserializeObject<RecipeDetialInfo>(data);
                }
            }
            catch (Exception ex)
            {
                recipieInfos = new RecipeDetialInfo();
            }
            return recipieInfos;
        }
    }
}
