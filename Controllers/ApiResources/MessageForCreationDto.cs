using System;

namespace Aumc.Controllers.ApiResources
{
    public class MessageForCreationDto
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.Now;
        public string Content { get; set; }
    }
}