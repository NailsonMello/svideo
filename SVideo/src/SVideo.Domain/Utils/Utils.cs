using Microsoft.AspNetCore.Http;
using System.IO;

namespace SVideo.Domain.Utils
{
    public static class Utils
    {
        public static byte[] FileToByte(this IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            }

            return null;
        }
    }
}
