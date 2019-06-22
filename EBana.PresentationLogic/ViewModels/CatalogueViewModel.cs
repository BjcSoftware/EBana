using System.Linq;
using System.Collections.ObjectModel;
using EBana.Domain.Models;
using System.Windows.Input;
using System;
using EBana.Domain.ArticlePictures;
using EBana.Domain.SearchEngine;
using EBana.PresentationLogic.Core.ViewModel;
using EBana.PresentationLogic.Core.Command;
using EBana.Domain.Commands;
using EBana.Services.Command;

namespace EBana.PresentationLogic.ViewModels
{
	public class CatalogueViewModel : Notifier
	{
		public ICommand SearchCommand { get; private set; }
		public ICommand ShowSelectedArticleFluCommand { get; private set; }
        public SearchSettings SearchSettings { get; private set; }

		private readonly IArticlePictureLocator articlePictureLocator;
        private readonly ICommandService<OpenWebPage> webBrowserService;
		private readonly ISearchSettingsProvider searchSettingsProvider;
		private readonly IArticleSearchEngine searchEngine;

		public CatalogueViewModel(
			IArticlePictureLocator articlePictureLocator,
			ICommandService<OpenWebPage> webBrowserService,
			ISearchSettingsProvider searchSettingsProvider,
			IArticleSearchEngine searchEngine)
		{
			if (articlePictureLocator == null)
				throw new ArgumentNullException(nameof(articlePictureLocator));
			if (webBrowserService == null)
				throw new ArgumentNullException(nameof(webBrowserService));
			if (searchSettingsProvider == null)
				throw new ArgumentNullException(nameof(searchSettingsProvider));
			if (searchEngine == null)
				throw new ArgumentNullException(nameof(searchEngine));

			this.articlePictureLocator = articlePictureLocator;
			this.webBrowserService = webBrowserService;
			this.searchSettingsProvider = searchSettingsProvider;
			this.searchEngine = searchEngine;

			SearchCommand = new RelayCommand(() => Search());
			ShowSelectedArticleFluCommand = new RelayCommand(() => ShowSelectedArticleFlu());
			
			InitPropertiesBoundToUI();
		}

		private void InitPropertiesBoundToUI()
		{
            SearchSettings = new SearchSettings();
			InitAvailableArticleTypesList();
			InitAvailableEpiTypesList();
			SearchResults = new ObservableCollection<Article>();
		}

		private void InitAvailableArticleTypesList()
		{
			var articlesTypes = searchSettingsProvider.GetArticleTypes();
			AvailableArticleTypes = new ObservableCollection<TypeArticle>(articlesTypes);
			SelectDefaultArticleType();
		}

		private void SelectDefaultArticleType()
		{
			if (AvailableArticleTypes.Any())
			{
				SearchSettings.ArticleTypeFilter = AvailableArticleTypes.First();
			}
		}

		private void InitAvailableEpiTypesList()
		{
			var epiTypes = searchSettingsProvider.GetEpiTypes();
			AvailableEpiTypes = new ObservableCollection<TypeEpi>(epiTypes);
			SelectDefaultEpiType();
		}

		private void SelectDefaultEpiType()
		{
			if (AvailableEpiTypes.Any())
			{
				SearchSettings.EpiTypeFilter = AvailableEpiTypes.First();
			}
		}

		/// <summary>
		/// Lancer une recherche à partir des paramètres de recherche actuels.
		/// Les résultats sont placés dans la liste SearchResults.
		/// </summary>
		private void Search()
		{
            var foundArticles = searchEngine.PerformSearch(SearchSettings);
            SearchResults = new ObservableCollection<Article>(foundArticles);
        }

		private void ShowSelectedArticleFlu()
		{
			string url = ((Banalise)SelectedArticle).LienFlu;
            webBrowserService.Execute(new OpenWebPage { Url = url });
		}

		private void OnSelectedArticleChanged()
		{
			articlePictureLocator.ClearPictureCache();
			UpdateDisplayedArticlePicture();
		}

		private void UpdateDisplayedArticlePicture()
		{
			SelectedArticlePicturePath = articlePictureLocator
				.GetArticlePictureLocationOrDefault(SelectedArticle).OriginalString;
		}

		private void OnAvailableEpiTypesChanged()
		{
			AreEpiAvailable = AvailableEpiTypes.Any();
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
		
		# endregion

		#region Paramètres EPIs
		private bool mAreEpiAvailable;
		public bool AreEpiAvailable
		{
			get { return mAreEpiAvailable; }
			set
			{
				mAreEpiAvailable = value;
				OnPropertyChanged("AreEpiAvailable");
			}
		}
		
		private ObservableCollection<TypeEpi> mAvailableEpiTypes;
		public ObservableCollection<TypeEpi> AvailableEpiTypes
		{
			get { return mAvailableEpiTypes; }
			set {
				mAvailableEpiTypes = value;
				OnPropertyChanged("AvailableEpiTypes");
				OnAvailableEpiTypesChanged();

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