using System.Linq;
using System.Collections.ObjectModel;
using EBana.Domain.Models;
using System.Windows.Input;
using System.Collections.Generic;
using System;
using EBana.Services.Dialog;
using EBana.Services.Web;
using EBana.Domain.ArticlePictures;
using EBana.Domain.SearchEngine;
using EBana.WpfUI.Core.ViewModel;
using EBana.WpfUI.Core.Command;

namespace EBana.WpfUI.ViewModels
{
	public class CatalogueViewModel : Notifier
	{
		public ICommand SearchCommand { get; private set; }
		public ICommand ShowSelectedArticleFluCommand { get; private set; }

		private readonly IArticlePictureLocator articlePictureLocator;
		private readonly IMessageBoxDialogService messageBoxService;
		private readonly IWebBrowserService webBrowserService;
		private readonly ISearchSettingsProvider searchSettingsProvider;
		private readonly IArticleSearchEngine articleSearchEngine;

		public CatalogueViewModel(
			IArticlePictureLocator articlePictureLocator,
			IMessageBoxDialogService messageBoxService, 
			IWebBrowserService webBrowserService,
			ISearchSettingsProvider searchSettingsProvider,
			IArticleSearchEngine articleSearchEngine)
		{
			if (articlePictureLocator == null)
				throw new ArgumentNullException(nameof(articlePictureLocator));
			if (messageBoxService == null)
				throw new ArgumentNullException(nameof(messageBoxService));
			if (webBrowserService == null)
				throw new ArgumentNullException(nameof(webBrowserService));
			if (searchSettingsProvider == null)
				throw new ArgumentNullException(nameof(searchSettingsProvider));
			if (articleSearchEngine == null)
				throw new ArgumentNullException(nameof(articleSearchEngine));

			this.articlePictureLocator = articlePictureLocator;
			this.messageBoxService = messageBoxService;
			this.webBrowserService = webBrowserService;
			this.searchSettingsProvider = searchSettingsProvider;
			this.articleSearchEngine = articleSearchEngine;

			SearchCommand = new RelayCommand(() => Search());
			ShowSelectedArticleFluCommand = new RelayCommand(() => ShowSelectedArticleFlu());
			
			InitPropertiesBoundToUI();
		}

		private void InitPropertiesBoundToUI()
		{
			SearchQuery = string.Empty;
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
				SelectedArticleType = AvailableArticleTypes.First();
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
				SelectedEpiType = AvailableEpiTypes.First();
			}
		}

		/// <summary>
		/// Lancer une recherche à partir des paramètres de recherche actuels.
		/// Les résultats sont placés dans la liste SearchResults.
		/// </summary>
		private void Search()
		{
			RefreshSearchResults();

			if (SearchResults.Count == 0)
			{
				NotifyUserThatNoResultFound();
			}
		}

		private void RefreshSearchResults()
		{
			IEnumerable<Article> foundArticles = PerformSearch();
			ResetSearchResultsTo(foundArticles);
		}

		private IEnumerable<Article> PerformSearch()
		{
			IEnumerable<Article> foundArticles;
			if(IsSearchedArticleSel())
			{
				foundArticles = articleSearchEngine
					.SearchSel(SearchQuery);
			}
			else if (IsSearchedArticleEpi())
			{
				foundArticles = articleSearchEngine
					.SearchEpi(SearchQuery, SelectedEpiType);
			}
			else
			{
				foundArticles = articleSearchEngine
					.SearchBanalise(SearchQuery);
			}

			return foundArticles;
		}

		private bool IsSearchedArticleSel()
		{
			return SelectedArticleType.Libelle == "SEL";
		}

		private bool IsSearchedArticleEpi()
		{
			return IsSearchedArticleEPI;
		}

		private void ResetSearchResultsTo(IEnumerable<Article> articles)
		{
			SearchResults = new ObservableCollection<Article>(articles);
		}

		private void NotifyUserThatNoResultFound()
		{
			messageBoxService.Show(
				"Information",
				"Aucun résultat ne correspond à vos critères de recherche.",
				DialogButton.Ok);
		}

		private void ShowSelectedArticleFlu()
		{
			string url = ((Banalise)SelectedArticle).LienFlu;
			webBrowserService.Open(url);
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

		#endregion

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
				OnAvailableEpiTypesChanged();

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