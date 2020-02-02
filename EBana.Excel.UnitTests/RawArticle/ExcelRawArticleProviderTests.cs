using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System;
using EBana.Domain.Models;
using EBana.Excel.Core;
using System.Linq;

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
            List<Record> emptyFileData = GetEmptyFileData();
            ExcelRawArticleProvider rawArticleProvider =
                CreateExcelRawArticleProvider(emptyFileData);

            var rawArticles = rawArticleProvider.GetRawArticlesFrom("empty.xls").ToList();

            Assert.AreEqual(
                expected: 0,
                actual: rawArticles.Count);
        }

        
        [Test]
        public void Constructor_NullMapperPassed_Throws()
        {
            IRecordToRawArticleMapper nullMapper = null;

            var stubRecordReader = Substitute.For<IExcelRecordReader>();
            var exception = Assert.Catch<ArgumentNullException>(
                () => new ExcelRawArticleProvider(nullMapper, stubRecordReader));
        }

        [Test]
        public void Constructor_NullExcelFileFactoryPassed_Throws()
        {
            IExcelRecordReader nullReader = null;
            
            var stubMapper = Substitute.For<IRecordToRawArticleMapper>();
            var exception = Assert.Catch<ArgumentNullException>(
                () => new ExcelRawArticleProvider(stubMapper, nullReader));
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

        private ExcelRawArticleProvider CreateExcelRawArticleProvider(List<Record> records)
        {
            var stubReader = Substitute.For<IExcelRecordReader>();
            stubReader
                .ReadAllRecordsFrom(Arg.Any<ExcelSource>())
                .Returns(records);

            return new ExcelRawArticleProvider(
                new RecordToRawArticleMapper(
                    new ArticleFieldToRecordFieldMapping()),
                stubReader);
        }

        private List<Record> GetRawArticleDataForTwoArticles()
        {
            return new List<Record> {
                new Record(new List<string> { "Ref1", "Lab1", "Div1", "1", "Loc1", "1", "Flu1", "Infos1", "EpiId1", "EpiType1" }),
                new Record(new List<string> { "Ref2", "Lab2", "Div2", "2", "Loc2", "2", "Flu2", "Infos2", "EpiId2", "EpiType2" })
            };
        }

        private List<Record> GetEmptyFileData()
        {
            return new List<Record>();
        }
    }
}
