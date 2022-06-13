namespace Aumc.Controllers.ApiResources
{
    public class FollowingDto
    {
        public UserDto Followee { get; set; }        
        public UserDto Follower { get; set; }        
    }
}