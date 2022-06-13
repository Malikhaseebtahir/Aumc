using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class SaveStudentGroupDto
    {
        [Required]
        [MaxLength(128)]
        public string Subject { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string Description { get; set; }        
    }
}