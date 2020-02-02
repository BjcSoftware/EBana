using EBana.Domain.Models;
using EBana.Excel.Core;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace EBana.Excel.UnitTests
{
    [TestFixture]
    public class RecordToRawArticleMapperWithEpiCachingTests
    {
        [Test]
        public void Constructor_NullDecoratedMapperPassed_Throws()
        {
            IRecordToRawArticleMapper nullDecoratedMapper = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new RecordToRawArticleMapperWithEpiCaching(nullDecoratedMapper));
        }

        [Test]
        public void Map_NullRecordPassed_Throws()
        {
            Record nullRecord = null;
            var mapper = CreateMapper();

            var exception = Assert.Catch<ArgumentNullException>(
                () => mapper.Map(nullRecord));
        }

        private static RecordToRawArticleMapperWithEpiCaching CreateMapper()
        {
            var stubDecoratedMapper = Substitute.For<IRecordToRawArticleMapper>();
            return new RecordToRawArticleMapperWithEpiCaching(stubDecoratedMapper);
        }

        [Test]
        public void Map_WhenPassingTwoRecordsWithSameEpiLabel_ReturnsTwoRawArticlesWithSameEpiTypeInstance()
        {
            // Arrange
            var stubDecoratedMapper = Substitute.For<IRecordToRawArticleMapper>();
            stubDecoratedMapper
                .Map(Arg.Is(FirstRecord))
                .Returns(FirstRawArticle);

            stubDecoratedMapper
                .Map(Arg.Is(SecondRecord))
                .Returns(SecondRawArticle);

            var mapper = new RecordToRawArticleMapperWithEpiCaching(stubDecoratedMapper);

            // Act
            var firstArticle = mapper.Map(FirstRecord);
            var secondArticle = mapper.Map(SecondRecord);

            // Assert
            Assert.AreSame(
                firstArticle.TypeEpi,
                secondArticle.TypeEpi);
        }

        private readonly Record FirstRecord = new Record(new List<string>());
        private readonly Record SecondRecord = new Record(new List<string>());

        private readonly RawArticle FirstRawArticle = new RawArticle
        {
            TypeEpi = new TypeEpi { Libelle = "Casque" }
        };

        private readonly RawArticle SecondRawArticle = new RawArticle
        {
            TypeEpi = new TypeEpi { Libelle = "Casque" }
        };
    }
}