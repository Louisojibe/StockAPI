namespace TestWebApp.Dtos.Auth
{
    public class NewUserDto
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
