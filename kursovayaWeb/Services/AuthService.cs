using kursovayaWeb.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace kursovayaWeb.Services
{
    

    public class AuthService
    {
        private HttpClient client = new HttpClient();
        public async Task<Responce> SignIn(SignInUser user)
        {
            JsonContent content = JsonContent.Create(user);
            using var response = await client.PostAsync("http://localhost:5043/api/Account/token", content);
            string responseText = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Responce resp = JsonSerializer.Deserialize<Responce>(responseText)!;
                RegisterUser.usernsme = resp.username;
                RegisterUser.access_token = resp.access_token;
                return resp;
            }
            return null!;
        }
        public async Task<AuthState> getUser()
        {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + RegisterUser.access_token);
                var response = await client.GetFromJsonAsync<AuthState>("http://localhost:5043/api/Account/info");
                if (response != null) return response;
            
            return null!;
        }
    }

}