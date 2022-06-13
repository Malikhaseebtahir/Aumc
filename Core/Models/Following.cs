using System;

namespace Aumc.Core.Models
{
    public class Following
    {
        public int FollowerId { get; set; }
        public int FolloweeId { get; set; }
        
        public User Follower { get; set; }
        public User Followee { get; set; }

        public void Notify(User followee, User follower)
        {
            var notification = Notification.FollowUser(followee, follower);

            followee.Notify(notification);
        }
    }
}