namespace Aumc.Core.Models
{
    public class UserReport
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public User Reporter { get; set; }

        public int ReporteeId { get; set; }
        public User Reportee { get; set; }

        public string Message { get; set; }
        public bool IsApproved { get; set; }
    }
}