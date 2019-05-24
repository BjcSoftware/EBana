using NUnit.Framework;

namespace EBana.Domain.ArticlePictures.UnitTests
{
    [TestFixture]
    class ArticlePictureSettingsTests
    {
        [Test]
        [TestCase("photo.jpg")]
        public void IsCorrectPictureFile_CorrectFilePassed_ReturnsTrue(string fileName)
        {
            var settings = CreateSettingsWithExtension(".jpg");

            bool result = settings.IsCorrectPictureFile(fileName);

            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("photo.exe")]
        [TestCase("photo.jpgg")]
        public void IsCorrectPictureFile_IncorrectFilePassed_ReturnsFalse(string fileName)
        {
            var settings = CreateSettingsWithExtension(".jpg");

            bool result = settings.IsCorrectPictureFile(fileName);

            Assert.IsFalse(result);
        }

        private ArticlePictureSettings CreateSettingsWithExtension(string fileExtension)
        {
            return new ArticlePictureSettings("folderPath", fileExtension, "default");
        }
    }
}
