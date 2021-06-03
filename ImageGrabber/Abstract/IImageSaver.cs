using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageGrabber.Abstract
{
    public interface IImageSaver
    {
        string DestinationFolderName();
        string GetFileNameFromUrl(string url);
        bool IsFileNameValid(string testName);
        void SaveImage(Stream sourceStream, string destinationPath);
    }
}
