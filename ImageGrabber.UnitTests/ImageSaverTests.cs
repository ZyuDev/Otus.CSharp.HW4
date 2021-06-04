using ImageGrabber.Concrete;
using NUnit.Framework;

namespace ImageGrabber.UnitTests
{
    public class Tests
    {

        private ImageSaver _imageSaver;

        [SetUp]
        public void Setup()
        {
            _imageSaver = new ImageSaver();
        }

        [Test]
        public void GetFileNameFromUrl_EmptyUrl_ReturnEmptyString()
        {
            var fileName = _imageSaver.GetFileNameFromUrl(null);

            Assert.IsEmpty(fileName);
        }

        [Test]
        public void GetFileNameFromUrl_UrlWithImage_ReturnFileName()
        {
            var fileName = _imageSaver.GetFileNameFromUrl(@"/path/image.jpg");

            Assert.AreEqual("image.jpg", fileName);
        }

        [Test]
        public void GetFileNameFromUrl_UrlWithImageNoSlash_ReturnFileName()
        {
            var fileName = _imageSaver.GetFileNameFromUrl(@"image.jpg");

            Assert.AreEqual("image.jpg", fileName);
        }

        [Test]
        public void IsFileNameValid_ValidFileName_ReturnTrue()
        {
            var isValid = _imageSaver.IsFileNameValid("image.jpg");

            Assert.IsTrue(isValid);
        }

        [Test]
        public void IsFileNameValid_NotValidFileName_ReturnFalse()
        {
            var isValid = _imageSaver.IsFileNameValid("image.jpg|");

            Assert.IsFalse(isValid);
        }

        [Test]
        public void IsFileNameValid_LongExtension_ReturnFalse()
        {
            var isValid = _imageSaver.IsFileNameValid("image.jpg?11234");

            Assert.IsFalse(isValid);
        }

        [Test]
        public void IsFileNameValid_NoExtension_ReturnFalse()
        {
            var isValid = _imageSaver.IsFileNameValid("image");

            Assert.IsFalse(isValid);
        }

        [Test]
        public void IsFileNameValid_EmptyFileName_ReturnFalse()
        {
            var isValid = _imageSaver.IsFileNameValid(null);

            Assert.IsFalse(isValid);
        }
    }
}