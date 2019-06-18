using System;

namespace EBana.Services.DesktopAppServices.ArticlePictures
{
    public class ArticlePictureSettings
    {
        public string PictureFolderPath { get; }
        private string PictureFileExtension { get; }
        public string DefaultPictureName { get; }

        public ArticlePictureSettings(
            string pictureFolderPath, 
            string pictureFileExtension, 
            string defaultPictureFileName)
        {
            if (pictureFolderPath == null)
                throw new ArgumentNullException(nameof(pictureFolderPath));
            if (pictureFileExtension == null)
                throw new ArgumentNullException(nameof(pictureFileExtension));
            if (defaultPictureFileName == null)
                throw new ArgumentNullException(nameof(defaultPictureFileName));

            PictureFolderPath = pictureFolderPath;
            PictureFileExtension = pictureFileExtension;
            DefaultPictureName = defaultPictureFileName;
        }

        public string FormatPicturePath(string pictureName)
        {
            return $"{PictureFolderPath}/{FormatPictureFileName(pictureName)}";
        }

        public string FormatPictureFileName(string pictureName)
        {
            return $"{pictureName}.{PictureFileExtension}";
        }

        public string FormatDefaultPictureFileName()
        {
            return FormatPictureFileName(DefaultPictureName);
        }

        public bool IsCorrectPictureFile(string fileName)
        {
            return fileName.EndsWith(PictureFileExtension);
        }
    }
}
