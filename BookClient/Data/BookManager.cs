using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookClient.Data
{
    public class BookManager
    {
        const string URL = "https://fast-castle-50377.herokuapp.com/";
        private string strAuthorizationKey;

        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            //login information and confirmation
            if (string.IsNullOrEmpty(strAuthorizationKey))
            {
                try
                {
                    UserItem loginObject = new UserItem();
                    loginObject.identifier = "student";
                    loginObject.password = "Student1234";
                    var content = new StringContent(JsonConvert.SerializeObject(loginObject), Encoding.UTF8,
                        "application/json");
                    //client.DefaultRequestHeaders.Accept(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await client.PostAsync(URL + "auth/local", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    strAuthorizationKey = "Bearer " + JsonConvert.DeserializeObject<LoginItem>(responseBody).jwt;
                    client.DefaultRequestHeaders.Add("Authorization", strAuthorizationKey);

                    return client;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message, e);
                }

            }

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async Task<List<BookItem>> GetAllBooks()
        {
            HttpClient client = await GetClient();
            string StrResult = await client.GetStringAsync(URL + "Books");
            return JsonConvert.DeserializeObject<List<BookItem>>(StrResult);
        }

        public async Task<BookItem> Add(string Title, string Description, string ISBN)
        {
            HttpClient client = await GetClient();
            BookItem item = new BookItem();
            item.Title = Title;
            item.Description = Description;
            item.ISBN = ISBN;
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8,
                "application/json");
            var response = await client.PostAsync(URL + "Books", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookItem>(responseBody);
        }
        public async Task<BookItem> Update(string Title, string Description, string ISBN, int id)
        {
            HttpClient client = await GetClient();
            BookItem item = new BookItem();
            item.Title = Title;
            item.Description = Description;
            item.ISBN = ISBN;
            var content = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8,
                "application/json");
            var response = await client.PutAsync(URL + "Books/" + id, content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookItem>(responseBody);
        }
        public async void Delete(int id)
        {
            HttpClient client = await GetClient();
            var response = await client.DeleteAsync(URL + "Books/" + id);
            response.EnsureSuccessStatusCode();
        }
    }
}
