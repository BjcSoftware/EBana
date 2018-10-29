using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace EBana.Converters
{
	/// <summary>
	/// Affiche les détails d'un article uniquement si un article est sélectionné
	/// value = article sélectionné
	/// </summary>
	public class SelectedArticleToDetailsVisibilityConverter : IValueConverter
	{
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
			if (targetType != typeof(Visibility)) {
				throw new InvalidOperationException("La cible doit être de type Visibility");
	    	}
			
			return (value == null) ? Visibility.Hidden : Visibility.Visible;
	    }
		
		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        return DependencyProperty.UnsetValue;
	    }
	}
}
