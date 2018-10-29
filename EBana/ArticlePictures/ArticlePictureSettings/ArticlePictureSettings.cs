namespace EBana.ArticlePictures
{
    public class ArticlePictureSettings
    {
        public string PictureFolderPath { get; private set; }
        public string PictureFileExtension { get; private set; }
        public string DefaultPictureName { get; private set; }

        public ArticlePictureSettings(string pictureFolderPath, string pictureFileExtension, string defaultPictureFileName)
        {
            PictureFolderPath = pictureFolderPath;
            PictureFileExtension = pictureFileExtension;
            DefaultPictureName = defaultPictureFileName;
        }
    }
}
