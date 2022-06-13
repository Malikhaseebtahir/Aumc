using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class SaveUniversityNewsDto
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}