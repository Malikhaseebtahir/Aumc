using System;
using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class UseRegisterDto
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify the password between 4 to 8 characters")]
        public string Password { get; set; }        
        
        public string Gender { get; set; }
        
        [Required]
        public string TeacherOrStudent { get; set; }
        
        [Required]
        public string KnownAs { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        public byte InterestId { get; set; }

        [Required]        
        public AddressDto Address { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
    }
}