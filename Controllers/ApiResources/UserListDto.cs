using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aumc.Controllers.ApiResources
{
    public class UserListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string TeacherOrStudent { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
        public AddressDto Address { get; set; }
    }
}