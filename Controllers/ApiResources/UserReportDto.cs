namespace Aumc.Controllers.ApiResources
{
    public class UserReportDto
    {
        public int Id { get; set; }
        public UserDto Reporter { get; set; }

        public UserDto Reportee { get; set; }

        public string Message { get; set; }        
    }
}