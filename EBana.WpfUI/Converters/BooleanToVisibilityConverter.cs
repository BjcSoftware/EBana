using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace EBana.WpfUI.Converters
{
	/// <summary>
	/// Convertir un Boolean en une valeur de type Visibility
	/// true = Visibility.Visible
	/// false = Visibility.Hidden
	/// </summary>
	public class BooleanToVisibilityConverter : IValueConverter
	{
		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {
			if (targetType != typeof(Visibility)) {
				throw new InvalidOperationException("La cible doit être de type Visibility");
	    	}
			
			if(value == null) return Visibility.Hidden;
			
			return ((bool)value ? Visibility.Visible : Visibility.Hidden);
	    }
		
		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	    {
	        return DependencyProperty.UnsetValue;
	    }
	}
}
