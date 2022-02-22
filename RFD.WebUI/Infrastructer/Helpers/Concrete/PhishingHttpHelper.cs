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
    public class PhishingHttpHelper : IPhishingHttpHelper
    {
        public HttpClient _httpClient;
        public PhishingHttpHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RFDClient");
        }
   
        public async Task<ApiResponse<PhishingModel>> AddAsync(Phishing phishing)
        {
            var response = await _httpClient.PostAsync("api/phishing", new StringContent(JsonConvert.SerializeObject(phishing), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<PhishingModel>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<PhishingModel>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }


        }
        public async Task<ApiResponse<Phishing>> UpdateAsync(Phishing emailGroup)
        {
            var response = await _httpClient.PutAsync($"api/phishing/{emailGroup.Id}", new StringContent(JsonConvert.SerializeObject(emailGroup), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<Phishing>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<Phishing>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }


        }
        public async Task<ApiResponse<Phishing>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/phishing/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<Phishing>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<Phishing>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }


        }
        public async Task<ApiResponse<PagingResponse<PhishingModel>>> GetAllAsync(PhishingParamerters phishing)
        {
            
            var query = phishing.ToQueryString();

            var response = await _httpClient.GetAsync($"api/phishing?{query}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse<PagingResponse<PhishingModel>>.Fail($"Bir sorun ile karşılaşıldı.({response.Content})");

            }

            var pagingResponse = new PagingResponse<PhishingModel>
            {
                Items = JsonConvert.DeserializeObject<ApiResponse<List<PhishingModel>>>(content).Data,
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };

            return ApiResponse<PagingResponse<PhishingModel>>.Success( pagingResponse);

            //return JsonConvert.DeserializeObject<ApiResponse<PaginatedList<PhishingModel>>>(await response.Content.ReadAsStringAsync());


        }
        public async Task<ApiResponse<bool>> PauseCampaign(int id)
        {
            var response = await _httpClient.GetAsync($"api/phishing/pause-campaign/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<bool>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }

        }
        public async Task<ApiResponse<Phishing>> Duplicate(Phishing phishing)
        {

            var response = await _httpClient.PostAsync("api/phishing/duplicate", new StringContent(JsonConvert.SerializeObject(phishing), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<Phishing>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<Phishing>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }

        }
        public async Task<ApiResponse<bool>> SelectedDelete(List<Phishing> phishings)
        {

            var response = await _httpClient.PostAsync("api/phishing/selected-delete", new StringContent(JsonConvert.SerializeObject(phishings), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<bool>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }

        }
        public async Task<ApiResponse<bool>> StartCampaign(PhishingModel phishing)
        {
            var response = await _httpClient.PostAsync($"api/phishing/start-campaign", new StringContent(JsonConvert.SerializeObject(phishing), Encoding.UTF8, "application/json"));

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
