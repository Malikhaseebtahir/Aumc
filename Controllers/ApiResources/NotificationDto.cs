using System;
using Aumc.Core.Models;

namespace Aumc.Controllers.ApiResources
{
    public class NotificationDto
    {
        public NotificationType Type { get; private set; }
        public byte? OrigionalRoom { get; private set; }
        public DateTime DateTime { get; private set; }        
        public GroupDto Group { get; private set; }
        public UserDto Follower { get; set; }
        public UserDto Liker { get; set; }
    }
}