using System;

namespace Aumc.Core.Models
{
    public class PostComment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;        
    }
}