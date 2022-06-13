using System;

namespace Aumc.Controllers.ApiResources
{
    public class GroupAttendanceReturnDto
    {
        public int GroupId { get; set; }
        public UserDto User { get; set; }
        public DateTime DateJoin { get; set; }
    }
}