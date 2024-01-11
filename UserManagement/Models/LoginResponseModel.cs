namespace UserManagement.Models
{
    public class LoginResponseModel
    {
        public string IdToken { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}
