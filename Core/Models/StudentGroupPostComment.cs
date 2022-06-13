using System;

namespace Aumc.Core.Models
{
    public class StudentGroupPostComment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public StudentGroupPost Post { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;            
    }
}