using Otus.CSharp.HW4.Concrete;
using System;
using System.Net.Http;

namespace Otus.CSharp.HW4
{
    class Program
    {
        static void Main(string[] args)
        {

            var connector = new HttpConnector(new HttpClient());
            var parser = new PageParser();

            var url = @"https://lapkins.ru/cat/";
            //url = @"https://yandex.ru/";
            url = @"http://funkot.ru/";

            Console.WriteLine($"Loading page  {url}");

            var content = connector.LoadPageContentAsync(url).Result;
            Console.WriteLine("Page loaded");

            var urlCollection = parser.FindImages(content);

            foreach(var imageUrl in urlCollection)
            {
                Console.WriteLine(imageUrl);
                break;
            }


        }
    }
}
