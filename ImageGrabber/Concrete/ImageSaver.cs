using ImageGrabber.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ImageGrabber.Concrete
{
    public class ImageSaver : IImageSaver
    {
        public const string FolderPrefix = "Images";

        public void SaveImage(Stream sourceStream, string destinationPath)
        {
            using (var fileStream = File.Create(destinationPath))
            {
                sourceStream.Seek(0, SeekOrigin.Begin);
                sourceStream.CopyTo(fileStream);
            }
        }

        public string DestinationFolderName()
        {
            return $"{FolderPrefix}_{DateTime.Now:yyyyMMddHHmmss}";
        }

        public string GetFileNameFromUrl(string url)
        {
            var fileName = "";

            if (string.IsNullOrEmpty(url))
            {
                return fileName;
            }

            var lastSlashIndex = url.LastIndexOf('/');

            if (lastSlashIndex > -1)
            {
                fileName = url.Substring(lastSlashIndex + 1);
            }
            else
            {
                fileName = url;
            }

            return fileName;
        }

        public bool IsFileNameValid(string testName)
        {
            if (string.IsNullOrEmpty(testName))
            {
                return false;
            }

            var regexCheck = new Regex("[" + Regex.Escape(new string(Path.GetInvalidPathChars())) + "]");

            if (regexCheck.IsMatch(testName))
            {
                return false;
            }

            var ext = Path.GetExtension(testName);

            if (ext.Length > 4 || ext.Length == 0)
            {
                return false;
            }

            return true;
        }
    }
}
