namespace kursovayaWeb.Models
{
    public class AuthState
    {
        public bool IsAuthenticated { get;  set; } = false;
        public string AccessToken { get;  set; } = "";
        public int UserId { get;  set; }
        public string FullName { get;  set; } = "";
        public string Role { get;  set; } = "";
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
