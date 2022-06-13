using System;

namespace Aumc.Controllers.ApiResources
{
    public class UserDto
    {
        public int Id { get; set; }
        public string KnownAs { get; set; }
        public DateTime LastActive { get; set; }
        public string TeacherOrStudent { get; set; }
        public string Url { get; set; }
    }
}