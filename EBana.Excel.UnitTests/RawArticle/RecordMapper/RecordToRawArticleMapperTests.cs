using NUnit.Framework;
using System;
using System.Collections.Generic;
using EBana.Domain.Models;
using EBana.Excel.Core;

namespace EBana.Excel.UnitTests
{
    [TestFixture]
    class RecordToRawArticleMapperTests
    {
        [Test]
        public void Constructor_NullMappingPassed_Throws()
        {
            ArticleFieldToRecordFieldMapping nullMapping = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => new RecordToRawArticleMapper(nullMapping));
        }

        [Test]
        public void Map_NullRecordPassed_Throws()
        {
            var mapper = CreateMapper();
            Record nullRecord = null;

            var exception = Assert.Catch<ArgumentNullException>(
                () => mapper.Map(nullRecord));
        }

        [Test]
        public void Map_CorrectRecordPassed_ReturnsCorrectRawArticle()
        {
            // Arrange
            var record = new Record(
                new List<string>
                {
                    "Ref",
                    "Libelle",
                    "Division",
                    "10",
                    "Localisation",
                    "10",
                    "Flu",
                    "Infos",
                    "X",
                    "Casque"
                }
            );

            RawArticle expectedArticle = new RawArticle
            {
                Ref = "REF",
                Libelle = "Libelle",
                Localisation = "Localisation",
                Quantite = 10,
                IdMagasin = 10,
                LienFlu = "Flu",
                InfosSupplementaires = "Infos",
                TypeEpi = new TypeEpi("Casque")
            };

            var mapper = CreateMapper();

            // Act
            RawArticle actualArticle = mapper.Map(record);


            // Assert
            Assert.AreEqual(expectedArticle, actualArticle);
        }

        private RecordToRawArticleMapper CreateMapper()
        {
            return new RecordToRawArticleMapper(
                new ArticleFieldToRecordFieldMapping());
        }
    }
}
