namespace SVideo.API.Models.Request
{
    /// <summary>
    /// Server Request
    /// </summary>
    public class ServerRequest
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Ip Address
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }
    }
}
