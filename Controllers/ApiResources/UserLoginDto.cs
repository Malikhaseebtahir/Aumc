using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}