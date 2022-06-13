using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class SaveCommentDto
    {
        [Required]
        public string Message { get; set; }
        
        [Required]
        public int GroupId { get; set; }
    }
}