using System;
using RestSharp;
using System.Threading;
using System.Text.RegularExpressions;
using CICD.Utils;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using CICD.Models;
using Microsoft.AspNetCore.Mvc;

namespace CICD.Helpers
{
	public class Client
	{
        
        public Client()
        {
        }

        /// <summary>
        /// Post
        /// </summary>      
        /// <param name="uri"></param>
        /// <param name="content"></param>       
        /// <returns></returns>
        public async Task<ApiResult> GetToken(string url, string content)
        {
            try
            {
                //get request uri
                var client = new RestClient(url.ToString());
                var request = new RestRequest();
                request.Method = RestSharp.Method.Post;
                request.AddHeader("Accept", "application/json");
                request.AddParameter("application/json", content, ParameterType.RequestBody);

                var response = client.ExecuteAsync(request).Result;
                if (response.IsSuccessful)
                {
                    return new ApiResult() { Data = response.Content };
                }
                else
                {
                    return new ApiResult() { Exception = "Error" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult() { Exception = ex.Message };
            }
        }

        /// <summary>
        /// Post
        /// </summary>      
        /// <param name="uri"></param>
        /// <param name="content"></param>       
        /// <returns></returns>
        public async Task<ApiResult> Post(string url, string content, string token)
        {
            try
            {
                //get request uri
                var client = new RestClient(url.ToString());
                var request = new RestRequest();
                request.Method = RestSharp.Method.Post;
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Authorization", $"Bearer {token}");
                request.AddHeader("Cache-Control", "no-cache");
                request.AddParameter("application/json", content, ParameterType.RequestBody);
                var response = client.ExecuteAsync(request).Result;
                if (response.IsSuccessful)
                {
                    return new ApiResult() { Data = response.Content };
                }
                else
                {
                    return new ApiResult() { Exception = "Error" };
                }
            }
            catch (Exception ex)
            {
                return new ApiResult() { Exception = ex.Message };
            }
        }
    }
}

