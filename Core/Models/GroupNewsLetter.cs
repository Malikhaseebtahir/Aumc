namespace Aumc.Core.Models
{
    public class GroupNewsLetter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public int GroupId { get; set; }
        public Group StudentGroup { get; set; }  
    }
}