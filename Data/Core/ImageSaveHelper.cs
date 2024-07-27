using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Data.Core
{
    public static class ImageHelper
    {
        private static readonly string[] AllowedExtensions = new[] { ".jpg", ".png", ".jpeg" };

        public static async Task<string> SaveImageAsync(IFormFile imageFile, string rootPath)
        {
            if (imageFile == null)
            {
                throw new ArgumentNullException(nameof(imageFile));
            }

            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!AllowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Geçerli bir resim seçiniz!");
            }

            var randomFileName = $"{Guid.NewGuid()}{extension}";
            var path = Path.Combine(rootPath, "wwwroot/img", randomFileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return randomFileName;
        }
    }
}