using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Aumc.Core.Models
{
    public class User: IdentityUser<int>
    {
        public string Gender { get; set; }
        public string KnownAs { get; set; }
        public string ClassName { get; set; }
        public Address Address { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public bool IsDisabled { get; set; }
        public byte? InterestId { get; set; }
        public Interest Interest { get; set; }
        public string TeacherOrStudent { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Like> Liker { get; set; } = new Collection<Like>();
        public ICollection<Like> Likee { get; set; } = new Collection<Like>();
        public ICollection<Post> Posts { get; set; } = new Collection<Post>();
        public ICollection<Photo> Photos { get; set; } = new Collection<Photo>();
        public ICollection<Group> Groups { get; set; } = new Collection<Group>();
        public ICollection<Comment> Comments { get; set; } = new Collection<Comment>();
        public ICollection<Message> MessageSent { get; set; } = new Collection<Message>();
        public ICollection<Following> Followees { get; set; } = new Collection<Following>();
        public ICollection<Following> Followers { get; set; } = new Collection<Following>();
        public ICollection<Message> MessageReceived { get; set; } = new Collection<Message>();
        public ICollection<UserReport> Reporters { get; set; } = new Collection<UserReport>();
        public ICollection<UserReport> Reportees { get; set; } = new Collection<UserReport>();
        public ICollection<UserNotification> UserNotifications { get; set; } = new Collection<UserNotification>();
    
        public int GetAge 
        { 
            get 
            {
                var today = DateTime.Now;
                var age = today.Year - DateOfBirth.Year;

                if (DateOfBirth.Date > today.AddYears(-age))
                    age--;
                    
                return age;                
            }
        }

        public void Notify(Notification notification)
        {
            UserNotifications.Add(new UserNotification(this, notification));
        }
    }
}