namespace Aumc.Core.Models
{
    public class StudentGroupNewsLetter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public int GroupId { get; set; }
        public StudentGroup StudentGroup { get; set; }
    }
}