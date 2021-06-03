using Otus.CSharp.HW4.Absract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Otus.CSharp.HW4.Concrete
{
    public class PageParser : IPageParser
    {
        public IEnumerable<string> FindImages(string content)
        {
            var ulrCollection = new List<string>();

            var pattern = "<img.+?src=[\\\"'](.+?)[\\\"'].*?>";
            var regEx = new Regex(pattern, RegexOptions.IgnoreCase);

            var matches = regEx.Matches(content);

            foreach (Match match in matches)
            {
                ulrCollection.Add(match.Groups[1].Value);
            }

            return ulrCollection;
        }
    }
}
