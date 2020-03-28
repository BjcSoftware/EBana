using EBana.Domain.Models;
using EBana.Domain.SearchEngine;
using EBana.EfDataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EBana.EfDataAccess
{
    public class SearchCriteriaProvider : ISearchSettingsProvider
    {
        private readonly IReader<Epi> epiReader;

        public SearchCriteriaProvider(
            IReader<Epi> epiReader)
        {
            if (epiReader == null)
                throw new ArgumentNullException(nameof(epiReader));

            this.epiReader = epiReader;
        }

        public IEnumerable<string> GetArticleTypes()
        {
            return new List<string>() { "Banalisé", "SEL" };
        }

        public IEnumerable<TypeEpi> GetEpiTypes()
        {
            return epiReader
                .GetAll()
                .Select(e => e.TypeEpi)
                .Distinct();
        }
    }
}
