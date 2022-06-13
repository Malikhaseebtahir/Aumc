using System;
using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class SaveGroupDto
    {
        [Required]
        [MaxLength(128)]
        public string ClassName { get; set; }
        
        [Required]
        [MaxLength(128)]        
        public string Section { get; set; }

        [Required]
        [MaxLength(128)]
        public string Subject { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public byte Room { get; set; }        
        public byte GroupTypeId { get; set; }    }
}