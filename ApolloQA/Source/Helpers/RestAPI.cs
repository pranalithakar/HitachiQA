using System;
using System.Text;
using OpenQA.Selenium;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ApolloQA.Source.Helpers
{
    public class RestAPI
    {


        public static dynamic GET( String URL)
        {

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", getAuthToken());
            HttpResponseMessage response  = client.GetAsync(processURL(URL)).Result;
            return ConsumeResponse(response);

        }
        public static dynamic POST(String URL, dynamic body)
        {
            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(processURL(URL));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", getAuthToken());

            String bodyString;
            if (body is String)
            {
                bodyString = body;
            }
            else
            {
                bodyString = JsonConvert.SerializeObject(body);
            }

            HttpContent content = new StringContent(bodyString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(processURL(URL), content).Result;
            return ConsumeResponse(response);
        }
        private static String processURL(String URL)
        {
            if (!URL.StartsWith("http"))
            {
                if (!URL.StartsWith("/"))
                {
                    return Environment.GetEnvironmentVariable("SERVER_HOST") + "/"+URL;
                }
                return Environment.GetEnvironmentVariable("SERVER_HOST") + URL;
            }

            return URL;
        }

        private static dynamic ConsumeResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            String dataObjects = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<dynamic>(dataObjects);
        }

        private static String getAuthToken()
        {

            String currentUser= (String)((IJavaScriptExecutor)Driver.Setup.driver).ExecuteScript("return window.localStorage.getItem('currentUser')");
            return (String)JsonConvert.DeserializeObject<dynamic>(currentUser)["accessToken"];

        }
    }
}
