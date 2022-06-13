using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aumc.Controllers.ApiResources
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public byte Room { get; set; }        
        public GroupTypeDto GroupType { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public UserDto User { get; set; }
        public ICollection<PostDto> Posts { get; set; } = new Collection<PostDto>();
        public ICollection<CommentDto> Comments { get; set; } = new Collection<CommentDto>();
    }
}