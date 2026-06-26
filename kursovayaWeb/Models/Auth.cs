namespace kursovayaWeb.Models
{
    public class LoginRequest
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = "";
        public string Role { get; set; } = "";
        public string Department { get; set; } = "";
        public string AccessToken { get; set; } = "";
    }
}
