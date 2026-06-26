using System.Net.Http.Json;
using kursovayaWeb.Models;

namespace kursovayaWeb.Services
{
    

    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly AuthState _authState;

        public AuthService(HttpClient http, AuthState authState)
        {
            _http = http;
            _authState = authState;
        }

        public async Task<(bool Success, string ErrorMessage)> LoginAsync(string login, string password)
        {
            try
            {
                var request = new LoginRequest { Login = login, Password = password };
                var response = await _http.PostAsJsonAsync("http://localhost:5043/api/Account/token", request);

                if (!response.IsSuccessStatusCode)
                    return (false, "Неверный логин или пароль");

                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (result == null)
                    return (false, "Ошибка ответа сервера");

                _authState.SetUser(result);

                // После входа подставляем токен во все дальнейшие запросы этого HttpClient
                _http.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.AccessToken);

                return (true, "");
            }
            catch (HttpRequestException)
            {
                return (false, "Нет соединения с сервером");
            }
        }
    }
}