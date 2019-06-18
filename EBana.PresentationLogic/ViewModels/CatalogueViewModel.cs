﻿using System.Linq;
using System.Collections.ObjectModel;
using EBana.Domain.Models;
using System.Windows.Input;
using System.Collections.Generic;
using System;
using EBana.Services.Dialog;
using EBana.Services.Web;
using EBana.Domain.ArticlePictures;
using EBana.Domain.SearchEngine;
using EBana.PresentationLogic.Core.ViewModel;
using EBana.PresentationLogic.Core.Command;

namespace EBana.PresentationLogic.ViewModels
{
	public class CatalogueViewModel : Notifier
	{
		public ICommand SearchCommand { get; private set; }
		public ICommand ShowSelectedArticleFluCommand { get; private set; }
        public SearchSettings SearchSettings { get; private set; }

		private readonly IArticlePictureLocator articlePictureLocator;
		private readonly IMessageBoxDialogService messageBoxService;
		private readonly IWebBrowserService webBrowserService;
		private readonly ISearchSettingsProvider searchSettingsProvider;
		private readonly IArticleSearchEngine searchEngine;

		public CatalogueViewModel(
			IArticlePictureLocator articlePictureLocator,
			IMessageBoxDialogService messageBoxService, 
			IWebBrowserService webBrowserService,
			ISearchSettingsProvider searchSettingsProvider,
			IArticleSearchEngine searchEngine)
		{
			if (articlePictureLocator == null)
				throw new ArgumentNullException(nameof(articlePictureLocator));
			if (messageBoxService == null)
				throw new ArgumentNullException(nameof(messageBoxService));
			if (webBrowserService == null)
				throw new ArgumentNullException(nameof(webBrowserService));
			if (searchSettingsProvider == null)
				throw new ArgumentNullException(nameof(searchSettingsProvider));
			if (searchEngine == null)
				throw new ArgumentNullException(nameof(searchEngine));

			this.articlePictureLocator = articlePictureLocator;
			this.messageBoxService = messageBoxService;
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
			RefreshSearchResults();

			if (SearchResults.Count == 0)
			{
				NotifyUserThatNoResultFound();
			}
		}

		private void RefreshSearchResults()
		{
			var foundArticles = searchEngine.PerformSearch(SearchSettings);
			ResetSearchResultsTo(foundArticles);
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