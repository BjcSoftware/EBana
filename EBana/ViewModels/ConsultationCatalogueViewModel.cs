using System.Linq;
using System.Collections.ObjectModel;
using EBana.Models;
using OsServices.Web;
using OsServices.Dialog;
using System.Windows.Input;
using Data.Repository;
using EBana.ArticlePictures;
using System.Collections.Generic;

namespace EBana.ViewModels
{
    public class ConsultationCatalogueViewModel : Notifier
	{
        public ICommand SearchCommand { get; private set; }
        public ICommand ShowSelectedArticleFluCommand { get; private set; }

        private readonly IArticlePictureLocator articlePictureLocator;
        private readonly IMessageBoxService messageBoxService;
        private readonly IWebBrowserService webBrowserService;
        private readonly IReader<TypeArticle> typeArticleReader;
        private readonly IReader<TypeEpi> typeEpiReader;
        private readonly ArticleSearchEngine articleSearchEngine;

        public ConsultationCatalogueViewModel(
            IArticlePictureLocator articlePictureLocator,
            IMessageBoxService messageBoxService, 
            IWebBrowserService webBrowserService,
            IReader<TypeArticle> typeArticleReader,
            IReader<TypeEpi> typeEpiReader,
            ArticleSearchEngine articleSearchEngine)
		{
            this.articlePictureLocator = articlePictureLocator;
            this.messageBoxService = messageBoxService;
            this.webBrowserService = webBrowserService;
            this.typeArticleReader = typeArticleReader;
            this.typeEpiReader = typeEpiReader;
            this.articleSearchEngine = articleSearchEngine;

            SearchCommand = new RelayCommand(() => SearchArticles());
            ShowSelectedArticleFluCommand = new RelayCommand(() => ShowSelectedArticleFlu());
            
            InitPropertiesBoundToUI();
        }

        private void InitPropertiesBoundToUI()
        {
            InitAvailableArticleTypesList();
            InitAvailableEpiTypesList();
            InitSearchCriterias();
            SearchResults = new ObservableCollection<Article>();
        }

        private void InitAvailableArticleTypesList()
        {
            var articlesTypes = typeArticleReader.GetAll();
            AvailableArticleTypes = new ObservableCollection<TypeArticle>(articlesTypes);
            SelectDefaultArticleType();
        }

        private void SelectDefaultArticleType()
        {
            if (AvailableArticleTypes.Count != 0)
            {
                SelectedArticleType = AvailableArticleTypes.First();
            }
        }

        private void InitAvailableEpiTypesList()
        {
            var epiTypes = typeEpiReader.GetAll();
            AvailableEpiTypes = new ObservableCollection<TypeEpi>(epiTypes);
            SelectDefaultEpiType();
        }

        private void SelectDefaultEpiType()
        {
            if (AvailableEpiTypes.Count != 0)
            {
                SelectedEpiType = AvailableEpiTypes.First();
            }
        }

        private void InitSearchCriterias()
        {
            AvailableSearchCriterias = new ObservableCollection<string>()
            {
                "Libellé",
                "Référence"
            };
            SelectDefaultSearchCriteria();
        }

        private void SelectDefaultSearchCriteria()
        {
            if(AvailableSearchCriterias.Count != 0)
            {
                SelectedSearchCriteria = AvailableSearchCriterias.First();
            }
        }

        /// <summary>
        /// Lancer une recherche à partir des paramètres de recherche actuels.
        /// Les résultats sont placés dans la liste SearchResults.
        /// </summary>
        private void SearchArticles()
		{
            RefreshSearchResults();

            if (SearchResults.Count == 0)
            {
                NotifyUserThatNoResultFound();
            }
        }

        private void RefreshSearchResults()
        {
            IEnumerable<Article> foundArticles;
            if (SelectedSearchCriteria == "Libellé")
            {
                foundArticles = SearchArticlesByLabel();
            }
            else
            {
                foundArticles = SearchArticlesByRef();
            }

            ResetSearchResultsTo(foundArticles);
        }

        private IEnumerable<Article> SearchArticlesByLabel()
        {
            IEnumerable<Article> foundArticles;
            if(IsSearchedArticleBanalise())
            {
                if(IsSearchedArticleEpi())
                {
                    foundArticles = articleSearchEngine.SearchEpiByLabel(SearchQuery, SelectedEpiType);
                }
                else
                {
                    foundArticles = articleSearchEngine.SearchBanaliseByLabel(SearchQuery);
                }
            }
            else
            {
                foundArticles = articleSearchEngine.SearchSelByLabel(SearchQuery);
            }

            return foundArticles;
        }

        private IEnumerable<Article> SearchArticlesByRef()
        {
            IEnumerable<Article> foundArticles;
            if (IsSearchedArticleBanalise())
            {
                if (IsSearchedArticleEpi())
                {
                    foundArticles = articleSearchEngine.SearchEpiByRef(SearchQuery, SelectedEpiType);
                }
                else
                {
                    foundArticles = articleSearchEngine.SearchBanaliseByRef(SearchQuery);
                }
            }
            else
            {
                foundArticles = articleSearchEngine.SearchSelByRef(SearchQuery);
            }

            return foundArticles;
        }

