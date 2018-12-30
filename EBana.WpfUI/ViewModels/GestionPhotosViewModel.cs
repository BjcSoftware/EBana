using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EBana.Domain.Models;
using EBana.Services.Dialog;
using EBana.Domain.ArticlePictures;
using EBana.Domain.SearchEngine;
using System.Linq;

namespace EBana.WpfUI.ViewModels
{
    public class GestionPhotosViewModel : Notifier
    {
        private readonly IArticlePictureLocator articlePictureLocator;
        private readonly IArticlePictureUpdater articlePictureUpdater;
        private readonly IFileDialogService fileDialogService;
        private readonly IMessageBoxDialogService messageBoxService;
        private readonly IArticleSearchEngine searchEngine;

        public GestionPhotosViewModel(
            IArticlePictureLocator articlePictureLocator,
            IArticlePictureUpdater articlePictureUpdater,
            IFileDialogService fileDialogService, 
            IMessageBoxDialogService messageBoxService, 
            IArticleSearchEngine searchEngine)
        {
            if (articlePictureLocator == null)
                throw new ArgumentNullException("articlePictureLocator");
            if (articlePictureUpdater == null)
                throw new ArgumentNullException("articlePictureUpdater");
            if (fileDialogService == null)
                throw new ArgumentNullException("fileDialogService");
            if (messageBoxService == null)
                throw new ArgumentNullException("messageBoxService");
            if (searchEngine == null)
                throw new ArgumentNullException("searchEngine");

            this.articlePictureLocator = articlePictureLocator;
            this.articlePictureUpdater = articlePictureUpdater;
            this.fileDialogService = fileDialogService;
            this.messageBoxService = messageBoxService;
            this.searchEngine = searchEngine;

            SearchCommand = new RelayCommand(() => Search());
            SelectNewPictureFileCommand = new RelayCommand(() => SelectNewPictureFile());
            UpdateSelectedArticlePictureCommand = new RelayCommand(() => UpdateSelectedArticlePicture());
            OpenPictureFolderCommand = new RelayCommand(() => OpenPictureFolder());

            SearchResults = new ObservableCollection<Article>();
            SearchQuery = string.Empty;
            OnlySearchArticlesWithoutPicture = true;
        }

        /// <summary>
        /// Rechercher des articles selon les critères de recherche
        /// </summary>
        private void Search()
        {
            RefreshSearchResults();
            
            if(SearchResults.Count == 0) {
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
            IEnumerable<Article> foundArticles =
                searchEngine.SearchArticles(SearchQuery);

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
            return !articlePictureLocator.IsArticleHavingAPicture(article);
        }

        private void ResetSearchResultsTo(IEnumerable<Article> articles)
        {
            SearchResults = new ObservableCollection<Article>(articles);
        }

        private void NotifyUserThatNoResultFound()
        {
            messageBoxService.Show("Information",
                                "Aucun résultat ne correspond à vos critères de recherche.",
                                DialogButton.Ok);
        }

        /// <summary>
        /// Ouvrir le répertoire dans lequel les photos d'articles sont stockées
        /// </summary>
        private void OpenPictureFolder()
        {
            string pictureFolder = articlePictureLocator
                .GetPictureFolderLocation().OriginalString;

            fileDialogService.OpenFileBrowserInFolder(pictureFolder);
        }

        /// <summary>
        /// Sélectionner une nouvelle photo
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
            Uri newPictureLocation = new Uri(DisplayedArticlePicturePath);
            articlePictureUpdater
                .UpdatePictureOfArticle(SelectedArticle, newPictureLocation);

            // le cache des photos n'est plus valide car une photo vient d'être mise à jour
            articlePictureLocator.ClearPictureCache();

            // la sélection de l'utilisateur a été validée
            HasTheUserSelectedANewPicture = false;

            NotifyUserArticlePictureSuccessfullyUpdated(SelectedArticle);

            RefreshSearchResults();
        }

        private void NotifyUserArticlePictureSuccessfullyUpdated(Article articleWithNewPicture)
        {
            string articleRef = articleWithNewPicture.Ref;
            string articleLabel = articleWithNewPicture.Libelle;
            messageBoxService.Show("Succès",
               $"La photo de l'article {articleRef} ({articleLabel}) a été mise à jour.",
               DialogButton.Ok);
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

                // désélectionner le potentiel choix de photo du joueur
                HasTheUserSelectedANewPicture = false;
            }
        }

        private void UpdateDisplayedPictureFromArticle(Article article)
        {
            Uri updatedArticlePicturePath = articlePictureLocator
                .GetArticlePictureLocationOrDefault(article);

            DisplayedArticlePicturePath = updatedArticlePicturePath.OriginalString;
        }

        #region Définition des propriétés
        public ICommand SearchCommand { get; private set; }
        public ICommand SelectNewPictureFileCommand { get; private set; }
        public ICommand UpdateSelectedArticlePictureCommand { get; private set; }
        public ICommand OpenPictureFolderCommand { get; private set; }

        #region Propriétés avec Notifier

        private string mSearchQuery;
        public string SearchQuery
        {
            get {
                return mSearchQuery;
            }
            set {
                mSearchQuery = value;
                OnPropertyChanged("SearchQuery");
            }
        }

        private bool mOnlySearchArticlesWithoutPicture;
        public bool OnlySearchArticlesWithoutPicture
        {
            get {
                return mOnlySearchArticlesWithoutPicture;
            }
            set {
                mOnlySearchArticlesWithoutPicture = value;
                OnPropertyChanged("OnlySearchArticlesWithoutPicture");
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
				OnPropertyChanged("SearchResults");
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
				OnPropertyChanged("SelectedArticle");
                OnSelectedArticleChanged();
            }
		}

        private bool mHasTheUserSelectedANewPicture;
        public bool HasTheUserSelectedANewPicture
        {
            get {
                return mHasTheUserSelectedANewPicture;
            }
            set {
                mHasTheUserSelectedANewPicture = value;
                OnPropertyChanged("HasTheUserSelectedANewPicture");
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
				OnPropertyChanged("DisplayedArticlePicturePath");
			}
		}

        #endregion

        #endregion
    }
}
