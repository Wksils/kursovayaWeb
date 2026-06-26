namespace kursovayaWeb.Models
{
    public class AuthState
    {
        public bool IsAuthenticated { get; private set; } = false;
        public string AccessToken { get; private set; } = "";
        public int UserId { get; private set; }
        public string FullName { get; private set; } = "";
        public string Role { get; private set; } = "";
        public void SetUser(LoginResponse response)
        {
            IsAuthenticated = true;
            AccessToken = response.AccessToken;
            UserId = response.UserId;
            FullName = response.FullName;
            Role = response.Role;
        }
        public void Logout()
        {
            IsAuthenticated = false;
            AccessToken = "";
            UserId = 0;
            FullName = "";
            Role = "";
        }
    }
}
