using System.ComponentModel;

namespace EBana.PresentationLogic.Core.ViewModel
{
	public class Notifier : INotifyPropertyChanged
	{
	    public event PropertyChangedEventHandler PropertyChanged;
	
	    protected void OnPropertyChanged(string propertyName)
	    {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
	}
}
