namespace Aumc.Controllers.ApiResources
{
    public class UserUpdateDto
    {
        public string Introduction { get; set; }
        public string ClassName { get; set; }
        public string LookingFor { get; set; }
        public byte InterestId { get; set; }
        public AddressDto Address { get; set; }
    }
}