        private bool IsSearchedArticleBanalise()
        {
            return SelectedArticleType.Libelle == "Banalisé";
        }

        private bool IsSearchedArticleEpi()
        {
            return IsSearchedArticleEPI;
        }

        private void ResetSearchResultsTo(IEnumerable<Article> articles)
        {
            articles = articles.OrderBy(a => a.Ref);
            SearchResults = new ObservableCollection<Article>(articles);
        }

        private void NotifyUserThatNoResultFound()
        {
            messageBoxService.Show("Information",
                                "Aucun résultat ne correspond à vos critères de recherche.",
                                System.Windows.MessageBoxButton.OK);
        }

        private void ShowSelectedArticleFlu()
        {
            string url = ((Banalise)SelectedArticle).LienFlu;
            webBrowserService.Open(url);
        }

        private void OnSelectedArticleChanged()
        {
            UpdateDisplayedArticlePicture();
        }

        private void UpdateDisplayedArticlePicture()
        {
            SelectedArticlePicturePath = articlePictureLocator
                .GetArticlePictureLocationOrDefault(SelectedArticle).OriginalString;
        }

        private void OnSelectedSearchCriteriaChanged()
		{
            SearchQuery = string.Empty;
		}

        #region Propriétés bound à l'UI

        #region Paramètres de recherche

        #region Type d'article
        private ObservableCollection<TypeArticle> mAvailableArticleTypes;
		public ObservableCollection<TypeArticle> AvailableArticleTypes
		{
			get { return mAvailableArticleTypes; }
			set {
				mAvailableArticleTypes = value;
				OnPropertyChanged("AvailableArticleTypes");
			}
		}
		
		private TypeArticle mSelectedArticleType;
		public TypeArticle SelectedArticleType
		{
			get { return mSelectedArticleType; }
			set {
				mSelectedArticleType = value;
				OnPropertyChanged("SelectedArticleType");
			}
		}
		
		# endregion
		
		#region Libellé / Référence à rechercher
		private string mSearchQuery;
		public string SearchQuery
        {
			get { return mSearchQuery; }
			set {
                mSearchQuery = value;
				OnPropertyChanged("SearchQuery");
			}
		}
		
		private ObservableCollection<string> mAvailableSearchCriterias;
		public ObservableCollection<string> AvailableSearchCriterias
		{
			get { return mAvailableSearchCriterias; }
			set {
                mAvailableSearchCriterias = value;
				OnPropertyChanged("AvailableSearchCriterias");
			}
		}
		
		private string mSelectedSearchCriteria;
		public string SelectedSearchCriteria
		{
			get { return mSelectedSearchCriteria; }
			set {
				mSelectedSearchCriteria = value;
				OnSelectedSearchCriteriaChanged();
				OnPropertyChanged("SelectedSearchCriteria");
			}
		}
		
		#endregion
		
		#region Paramètres EPIs
		private bool mIsSearchedArticleEPI;
		public bool IsSearchedArticleEPI
		{
			get { return mIsSearchedArticleEPI; }
			set {
				mIsSearchedArticleEPI = value;
				OnPropertyChanged("IsSearchedArticleEPI");
			}
		}
		
		private ObservableCollection<TypeEpi> mAvailableEpiTypes;
		public ObservableCollection<TypeEpi> AvailableEpiTypes
		{
			get { return mAvailableEpiTypes; }
			set {
				mAvailableEpiTypes = value;
				OnPropertyChanged("AvailableEpiTypes");
			}
		}
		
		private TypeEpi mSelectedEpiType;
		public TypeEpi SelectedEpiType
		{
			get { return mSelectedEpiType; }
			set {
				mSelectedEpiType = value;
				OnPropertyChanged("SelectedEpiType");
			}
		}

        #endregion

        #endregion

        #region Résultats de recherche
        private ObservableCollection<Article> mSearchResults;
        public ObservableCollection<Article> SearchResults
        {
            get { return mSearchResults; }
            set
            {
                mSearchResults = value;
                OnPropertyChanged("SearchResults");
            }
        }

        private Article mSelectedArticle;
		public Article SelectedArticle
		{
			get { return mSelectedArticle; }
			set {
				mSelectedArticle = value;
				OnPropertyChanged("SelectedArticle");
                OnSelectedArticleChanged();
			}
		}

        private string mSelectedArticlePicturePath;
        public string SelectedArticlePicturePath
        {
            get { return mSelectedArticlePicturePath; }
            set
            {
                mSelectedArticlePicturePath = value;
                OnPropertyChanged("SelectedArticlePicturePath");
            }
        }

		#endregion
		
		#endregion
	}
}