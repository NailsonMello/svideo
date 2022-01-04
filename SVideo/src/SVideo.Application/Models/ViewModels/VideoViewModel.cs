using System;

namespace SVideo.Application.Models.ViewModels
{
    public class VideoViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public byte[] SizeInBytes { get; set; }
        public Guid IdServer { get; set; }
    }
}
