using System;

namespace SVideo.Application.Models.ViewModels
{
    public class ServerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
}
