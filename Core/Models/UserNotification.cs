using System;

namespace Aumc.Core.Models
{
    public class UserNotification
    {
        public int UserId { get; private set; }
        public User User { get; private set; }
        public int NotificationId { get; private set; }
        public Notification Notification { get; private set; }        

        public bool IsRead { get; set; }

        public UserNotification()
        { }
        public UserNotification(User user, Notification notification)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            
            if (notification == null)
                throw new ArgumentNullException("notification");

            this.User = user;
            this.Notification = notification;
        }
    }
}