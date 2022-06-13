using System.ComponentModel.DataAnnotations;

namespace Aumc.Controllers.ApiResources
{
    public class SaveStudentGroupAttendanceDto
    {
        [Required]
        public int GroupId { get; set; }
    }
}