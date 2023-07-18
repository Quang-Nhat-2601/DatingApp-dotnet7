using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class LoginDTO
    {  
        [Required]
        public string UserName {get; set;}
        public string Password {get; set;}
    }
}