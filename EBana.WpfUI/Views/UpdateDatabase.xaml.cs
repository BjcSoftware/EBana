using EBana.Domain.Models;
using System.Data.Entity;
using System.Windows.Controls;
using EBana.Excel;
using EBana.WpfUI.ViewModels;
using EBana.WindowsServices.Dialog;
using EBana.Services.Dialog;
using EBana.EfDataAccess;
using EBana.Domain;
using EBana.EfDataAccess.Repository;
using EBana.Security.Hash;

namespace EBana.WpfUI.Views
{
	public partial class UpdateDatabase : Page
	{
		public UpdateDatabase()
		{
			InitializeComponent();
			DataContext = CreateViewModel();
		}

		private UpdateArticlesViewModel CreateViewModel()
		{
			return new UpdateArticlesViewModel(
				CreateFileDialogService(),
				new MessageBoxDialogService(),
				CreateUpdater(),
				CreateArticleProvider());
		}

		private IFileDialogService CreateFileDialogService()
		{
			return new WindowsFileDialogService
			{
				Filter = "Fichiers Excel (*.xlsx;*.xls)|*xlsx;*xls|Tous les fichiers (*.*)|*.*"
			};
		}

		private IArticleStorageUpdater CreateUpdater()
		{
			DbContext context = CreateDbContext();

			IWriter<Article> articleWriter = 
				new EfWriter<Article>(context);
			IWriter<TypeEpi> typeEpiWriter = 
				new EfWriter<TypeEpi>(context);

			return new ArticleStorageUpdater(
				new ArticleRepository(
					articleWriter,
					typeEpiWriter));
		}

		private DbContext CreateDbContext()
		{
			return new EBanaContext(
				new BCryptHash());
		}

		private IArticleProvider CreateArticleProvider()
		{
			return new ExcelArticleProvider(
				CreateRawArticleProvider(),
				CreateRawArticleToArticleMapper());
		}

		private IRawArticleProvider CreateRawArticleProvider()
		{
			return new ExcelRawArticleProvider(
				new RecordToRawArticleMapperWithEpiCaching(
					new RecordToRawArticleMapper(
						new ArticleFieldToRecordFieldMapping())),
				new ExcelFileFactory());
		}

		private IRawArticleToArticleMapper CreateRawArticleToArticleMapper()
		{
			return new RawArticleToArticleMapper(
				new ArticleSettings());
		}
	}
}