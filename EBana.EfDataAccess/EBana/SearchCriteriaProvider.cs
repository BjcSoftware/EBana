using EBana.Domain.Models;
using EBana.Domain.SearchEngine;
using EBana.EfDataAccess.Repository;
using System;
using System.Collections.Generic;

namespace EBana.EfDataAccess
{
    public class SearchCriteriaProvider : ISearchSettingsProvider
    {
        private readonly IReader<TypeArticle> typeArticleReader;
        private readonly IReader<TypeEpi> typeEpiReader;

        public SearchCriteriaProvider(
            IReader<TypeArticle> typeArticleReader,
            IReader<TypeEpi> typeEpiReader)
        {
            if (typeArticleReader == null)
                throw new ArgumentNullException("typeArticleReader");
            if (typeEpiReader == null)
                throw new ArgumentNullException("typeEpiReader");

            this.typeArticleReader = typeArticleReader;
            this.typeEpiReader = typeEpiReader;
        }

        public IEnumerable<TypeArticle> GetArticleTypes()
        {
            return typeArticleReader.GetAll();
        }

        public IEnumerable<TypeEpi> GetEpiTypes()
        {
            return typeEpiReader.GetAll();
        }
    }
}
