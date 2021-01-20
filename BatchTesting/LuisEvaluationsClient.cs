// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace BatchTesting
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    internal class LuisEvaluationsClient
    {
        private const string ApimHeader = "Ocp-Apim-Subscription-Key";

        private readonly string _key;

        private readonly string _uri;

        private static HttpClient _httpClient;

        public LuisEvaluationsClient(string endpoint, string appId, string key)
        {
            _key = key;
            _uri = $"{endpoint}/luis/v3.0-preview/apps/{appId}/slots/production/evaluations";
            _httpClient = new HttpClient();
        }

        public async Task<OperationContract> CreateEvaluationsOperationAsync(LuisEvaluationsRequest batchInput)
        {
            var headers = new Dictionary<string, string>()
            {
                [ApimHeader] = _key
            };
            HttpResponseMessage res = await SendJsonPostRequestAsync(_uri, batchInput, headers);

            if (res.StatusCode != HttpStatusCode.Accepted)
            {
                throw new Exception($"Received status code {res.StatusCode} and message {await res.Content.ReadAsStringAsync()}");
            }
            var responseString = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OperationContract>(responseString);
        }

        public async Task<OperationContract> GetEvaluationsStatusAsync(string operationId)
        {
            var url = _uri + $"/{operationId}/status";

            var headers = new Dictionary<string, string>()
            {
                [ApimHeader] = _key
            };

            HttpResponseMessage res = await SendGetRequestAsync(url, headers);

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Received status code {res.StatusCode} and message {await res.Content.ReadAsStringAsync()}");
            }
            var responseString = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OperationContract>(responseString);
        }

        // TODO: handle pagination
        public async Task<EvaluationResult> GetEvaluationsResultAsync(string operationId)
        {
            var url = _uri + $"/{operationId}/result?verbose=true";

            var headers = new Dictionary<string, string>()
            {
                [ApimHeader] = _key
            };

            HttpResponseMessage res = await SendGetRequestAsync(url, headers);

            if (res.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Received status code {res.StatusCode} and message {await res.Content.ReadAsStringAsync()}");
            }
            var responseString = await res.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EvaluationResult>(responseString);
        }

        private async Task<HttpResponseMessage> SendGetRequestAsync(string url, Dictionary<string, string> headers)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                PopulateRequestMessageHeaders(headers, requestMessage);
                HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
                return response;
            }
        }

        private async Task<HttpResponseMessage> SendJsonPostRequestAsync(string url, object body, Dictionary<string, string> headers)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, url))
            {
                PopulateRequestMessageHeaders(headers, requestMessage);
                var requestBodyAsJson = JsonConvert.SerializeObject(body);
                requestMessage.Content = new StringContent(requestBodyAsJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
                return response;
            }
        }

        private void PopulateRequestMessageHeaders(Dictionary<string, string> headers, HttpRequestMessage requestMessage)
        {
            foreach (KeyValuePair<string, string> h in headers)
            {
                requestMessage.Headers.Add(h.Key, h.Value);
            }
        }
    }
}