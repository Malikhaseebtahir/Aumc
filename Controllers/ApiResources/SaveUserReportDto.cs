namespace Aumc.Controllers.ApiResources
{
    public class SaveUserReportDto
    {
        public int ReporterId { get; set; }
        public UserDto Reporter { get; set; }

        public int ReporteeId {  get; set; }
        public UserDto Reportee { get; set; }

        public string Message { get; set; }
    }
}