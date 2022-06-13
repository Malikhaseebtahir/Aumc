using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class SaveStudentGroupNewsLetterDto
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }     
        
        [Required]
        public int GroupId { get; set; }   
    }
}