using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;
using EBana.Models;

namespace EBana.Converters
{	
	public class SelectedItemTypeToVisibilityConverter : IValueConverter
	{
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
			if (targetType != typeof(Visibility)) {
				throw new InvalidOperationException("La cible doit être de type bool");
	    	}
						
			if(value == null) return Visibility.Hidden;
			
			string articleTypeLabelLinkedToSender = parameter.ToString();
			
			TypeArticle selectedArticleType = (TypeArticle)value;
			return (selectedArticleType.Libelle == articleTypeLabelLinkedToSender) ? Visibility.Visible : Visibility.Hidden;
	    }
		
		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        return DependencyProperty.UnsetValue;
	    }
	}
}
