using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aumc.Core.Models
{
    public class StudentGroupAttendance
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int GroupId { get; set; }
        public StudentGroup Group { get; set; }        

        public DateTime DateJoin { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; }
    }
}