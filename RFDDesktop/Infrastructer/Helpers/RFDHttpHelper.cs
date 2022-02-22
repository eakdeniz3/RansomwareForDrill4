using RFDDesktop.Infrastructer.Extentions;
using RFDDesktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using RFDDesktop;

namespace RFDDesktop.Infrastructer.Helpers
{

    public class RFDHttpHelper
    {
        public const string _apiKey = "94298e93-398d-4e08-a610-e042f489cb9c";
        public const string _baseAddress = "http://10.56.16.216:53716/api/summary";
        //public const string _baseAddress = "http://54.124.34.119:53716/api/summary";
        public HttpClient _client;
        public RFDHttpHelper()
        {
        }

        public string GetUserName()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
            ManagementObjectCollection collection = searcher.Get();
            var usernames = collection.Cast<ManagementBaseObject>().ToList();
            var username = "";
            foreach (var item in usernames)
            {
                if (item["UserName"].ToString().ToLower() != "system")
                    username += item["UserName"];

            }
            return username;
        }

        public async Task<int> AddAsync(string url = _baseAddress, int timeout = 30000)
        {
            try
            {
                using (_client = new HttpClient())
                {
                    var summary = new Summary
                    {
                        ComputerName = Environment.MachineName,
                        UserName = GetUserName(),
                    };
                    _client.Timeout = new TimeSpan(timeout);
                    _client.DefaultRequestHeaders.Add("ApiKey", _apiKey);
                    _client.DefaultRequestHeaders.Add("Accept", "application/xml");
                    var content = new StringContent(XmlConverter.Serialize<Summary>(summary), Encoding.UTF8, "application/xml");
                    // url = string.IsNullOrEmpty(url) ? _baseAddress : url;
                    var response = await _client.PostAsync($"{url}", content);
                    var result = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(result))
                    {
                        var model = XmlConverter.DeserializeObject<ResponseModel<int>>(result);
                        if (model.IsSucceeded)
                            return model.Data;
                    }
                    return default(int);
                }
            }
            catch
            {
                return default(int);
            }
        }


        public async Task<bool> DidTrueClosedAsync(string url = _baseAddress, int timeout = 30000)
        {
            try
            {
                using (_client = new HttpClient())
                {
                    _client.Timeout = new TimeSpan(timeout);
                    _client.DefaultRequestHeaders.Add("ApiKey", _apiKey);
                    _client.DefaultRequestHeaders.Add("Accept", "application/xml");
                    var response = await _client.GetAsync($"{url}/{Program._summayId}/did-true-closed");
                    var result = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(result))
                    {
                        var model = XmlConverter.DeserializeObject<ResponseModel<bool>>(result);
                        if (model.IsSucceeded)
                            return model.Data;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetSummaryCountAsync(string url = _baseAddress, int timeout = 30000)
        {
            try
            {
                using (_client = new HttpClient())
                {
                    _client.DefaultRequestHeaders.Add("ApiKey", _apiKey);
                    _client.DefaultRequestHeaders.Add("Accept", "application/xml");
                    _client.Timeout = new TimeSpan(timeout);
                    var response = await _client.GetAsync($"{url}/get-summary-count");
                    var result = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(result))
                    {
                        var model = XmlConverter.DeserializeObject<ResponseModel<int>>(result);

                        if (model.IsSucceeded)
                            return model.Data;
                    }
                    return default(int);

                }
            }
            catch
            {
                return default(int);
            }
        }
    }
}
