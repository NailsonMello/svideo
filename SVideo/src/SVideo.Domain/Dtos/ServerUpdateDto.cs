using System;

namespace SVideo.Domain.Dtos
{
    public class ServerUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
}
