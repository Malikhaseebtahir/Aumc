using System;

namespace Aumc.Core.Models
{
    public class Like
    {
        public int LikerId { get; set; }
        public int LikeeId { get; set; }
        public User Liker { get; set; }
        public User Likee { get; set; }

        public void Notify(User likee, User liker)
        {
            var notification = Notification.LikerUser(likee, liker);

            likee.Notify(notification);
        }
    }
}