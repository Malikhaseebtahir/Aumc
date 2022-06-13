namespace Aumc.Controllers.ApiResources
{
    public class StudentGroupDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public UserDto User { get; set; }
    }
}