using System;
using System.Collections.Generic;
using System.Text;

namespace Otus.CSharp.HW4.Absract
{
    public interface IPageParser
    {
        IEnumerable<string> FindImages(string content);
    }
}
