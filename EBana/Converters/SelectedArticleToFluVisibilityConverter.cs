using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using EBana.Models;

namespace EBana.Converters
{
    /// <summary>
    /// Le FLU n'est visible que pour les articles de type banalisé
    /// </summary>
	public class SelectedArticleToFluVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if(targetType != typeof(Visibility))
				throw new InvalidOperationException("La cible doit être de type Visibility");
			
			Article selectedArticle = (Article)value;
			
			if(selectedArticle is Banalise) {
				if(((Banalise)selectedArticle).LienFlu != null) {
					return Visibility.Visible;
				}
			}
			
			return Visibility.Hidden;
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
