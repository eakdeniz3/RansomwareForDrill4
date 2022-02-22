using Newtonsoft.Json;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Infrastructer.EF.Helpers;
using RFD.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Helpers
{
    public class RFDHelper : IRFDHelper
    {
        public HttpClient _httpClient;
        public RFDHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RFDClient");
        }

        public async Task<ApiResponse<bool>> PauseInsiderCampaign(int id)
        {
            var response = await _httpClient.GetAsync($"api/pause-insider-campaign/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<bool>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }

        }


        public async Task<ApiResponse<bool>> StartInsiderCampaign(InsiderModel insider)
        {
            var response = await _httpClient.PostAsync($"api/start-insider-campaign", new StringContent(JsonConvert.SerializeObject(insider, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<bool>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }
        }
    }
}
