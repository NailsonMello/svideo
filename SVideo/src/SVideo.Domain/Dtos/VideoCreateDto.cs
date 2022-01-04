using Microsoft.AspNetCore.Http;

namespace SVideo.Domain.Dtos
{
    public class VideoCreateDto
    {
        public string Description { get; set; }
        public IFormFile SizeInBytes { get; set; }
    }
}
