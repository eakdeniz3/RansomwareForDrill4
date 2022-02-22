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
    public class SummaryHttpHelper : ISummaryHttpHelper

    {
        public HttpClient _httpClient;
        public SummaryHttpHelper(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RFDClient");
        }
   
    
       
        public async Task<ApiResponse<Summary>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/summary/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse<Summary>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return ApiResponse<Summary>.Fail($"Bir sorun ile karşılaşıldı.({response.StatusCode})");
            }


        }
        public async Task<ApiResponse<PagingResponse<SummaryModel>>> GetAllAsync(SummaryParamerters paramerters)
        {
            
            var query = paramerters.ToQueryString();

            var response = await _httpClient.GetAsync($"api/summary?{query}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse<PagingResponse<SummaryModel>>.Fail($"Bir sorun ile karşılaşıldı.({response.Content})");

            }

            var pagingResponse = new PagingResponse<SummaryModel>
            {
                Items = JsonConvert.DeserializeObject<ApiResponse<List<SummaryModel>>>(content).Data,
                MetaData = JsonConvert.DeserializeObject<MetaData>(response.Headers.GetValues("X-Pagination").First())
            };
        
            return ApiResponse<PagingResponse<SummaryModel>>.Success( pagingResponse);

            //return JsonConvert.DeserializeObject<ApiResponse<PaginatedList<PhishingModel>>>(await response.Content.ReadAsStringAsync());


        }

        public async Task<ApiResponse<CountData>> GetCountDataAsync()
        {
            var response = await _httpClient.GetAsync($"api/summary/get-count-data");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse<CountData>.Fail($"Bir sorun ile karşılaşıldı.({response.Content})");

            }


            return ApiResponse<CountData>.Success(JsonConvert.DeserializeObject<ApiResponse<CountData>>(content).Data);
        }

        public async Task<ApiResponse<bool>> SelectedDelete(List<Summary> computersRunningApp)
        {

            var response = await _httpClient.PostAsync("api/summary/selected-delete", new StringContent(JsonConvert.SerializeObject(computersRunningApp), Encoding.UTF8, "application/json"));

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
