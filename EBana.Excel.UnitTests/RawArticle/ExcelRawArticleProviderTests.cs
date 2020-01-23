using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System;
using EBana.Domain.Models;
using EBana.Excel.Core;

namespace EBana.Excel.UnitTests
{
    [TestFixture]
    public class ExcelRawArticleProviderTests
    {
        [Test]
        public void GetRawArticles_FileContainingTwoRows_ReturnsTwoCorrectRawArticles()
        {
            ExcelRawArticleProvider rawArticleProvider = 
                CreateExcelRawArticleProvider(
                    GetRawArticleDataForTwoArticles());

            var actualRawArticles = rawArticleProvider.GetRawArticlesFrom("articles.xls");
            
            var expectedRawArticles = GetExpectedRawArticlesForTwoArticles();
            Assert.AreEqual(
                expectedRawArticles,
                actualRawArticles);
        }

        [Test]
        public void GetRawArticles_EmptyFile_ReturnsEmptyList()
        {
            string[,] emptyFileData = GetEmptyFileData();
            ExcelRawArticleProvider rawArticleProvider =
                CreateExcelRawArticleProvider(emptyFileData);

            var rawArticles = rawArticleProvider.GetRawArticlesFrom("empty.xls");

            Assert.AreEqual(
                expected: 0,
                actual: rawArticles.Count);
        }

        
        [Test]
        public void Constructor_NullMapperPassed_Throws()
        {
            IRecordToRawArticleMapper nullMapper = null;

            var stubExcelFileFactory = Substitute.For<IExcelFileFactory>();
            var exception = Assert.Catch<ArgumentNullException>(
                () => new ExcelRawArticleProvider(nullMapper, stubExcelFileFactory));
        }

        [Test]
        public void Constructor_NullExcelFileFactoryPassed_Throws()
        {
            IExcelFileFactory nullFactory = null;
            
            var stubMapper = Substitute.For<IRecordToRawArticleMapper>();
            var exception = Assert.Catch<ArgumentNullException>(
                () => new ExcelRawArticleProvider(stubMapper, nullFactory));
        }

        [Test]
        public void GetRawArticles_NullSourcePassed_Throws()
        {
            ExcelRawArticleProvider rawArticleProvider = 
                CreateExcelRawArticleProvider();

            var exception = Assert.Catch<ArgumentNullException>(
                () => rawArticleProvider.GetRawArticlesFrom(null));
        }

        private List<RawArticle> GetExpectedRawArticlesForTwoArticles()
        {
            return new List<RawArticle>
            {
                new RawArticle() {
                    IdArticle = 0,
                    Ref = "REF1",
                    Libelle = "Lab1",
                    Localisation = "Loc1",
                    Quantite = 1,
                    IdMagasin = 1,
                    InfosSupplementaires = "Infos1",
                    LienFlu = "Flu1",
                    TypeEpi = new TypeEpi() { Libelle = "Epitype1" }
                },
                new RawArticle() {
                    IdArticle = 0,
                    Ref = "REF2",
                    Libelle = "Lab2",
                    Localisation = "Loc2",
                    Quantite = 2,
                    IdMagasin = 2,
                    InfosSupplementaires = "Infos2",
                    LienFlu = "Flu2",
                    TypeEpi = new TypeEpi() { Libelle = "Epitype2" }
                }
            };
        }

        private ExcelRawArticleProvider CreateExcelRawArticleProvider()
        {
            return CreateExcelRawArticleProvider(GetEmptyFileData());
        }

        private ExcelRawArticleProvider CreateExcelRawArticleProvider(string[,] rawArticlesData)
        {
            IExcelFileReader stubExcelFile = CreateStubExcelFile(rawArticlesData);
            var excelFileFactoryStub = Substitute.For<IExcelFileFactory>();
            excelFileFactoryStub
                .CreateExcelFile(Arg.Any<string>())
                .Returns(stubExcelFile);

            return new ExcelRawArticleProvider(
                new RecordToRawArticleMapper(
                    new ArticleFieldToRecordFieldMapping()),
                excelFileFactoryStub);
        }

        private IExcelFileReader CreateStubExcelFile(string[,] data)
        {
            var stubExcelFile = Substitute.For<IExcelFileReader>();
            stubExcelFile
                .GetCellsAsStringInRange(Arg.Any<RectangularRange>())
                .Returns(data);

            stubExcelFile
                .RowCount
                .Returns((uint)(data.GetLength(0) + 1));

            return stubExcelFile;
        }

        private string[,] GetRawArticleDataForTwoArticles()
        {
            return new string[2, 10] {
                { "Ref1", "Lab1", "Div1", "1", "Loc1", "1", "Flu1", "Infos1", "EpiId1", "EpiType1" },
                { "Ref2", "Lab2", "Div2", "2", "Loc2", "2", "Flu2", "Infos2", "EpiId2", "EpiType2" }
            };
        }

        private string[,] GetEmptyFileData()
        {
            return new string[,] { };
        }
    }
}
