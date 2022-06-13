using System;

namespace Aumc.Core.Models
{
    public class Attendance
    {
        public int GroupId { get; set; }
        public int UserId { get; set; }
        public Group Group { get; set; }
        public User User { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateJoin { get; set; } = DateTime.Now;
    }
}