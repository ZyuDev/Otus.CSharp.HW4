using System;
using System.Collections.Generic;
using System.Text;

namespace ImageGrabber.Abstract
{
    public interface IPageParser
    {
        List<string> FindImages(string content);
    }
}
