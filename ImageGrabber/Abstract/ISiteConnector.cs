using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ImageGrabber.Abstract
{
    public interface ISiteConnector
    {
        Uri BaseUrl { get; }
        Task<string> LoadPageContentAsync();
        Task<Stream> LoadImageAsync(string imageUrl);
    }
}
