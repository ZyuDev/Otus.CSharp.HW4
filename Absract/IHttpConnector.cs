using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Otus.CSharp.HW4.Absract
{
    public interface IHttpConnector
    {
        Task<string> LoadPageContentAsync(string url);
    }
}
