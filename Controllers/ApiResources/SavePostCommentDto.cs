using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class SavePostCommentDto
    {
        [Required]
        public string Message { get; set; }
        
        [Required]
        public int PostId { get; set; }
    }
}