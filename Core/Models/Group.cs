using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Aumc.Core.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public byte Room { get; set; }
        public byte GroupTypeId { get; set; }
        public GroupType GroupType { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCancel { get; private set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ICollection<Post> Posts { get; set; } = new Collection<Post>();
        public ICollection<Comment> Comments { get; set; } = new Collection<Comment>();
        public ICollection<Attendance> Attendances { get; set; } = new Collection<Attendance>();

        public void Cancel(IEnumerable<User> attendances)
        {
            IsCancel = true;

            var notification = Notification.CancelGroup(this);

            foreach (var attendee in attendances)
                attendee.Notify(notification);
        }

        public void SendNotification(IEnumerable<User> attendances, byte origionalRoom)
        {
            var notification = Notification.UpdateGroup(this, origionalRoom);

            foreach (var attendance in attendances)
                attendance.Notify(notification);
        }

        public void SendPostCreationNotification(IEnumerable<User> attendances, Post post)
        {
            var notification = Notification.CreatePost(post, this);

            foreach (var attendance in attendances)
                attendance.Notify(notification);
        }

        public void SendPostUpdatedNotification(IEnumerable<User> attendances, Post post)
        {
            var notification = Notification.UpdatePost(post, this);
            
            foreach (var attendence in attendances)
                attendence.Notify(notification);
        }

        public void SendPostDeleteNotification(IEnumerable<User> attendances, Post post)
        {
            var notification = Notification.DeletePost(post, this);

            foreach (var attendance in attendances)
                attendance.Notify(notification);
        }
    }
}