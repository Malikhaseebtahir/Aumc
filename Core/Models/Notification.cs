using System;

namespace Aumc.Core.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public NotificationType Type { get; private set; }
        public byte? OrigionalRoom { get; private set; }

        public Post Post { get; private set; }
        public int? PostId { get; private set; }

        public int? FollowerId { get; private set; }
        public User Follower { get; private set; }
        public int? FolloweeId { get; private set; }
        public User Followee { get; private set; }

        public int? LikeeId { get; private set; }
        public User Likee { get; private set; }
        public int? LikerId { get; private set; }
        public User Liker { get; private set; }
        
        public Group Group { get; private set; }
        public int? GroupId { get; private set; }
        public DateTime DateTime { get; private set; } = DateTime.Now;

        public Notification()
        { }
        private Notification(NotificationType notificationType, Group group)
        {
            if (group == null)
                throw new ArgumentNullException("group");

            Type = notificationType;
            Group = group;
        }

        private Notification(NotificationType notificationType, Post post, Group group)
        {
            if (post == null)
                throw new ArgumentNullException("post");
            
            if (group == null)
                throw new ArgumentNullException("group");

            Type = notificationType;
            Group = group;
            Post = post;
        }
        
        public Notification(NotificationType notificationType, User followee, User follower)
        {
            if (followee == null)
                throw new ArgumentNullException("followee");
            
            if (follower == null)
                throw new ArgumentNullException("follower");
            
            if (Convert.ToInt32(notificationType) == 6)
            {
                Type = notificationType;
                Followee = followee;
                Follower = follower;
            }

            if (Convert.ToInt32(notificationType) == 7)
            {
                Type = notificationType;
                Likee = followee;
                Liker = follower;
            }
        }

        public static Notification UpdateGroup(Group newGroup, byte origionalRoom)
        {
            return new Notification(NotificationType.GroupUpdated, newGroup)
            {
                OrigionalRoom = origionalRoom
            };
        }

        public static Notification CancelGroup(Group group)
        {
            return new Notification(NotificationType.GroupDelete, group);
        }

        public static Notification CreatePost(Post post, Group group)
        {
            return new Notification(NotificationType.PostCreated, post, group);
        }

        public static Notification UpdatePost(Post post, Group group)
        {
            return new Notification(NotificationType.PostUpdated, post, group);
        }

        public static Notification DeletePost(Post post, Group group)
        {
            return new Notification(NotificationType.PostDelete, post, group);
        }

        public static Notification FollowUser(User followee, User follower)
        {
            return new Notification(NotificationType.FollowUser, followee, follower);
        }

        public static Notification LikerUser(User likee, User liker)
        {
            return new Notification(NotificationType.LikerUser, likee, liker);
        }
    }
}