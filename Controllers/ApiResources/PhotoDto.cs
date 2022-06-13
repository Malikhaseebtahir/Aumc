using System;

namespace Aumc.Controllers.ApiResources
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsApproved { get; set; }
        public bool IsMain { get; set; }
    }
}