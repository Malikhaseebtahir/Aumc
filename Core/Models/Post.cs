using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aumc.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public byte PostCategoryId { get; set; }
        public bool IsApproved { get; set; }
        public string PublicId { get; set; }
        public PostCategory PostCategory { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<PostComment> PostCommets { get; set; }
            = new Collection<PostComment>();
    }
}