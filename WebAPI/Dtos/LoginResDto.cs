using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public class LoginResDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}