﻿using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EBana.Domain.Models;
using EBana.Services.Dialog;
using EBana.Domain.ArticlePictures;
using EBana.Domain.SearchEngine;
using System.Linq;
using EBana.PresentationLogic.Core.ViewModel;
using EBana.PresentationLogic.Core.Command;

namespace EBana.PresentationLogic.ViewModels
{
	public class GestionPhotosViewModel : Notifier
	{
		private readonly IArticlePictureLocator locator;
		private readonly IArticlePictureUpdater updater;
		private readonly IFileDialogService fileDialogService;
		private readonly IArticleSearchEngine searchEngine;

        public SearchSettings SearchSettings { get; private set; }

		public GestionPhotosViewModel(
			IArticlePictureLocator locator,
			IArticlePictureUpdater updater,
			IFileDialogService fileDialogService,
			IArticleSearchEngine searchEngine)
        {
            if (locator == null)
                throw new ArgumentNullException(nameof(locator));
            if (updater == null)
                throw new ArgumentNullException(nameof(updater));
            if (fileDialogService == null)
                throw new ArgumentNullException(nameof(fileDialogService));
            if (searchEngine == null)
                throw new ArgumentNullException(nameof(searchEngine));

            this.locator = locator;
            this.updater = updater;
            this.fileDialogService = fileDialogService;
            this.searchEngine = searchEngine;

            SearchCommand = new RelayCommand(() => Search());
            SelectNewPictureFileCommand = new RelayCommand(() => SelectNewPictureFile());
            UpdateSelectedArticlePictureCommand = new RelayCommand(() => UpdateSelectedArticlePicture());
            OpenPictureFolderCommand = new RelayCommand(() => OpenPictureFolder());

            InitSearchSettings();

            SearchResults = new ObservableCollection<Article>();
            OnlySearchArticlesWithoutPicture = true;
        }

        private void InitSearchSettings()
        {
            SearchSettings = new SearchSettings();

            // ne pas filtrer sur le type d'article
            SearchSettings.ArticleTypeFilter = null;
        }

        /// <summary>
        /// Rechercher des articles selon les critères de recherche
        /// </summary>
        private void Search()
		{
			RefreshSearchResults();
		}

		private void RefreshSearchResults()
		{
			IEnumerable<Article> foundArticles = PerformSearch();
			ResetSearchResultsTo(foundArticles);
		}

		private IEnumerable<Article> PerformSearch()
		{
			var foundArticles = searchEngine.PerformSearch(SearchSettings);

			if (OnlySearchArticlesWithoutPicture)
			{
				foundArticles = ExtractArticlesWithoutPictureFrom(foundArticles);
			}

			return foundArticles;
		}

		private IEnumerable<Article> ExtractArticlesWithoutPictureFrom(
			IEnumerable<Article> articles)
		{
			var articlesWithoutPicture = 
				from article in articles
				where IsArticleNotHavingAPicture(article)
				select article;

			return articlesWithoutPicture;
		}

		private bool IsArticleNotHavingAPicture(Article article)
		{
			return !locator.IsArticleHavingAPicture(article);
		}

		private void ResetSearchResultsTo(IEnumerable<Article> articles)
		{
			SearchResults = new ObservableCollection<Article>(articles);
		}

		/// <summary>
		/// Ouvrir le répertoire dans lequel les photos d'articles sont stockées
		/// </summary>
		private void OpenPictureFolder()
		{
			string pictureFolder = locator
				.PictureFolderLocation.OriginalString;

			fileDialogService.OpenFileBrowserInFolder(pictureFolder);
		}

		/// <summary>
		/// Sélectionner une nouvelle photo pour l'article sélectionné
		/// </summary>
		private void SelectNewPictureFile()
		{
			string newPicturePath = fileDialogService.OpenFileSelectionDialog();
			if (newPicturePath != null)
			{
				HasTheUserSelectedANewPicture = true;
				DisplayedArticlePicturePath = newPicturePath;
			}
		}

		private void UpdateSelectedArticlePicture()
		{
			string newPictureLocation = DisplayedArticlePicturePath;
			updater.UpdatePictureOfArticle(
                SelectedArticle, 
                newPictureLocation);

			// le cache des photos n'est plus valide car une photo vient d'être mise à jour
			locator.ClearPictureCache();

			// la sélection de l'utilisateur a été validée
			HasTheUserSelectedANewPicture = false;

			RefreshSearchResults();
		}

		private void OnOnlySearchArticlesWithoutPictureChanged()
		{
			// actualiser les résultats de recherche
			Search();
		}

		private void OnSelectedArticleChanged()
		{
			if(SelectedArticle != null)
			{
				UpdateDisplayedPictureFromArticle(SelectedArticle);

				// désélectionner le potentiel choix de photo de l'utilisateur
				HasTheUserSelectedANewPicture = false;
			}
		}

		private void UpdateDisplayedPictureFromArticle(Article article)
		{
			Uri updatedArticlePicturePath = locator
				.GetArticlePictureLocationOrDefault(article);

			DisplayedArticlePicturePath = updatedArticlePicturePath.OriginalString;
		}

		#region Définition des propriétés
		public ICommand SearchCommand { get; private set; }
		public ICommand SelectNewPictureFileCommand { get; private set; }
		public ICommand UpdateSelectedArticlePictureCommand { get; private set; }
		public ICommand OpenPictureFolderCommand { get; private set; }

		#region Propriétés avec Notifier

		private bool mOnlySearchArticlesWithoutPicture;
		public bool OnlySearchArticlesWithoutPicture
		{
			get {
				return mOnlySearchArticlesWithoutPicture;
			}
			set {
				mOnlySearchArticlesWithoutPicture = value;
				OnPropertyChanged(nameof(OnlySearchArticlesWithoutPicture));
				OnOnlySearchArticlesWithoutPictureChanged();
			}
		}

		private ObservableCollection<Article> mSearchResults;
		public ObservableCollection<Article> SearchResults
		{
			get {
				return mSearchResults;
			}
			set {
				mSearchResults = value;
				OnPropertyChanged(nameof(SearchResults));
			}
		}
		
		private Article mSelectedArticle;
		public Article SelectedArticle
		{
			get {
				return mSelectedArticle;
			}
			set {
				mSelectedArticle = value;
				OnPropertyChanged(nameof(SelectedArticle));
				OnSelectedArticleChanged();
			}
		}

		// propriété utilisée dans le xaml de la vue associée
		private bool mHasTheUserSelectedANewPicture;
		public bool HasTheUserSelectedANewPicture
		{
			get {
				return mHasTheUserSelectedANewPicture;
			}
			set {
				mHasTheUserSelectedANewPicture = value;
				OnPropertyChanged(nameof(HasTheUserSelectedANewPicture));
			}
		}

		private string mDisplayedArticlePicturePath;
		public string DisplayedArticlePicturePath
		{
			get {
				return mDisplayedArticlePicturePath;
			}
			set {
				mDisplayedArticlePicturePath = value;
				OnPropertyChanged(nameof(DisplayedArticlePicturePath));
			}
		}

		#endregion

		#endregion
	}
}
