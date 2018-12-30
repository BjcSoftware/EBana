using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using System.IO;
using EBana.Domain.Models;

namespace EBana.WpfUI.Converters
{
	/// <summary>
	/// Récupérer l'emplacement de l'image liée à un epi s'il y en a une
    /// Sinon retourner l'emplacement d'une autre image indiquant l'absence d'image
	/// </summary>
	public class SelectedEpiTypeToPicturePathConverter : IValueConverter
	{
		private string pictureExtension = "JPG";
		private string pictureDirectory = "images/epi";
		
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(targetType != typeof(System.Windows.Media.ImageSource))
				throw new InvalidOperationException("La cible doit être de type ImageSource");
            
            TypeEpi selectedEpiType = (TypeEpi)value;

		    string notFoundPath = null;

            // aucun EPI n'est sélectionné
            if (selectedEpiType == null)
                return notFoundPath;

            // emplacement où devrait être l'image de l'EPI sélectionné si elle existe
            string potentialImagePath = string.Format("{0}/{1}.{2}", 
                                                      pictureDirectory,
                                                      selectedEpiType.Libelle.ToLower(), 
                                                      pictureExtension);

            // il existe une image liée à l'EPI sélectionné, retourner son emplacement
            if(File.Exists(potentialImagePath))
            {
                return string.Format("pack://siteoforigin:,,,/{0}", potentialImagePath);
            }

            // la photo sélectionnée n'a pas de photo
            return notFoundPath;
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
