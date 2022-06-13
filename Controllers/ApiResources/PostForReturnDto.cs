using System;

namespace Aumc.Controllers.ApiResources
{
    public class PostForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string PublicId { get; set; }
    }
}