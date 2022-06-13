using System;

namespace Aumc.Controllers.ApiResources
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public UserDto User { get; set; }
        public GroupDto Group { get; set; }
        public DateTime DateAdded { get; set; }
    }
}