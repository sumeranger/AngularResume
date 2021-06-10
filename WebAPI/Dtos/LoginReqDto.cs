using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos
{
    public class LoginReqDto
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}