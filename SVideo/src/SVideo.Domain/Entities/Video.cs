using System;

namespace SVideo.Domain.Entities
{
    public class Video : GenericClass
    {
        public string Description { get; set; }
        public string ContentType { get; set; }
        public byte[] SizeInBytes { get; set; }
        public Guid IdServer { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Server Server { get; set; }
    }
}
