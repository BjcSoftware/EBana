using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace EBana.WpfUI.Converters
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
			
			string selectedArticleType = (string)value;
			return (selectedArticleType == articleTypeLabelLinkedToSender) ? Visibility.Visible : Visibility.Hidden;
	    }
		
		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        return DependencyProperty.UnsetValue;
	    }
	}
}
