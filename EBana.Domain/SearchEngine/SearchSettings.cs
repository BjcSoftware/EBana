using EBana.Domain.Models;
using System.ComponentModel;

namespace EBana.Domain.SearchEngine
{
    public class SearchSettings : INotifyPropertyChanged
    {
        public SearchSettings()
        {
            Query = string.Empty;
        }

        private string _query;
        public string Query {
            get {
                return _query;
            }
            set {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private string _articleTypeFilter;
        public string ArticleTypeFilter {
            get {
                return _articleTypeFilter;
            }
            set {
                _articleTypeFilter = value;
                OnPropertyChanged(nameof(ArticleTypeFilter));
            }
        }

        private bool _isEpi;
        public bool IsEpi {
            get {
                return _isEpi;
            }
            set {
                _isEpi = value;
                OnPropertyChanged(nameof(IsEpi));
            }
        }

        private TypeEpi _epiTypeFilter;
        public TypeEpi EpiTypeFilter {
            get {
                return _epiTypeFilter;
            }
            set {
                _epiTypeFilter = value;
                OnPropertyChanged(nameof(EpiTypeFilter));
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
