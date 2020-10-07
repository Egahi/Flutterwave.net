﻿using Flutterwave.Net.Utilities;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Flutterwave.Net
{
    public class FlutterwaveApi : IFlutterwaveApi
    {
        private HttpClient _httpClient { get; }
        public IBanks Banks { get; }
        public IMiscellaneous Miscellaneous { get; }
        public IPayments Payments { get; }
        public ISubAccounts SubAccounts { get; }
        public ITransactions Transactions { get; }

        public FlutterwaveApi(string secretKey)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(Endpoints.FLUTTERWAVE_API_BASE_URL) };
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Banks = new Banks(this);
            Miscellaneous = new Miscellaneous(this);
            Payments = new Payments(this);
            SubAccounts = new SubAccounts(this);
            Transactions = new Transactions(this);
        }

        /// <summary>
        /// Make Get requests to Flutterwave API
        /// </summary>
        /// <typeparam name="T">Response Data Type</typeparam>
        /// <param name="relativeUrl">endpoint</param>
        /// <returns></returns>
        internal T Get<T>(string relativeUrl)
        {
            string responseStr = _httpClient.GetAsync(relativeUrl)
                                            .Result
                                            .Content
                                            .ReadAsStringAsync()
                                            .Result;

            var responseData = JsonConvert.DeserializeObject<T>(responseStr);

            return responseData;
        }

        /// <summary>
        /// Make Post requests to Flutterwave to API
        /// </summary>
        /// <typeparam name="T">Response Data Type</typeparam>
        /// <param name="relativeUrl">endpoint</param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal T Post<T>(string relativeUrl, object data)
        {
            var jsonData = new StringContent(JsonConvert.SerializeObject(data),
                                             Encoding.UTF8,
                                             "application/json");

            string responseStr = _httpClient.PostAsync(relativeUrl, jsonData)
                                            .Result
                                            .Content
                                            .ReadAsStringAsync()
                                            .Result;

            var responseData = JsonConvert.DeserializeObject<T>(responseStr);

            return responseData;
        }

        /// <summary>
        /// Make Delete requests to Flutterwave to API
        /// </summary>
        /// <typeparam name="T">Response Data Type</typeparam>
        /// <param name="relativeUrl">endpoint</param>
        /// <returns></returns>
        internal T Delete<T>(string relativeUrl)
        {
            string responseStr = _httpClient.DeleteAsync(relativeUrl)
                                            .Result
                                            .Content
                                            .ReadAsStringAsync()
                                            .Result;

            var responseData = JsonConvert.DeserializeObject<T>(responseStr);

            return responseData;
        }
    }
}
