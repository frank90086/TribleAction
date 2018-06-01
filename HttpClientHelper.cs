using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TribleAction.Models.ApiModels;

namespace TribleAction
{
    public class HttpClientHelper
    {
        private HttpClient _httpClient;

        private readonly string _baseUrl;

        public HttpClientHelper(IOptions<ApiBaseUrl> setting)
        {
            _baseUrl = setting.Value.BaseUrl;
            Run();
        }

        private void Run()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-Device-Type", new string[] { "Web" });
        }

        #region CRUDAtion
        public T Get<T>(string uri) where T : class
        {
            HttpResponseMessage response = _httpClient.GetAsync(uri).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                if (result != null)
                    return PublicMethod.JsonDeSerialize<T>(result);
                else
                    return ReturnNewObject<T>(typeof(T)) as T;
            }
            else
                return ReturnNewObject<T>(typeof(T)) as T;
        }

        public T Post<T, P>(string uri, P model) where T : class where P : class
        {
            if (model != null)
            {
                Type propertys = typeof(P);
                List<KeyValuePair<string, string>> contentList = new List<KeyValuePair<string, string>>();

                foreach (var pro in propertys.GetProperties())
                {
                    contentList.Add(new KeyValuePair<string, string>(pro.Name, pro.GetValue(model).ToString()));
                }

                var content = new FormUrlEncodedContent(contentList);

                HttpResponseMessage response = _httpClient.PostAsync(uri, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return PublicMethod.JsonDeSerialize<T>(result);
                }
                else
                    return ReturnNewObject<T>(typeof(T)) as T;
            }
            else
                return ReturnNewObject<T>(typeof(T)) as T;
        }

        public T Put<T, P>(string uri, P model) where T : class where P : class
        {
            if (model != null)
            {
                Type propertys = typeof(P);
                List<KeyValuePair<string, string>> contentList = new List<KeyValuePair<string, string>>();

                foreach (var pro in propertys.GetProperties())
                {
                    if (pro.GetValue(model, null) != null)
                        contentList.Add(new KeyValuePair<string, string>(pro.Name, pro.GetValue(model).ToString()));
                    else
                        contentList.Add(new KeyValuePair<string, string>(pro.Name, null));
                }

                var content = new FormUrlEncodedContent(contentList);

                HttpResponseMessage response = _httpClient.PutAsync(uri, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return PublicMethod.JsonDeSerialize<T>(result);
                }
                else
                    return ReturnNewObject<T>(typeof(T)) as T;
            }
            else
                return ReturnNewObject<T>(typeof(T)) as T;
        }

        public T Delete<T>(string uri, string id) where T : class
        {
            if (!String.IsNullOrEmpty(id))
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(uri + id + "/").Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return PublicMethod.JsonDeSerialize<T>(result);
                }
                else
                    return ReturnNewObject<T>(typeof(T)) as T;
            }
            else
                return ReturnNewObject<T>(typeof(T)) as T;
        }
        #endregion

        private object ReturnNewObject<T>(Type objectType)
        {
            object obj = Activator.CreateInstance(objectType);
            return obj;
        }
    }
}