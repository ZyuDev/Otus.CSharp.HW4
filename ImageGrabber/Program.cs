using ImageGrabber.Concrete;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ImageGrabber
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var defaultAddress = @"https://lapkins.ru/cat/";

            Console.WriteLine($"Enter site address to grab images, if empty will be processed: {defaultAddress}");
            var userAddress = Console.ReadLine();

            var siteAddress = string.IsNullOrEmpty(userAddress) ? defaultAddress : userAddress;

            Uri siteUri = null;
            try
            {
                siteUri = new Uri(siteAddress);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Wrong address: {siteAddress}. Message: {e.Message}");
            }

            if (siteUri != null)
            {
                var connector = new SiteConnector(new HttpClient(), siteUri);
                var parser = new PageParser();
                var imageSaver = new ImageSaver();

                var grabber = new ImageGrabberApp(connector, parser, imageSaver);
                await grabber.GrabImagesAsync();
            }


        }
    }
}
