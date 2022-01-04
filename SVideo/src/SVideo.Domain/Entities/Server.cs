using System.Collections.Generic;

namespace SVideo.Domain.Entities
{
    public class Server : GenericClass
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
