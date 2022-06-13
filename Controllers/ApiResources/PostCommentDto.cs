using System;

namespace Aumc.Controllers.ApiResources
{
    public class PostCommentDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public UserDto User { get; set; }
        public PostDto Post { get; set; }
        public DateTime DateAdded { get; set; }
    }
}