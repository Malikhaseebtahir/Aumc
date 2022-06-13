namespace Aumc.Core.Models
{
    public class BlockUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string KnownAs { get; set; }
        public string Message { get; set; }
        public string ClassName { get; set; }
        public string TeacherOrStudent { get; set; }
    }
}