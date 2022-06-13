using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aumc.Controllers.ApiResources
{
    public class UserDetailsDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string ClassName { get; set; }
        public string TeacherOrStudent { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public InterestDto Interest { get; set; }
        public AddressDto Address { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoDto> Photos { get; set; } = new Collection<PhotoDto>(); 
    }
}