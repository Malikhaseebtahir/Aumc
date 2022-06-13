using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aumc.Core.Models
{
    public class StudentGroupPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public bool IsApproved { get; set; }

        public int GroupId { get; set; }
        public StudentGroup Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<StudentGroupPostComment> StudentGroupPostComments { get; set; }
            = new Collection<StudentGroupPostComment>();
    }
}