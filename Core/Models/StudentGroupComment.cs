using System;

namespace Aumc.Core.Models
{
    public class StudentGroupComment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public StudentGroup Group { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
    }
}