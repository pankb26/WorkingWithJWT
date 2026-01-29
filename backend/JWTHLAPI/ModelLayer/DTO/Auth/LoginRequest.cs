using System.ComponentModel.DataAnnotations;

namespace JWTHLAPI.ModelLayer.DTO.Auth
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}