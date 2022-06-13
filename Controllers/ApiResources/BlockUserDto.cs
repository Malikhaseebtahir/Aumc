using System;

namespace Aumc.Controllers.ApiResources
{
    public class BlockUserDto: UserDto
    {
        public string ClassName { get; set; }
        public string Message { get; set; }
    }
}