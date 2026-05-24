using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using LegacyCRM.Core.Models;
using Newtonsoft.Json;

namespace LegacyCRM.Web.Services
{
    public class IntegrationService
    {
        public string DownloadCustomerFeed()
        {
            string apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            string requestUrl = apiBaseUrl.TrimEnd('/') + "/data";
            Exception lastError = null;

            for (int attempt = 1; attempt <= 3; attempt++)
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.Headers["Content-Type"] = "application/json";
                        return client.DownloadString(requestUrl);
                    }
                }
                catch (WebException ex)
                {
                    lastError = ex;
                    Thread.Sleep(1000);
                }
            }

            if (lastError != null)
            {
                throw lastError;
            }

            return string.Empty;
        }

        public string PostCustomerSnapshot(Customer customer)
        {
            string apiBaseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            string requestUrl = apiBaseUrl.TrimEnd('/') + "/customers/sync";
            string payload = JsonConvert.SerializeObject(customer, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = "POST";
            request.ContentType = "application/json";

            byte[] bytes = Encoding.UTF8.GetBytes(payload);
            request.ContentLength = bytes.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            using (var responseStream = response.GetResponseStream())
            using (var reader = new StreamReader(responseStream ?? Stream.Null))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
