using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TestCase
{
    public class SimpleOAuthClient
    {

        public string AccessToken { get; set; }

        public string TokenUri { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }



        public SimpleOAuthClient(string uri, string userName, string password)
        {
            this.TokenUri = uri;
            this.UserName = userName;
            this.Password = password;
        }

        public async Task<Token> GetToken()
        {
            Console.WriteLine("Token:" + TokenUri);
            Console.WriteLine("--------------------------------");
            StringBuilder sb = new StringBuilder();
            sb.Append("grant_type=password");
            sb.Append("&username=" + UserName);
            sb.Append("&password=" + Password);
            sb.Append("&client_id=Mobile");
            HttpResponseMessage response;
            string oauthcontent = sb.ToString();
            using (var client = new HttpClient())
            {
                Console.WriteLine("Post Content:" + oauthcontent);
                Console.WriteLine("--------------------------------");
                StringContent queryString = new StringContent(oauthcontent);
                response = await client.PostAsync(TokenUri, queryString);

            }
            string responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Response Content:" + responseContent);
            Console.WriteLine("--------------------------------");
            if (!response.IsSuccessStatusCode)
            {
              //  
                throw new Exception(string.Format(
                    "API Error: Status Code: {0}", response.StatusCode));
            }

            //string responseContent = await response.Content.ReadAsStringAsync();
           // Console.WriteLine(responseContent);
            if (!string.IsNullOrEmpty(responseContent))
            {
                Token token = JsonConvert.DeserializeObject<Token>(responseContent);
                return token;
            }

            return null;
        }


        public async Task<string> PostAsync(string baseRequestUri)
        {
            Console.WriteLine("PostAsync Uri:" + baseRequestUri);
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                StringContent queryString = new StringContent("");
                response = await client.PostAsync(baseRequestUri, queryString);
            }
            Console.WriteLine("--------------------------------");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(string.Format(
                    "API Error: Status Code: {0}", response.StatusCode));
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
