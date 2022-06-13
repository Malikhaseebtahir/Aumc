using System;

namespace Aumc.Controllers.ApiResources
{
    public class StudentGroupPostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public UserDto User { get; set; }
        public DateTime DateAdded { get; set; }
    }
}