namespace AspServer.Models
{
    public class ChangePasswordInput
    {
        public string? Token { get; set; }
        public string? Password { get; set; }
    }
}
