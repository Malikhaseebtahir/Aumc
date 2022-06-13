using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class SavePostDto
    {
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public byte PostCategoryId { get; set; }
        
        [Required]
        public int GroupId { get; set; }
    }
}