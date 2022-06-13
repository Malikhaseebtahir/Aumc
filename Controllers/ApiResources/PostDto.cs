using System;

namespace Aumc.Controllers.ApiResources
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string PublicId { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
        public PostCategoryDto PostCategory { get; set; }
    }
}