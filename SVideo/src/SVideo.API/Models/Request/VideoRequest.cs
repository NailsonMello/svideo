using Microsoft.AspNetCore.Http;

namespace SVideo.API.Models.Request
{
    /// <summary>
    /// Video Request
    /// </summary>
    public class VideoRequest
    {
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// SizeInBytes
        /// </summary>
        public IFormFile SizeInBytes { get; set; }
    }
}
