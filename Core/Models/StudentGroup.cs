using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aumc.Core.Models
{
    public class StudentGroup
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<StudentGroupPost> StudentGroupPosts { get; set; } 
            = new Collection<StudentGroupPost>();

        public ICollection<StudentGroupAttendance> StudentGroupAttendances { get; set; } 
            = new Collection<StudentGroupAttendance>();

        public ICollection<StudentGroupComment> StudentGroupComments { get; set; }
        = new Collection<StudentGroupComment>();

    }
}