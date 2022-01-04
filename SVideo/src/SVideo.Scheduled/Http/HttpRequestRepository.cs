using RestSharp;
using SVideo.Application.Exceptions;
using System;
using System.Collections.Generic;

namespace SVideo.Scheduled.Http
{
    public class HttpRequestRepository
    {
        protected readonly RestClient client;

        public HttpRequestRepository()
        {
            client = new RestClient();
            client.AddDefaultHeaders(new Dictionary<string, string>()
            {
                {"Content-Type", "application/json"},
                {"Accept", "application/json"},
                {"User-Agent", ".net application"}
            });
        }

        protected virtual RestRequest ComposeRequest(Uri uri, Dictionary<string, string> headers, Dictionary<string, object> parameters)
        {
            RestRequest request = new RestRequest(uri);

            if (headers != null)
            {
                request.AddHeaders(headers);
            }

            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            return request;
        }

        private void ValidateResponse<T>(IRestResponse<T> response)
        {
            if (!response.IsSuccessful)
            {
                string url = response.Request.Resource;
                string method = response.Request.Method.ToString();
                string message = response.ErrorMessage;

                throw new HttpException(method, url, response.StatusCode, message);
            }
        }

        public virtual TResponse Get<TResponse>(Uri uri, Dictionary<string, string> headers, Dictionary<string, object> parameters, bool validateResponse = true)
        {
            RestRequest request = ComposeRequest(uri, headers, parameters);

            client.UseJson();

            IRestResponse<TResponse> response = client.Get<TResponse>(request);

            if (validateResponse)
            {
                ValidateResponse<TResponse>(response);
            }

            return response.Data;
        }

        public virtual TResponse Delete<TResponse>(Uri uri, Dictionary<string, string> headers, Dictionary<string, object> parameters)
        {
            RestRequest request = ComposeRequest(uri, headers, parameters);
 
            IRestResponse<TResponse> response = client.Delete<TResponse>(request);

            ValidateResponse<TResponse>(response);

            return response.Data;
        }

        public virtual TResponse Put<TResponse>(Uri uri, Dictionary<string, string> headers, Dictionary<string, object> parameters)
        {
            RestRequest request = ComposeRequest(uri, headers, parameters);
            
            IRestResponse<TResponse> response = client.Put<TResponse>(request);

            ValidateResponse<TResponse>(response);
            return response.Data;
        }
    }
}
