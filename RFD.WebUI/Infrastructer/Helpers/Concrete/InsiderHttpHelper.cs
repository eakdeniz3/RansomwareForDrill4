using Newtonsoft.Json;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using RFD.Infrastructer.EF.Helpers;
using RFD.WebUI.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RFD.WebUI.Infrastructer.Helpers
{
    public class InsiderHttpHelper : IInsiderHttpHelper
    {
        public HttpClient _httpClient;
        public InsiderHttpHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RFDClient");
        }
   
        public async Task<ApiResponse<InsiderModel>> AddAsync(Insider insider)
        {
            var response = await _httpClient.PostAsync("api/insider", new StringContent(JsonConvert.SerializeObject(insider), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<InsiderModel>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<InsiderModel>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }


        }
        public async Task<ApiResponse<Insider>> UpdateAsync(Insider insider)
        {
            var response = await _httpClient.PutAsync($"api/insider/{insider.Id}", new StringContent(JsonConvert.SerializeObject(insider), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<Insider>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<Insider>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }


        }
        public async Task<ApiResponse<Insider>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/insider/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<Insider>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<Insider>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }


        }
        public async Task<ApiResponse<PagingResponse<InsiderModel>>> GetAllAsync(InsiderParamerters paramerters)
        {
            
            var query = paramerters.ToQueryString();

            var response = await _httpClient.GetAsync($"api/insider?{query}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse<PagingResponse<InsiderModel>>.Fail($"Bir sorun ile karşılaşıldı.({response.Content})");

            }

            var pagingResponse = new PagingResponse<InsiderModel>
            {
                Items = JsonConvert.DeserializeObject<ApiResponse<List<InsiderModel>>>(content).Data,
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };

            return ApiResponse<PagingResponse<InsiderModel>>.Success( pagingResponse);

            //return JsonConvert.DeserializeObject<ApiResponse<PaginatedList<PhishingModel>>>(await response.Content.ReadAsStringAsync());


        }
        public async Task<ApiResponse<bool>> PauseCampaign(int id)
        {
            var response = await _httpClient.GetAsync($"api/insider/pause-campaign/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<bool>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }

        }
        public async Task<ApiResponse<Insider>> Duplicate(Insider insider)
        {

            var response = await _httpClient.PostAsync("api/insider/duplicate", new StringContent(JsonConvert.SerializeObject(insider), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<Insider>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<Insider>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }

        }
        public async Task<ApiResponse<bool>> SelectedDelete(List<Insider> insiders)
        {

            var response = await _httpClient.PostAsync("api/insider/selected-delete", new StringContent(JsonConvert.SerializeObject(insiders), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<bool>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }

        }
        public async Task<ApiResponse<bool>> StartCampaign(InsiderModel insider)
        {
            var response = await _httpClient.PostAsync($"api/insider/start-campaign", new StringContent(JsonConvert.SerializeObject(insider), Encoding.UTF8, "application/json"));

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
