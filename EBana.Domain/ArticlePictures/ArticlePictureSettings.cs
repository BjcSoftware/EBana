using System;

namespace EBana.Domain.ArticlePictures
{
    public class ArticlePictureSettings
    {
        public string PictureFolderPath { get; private set; }
        public string PictureFileExtension { get; private set; }
        public string DefaultPictureName { get; private set; }

        public ArticlePictureSettings(
            string pictureFolderPath, 
            string pictureFileExtension, 
            string defaultPictureFileName)
        {
            if (pictureFolderPath == null)
                throw new ArgumentNullException("pictureFolderPath");
            if (pictureFileExtension == null)
                throw new ArgumentNullException("pictureFileExtension");
            if (defaultPictureFileName == null)
                throw new ArgumentNullException("defaultPictureFileName");

            PictureFolderPath = pictureFolderPath;
            PictureFileExtension = pictureFileExtension;
            DefaultPictureName = defaultPictureFileName;
        }
    }
}
