using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class FormFileExtension
    {
        public static async Task<byte[]> getBytes(this IFormFile formFile)
        {
            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);
               
                return memoryStream.ToArray();
            }
        }
    }
}
