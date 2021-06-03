using ImageGrabber.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGrabber.Concrete
{
    public class ImageGrabberApp
    {
        private ISiteConnector _connector;
        private IPageParser _parser;
        private IImageSaver _imageSaver;

        private int _imagesFound;
        private int _imagesDownloaded;
        private int _errors;

        public ImageGrabberApp(ISiteConnector connector, IPageParser parser, IImageSaver imageSaver)
        {
            _connector = connector;
            _parser = parser;
            _imageSaver = imageSaver;
        }

        public async Task GrabImagesAsync()
        {
            Console.WriteLine($"Loading page  {_connector.BaseUrl}");

            string content = "";
            try
            {
                content =  await _connector.LoadPageContentAsync();
                Console.WriteLine("Page loaded");

            }
            catch(Exception e)
            {
                Console.WriteLine($"Cannot load page {_connector.BaseUrl}. Message: {e.Message}");
            }

            if (string.IsNullOrEmpty(content))
            {

                Console.WriteLine($"Content is empty.");
                return;
            }

            var urlCollection = _parser.FindImages(content);
            _imagesFound = urlCollection.Count();

            if (_imagesFound == 0)
            {
                Console.WriteLine("No images found.");
            }

            var folderName = _imageSaver.DestinationFolderName();
            var dirInfo = Directory.CreateDirectory(folderName);

            foreach (var imageUrl in urlCollection)
            {

                Stream stream = null; ;
                try
                {
                    stream = await _connector.LoadImageAsync(imageUrl);
                    Console.WriteLine($"Image {imageUrl} loaded: {stream.Length}");

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Cannot load image: {imageUrl}. Message: {e.Message}");
                    _errors++;
                }

                if (stream == null)
                {
                    continue;
                }

                var fileName = _imageSaver.GetFileNameFromUrl(imageUrl);

                if (!_imageSaver.IsFileNameValid(fileName))
                {
                    Console.WriteLine($"ERROR. File name is not valid: {fileName}");
                    _errors++;
                    continue;
                }

                var fullPath = $"{dirInfo.FullName}\\{fileName}";

                try
                {
                    _imageSaver.SaveImage(stream, fullPath);
                    _imagesDownloaded++;
                    Console.WriteLine($"File saved: {fullPath}");

                }
                catch (Exception e)
                {
                    _errors++;
                    Console.WriteLine($"ERROR. Cannot save file: {fullPath} . Message: {e.Message}");
                }


            }

            Console.WriteLine();
            Console.WriteLine($"Images found: {_imagesFound}, Images downloaded: {_imagesDownloaded}, Errors: {_errors}");
        }
    }
}
