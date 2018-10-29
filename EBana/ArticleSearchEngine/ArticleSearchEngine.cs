using Data.Repository;
using EBana.Models;
using System.Collections.Generic;

namespace EBana
{
    public class ArticleSearchEngine
    {
        private readonly IReader<Banalise> banaliseReader;
        private readonly IReader<EPI> epiReader;
        private readonly IReader<SEL> selReader;

        public ArticleSearchEngine(
            IReader<Banalise> banaliseReader,
            IReader<EPI> epiReader,
            IReader<SEL> selReader)
        {
            this.banaliseReader = banaliseReader;
            this.epiReader = epiReader;
            this.selReader = selReader;
        }

        public IEnumerable<Article> SearchBanaliseByRef(string reference)
        {
            reference = reference.ToLower();
            return banaliseReader.Find(a => a.Ref.ToLower().Contains(reference));
        }

        public IEnumerable<Article> SearchEpiByRef(string reference, TypeEpi type)
        {
            reference = reference.ToLower();
            return epiReader.Find(a => a.Ref.ToLower().Contains(reference) && a.TypeEpi.Libelle == type.Libelle);
        }

        public IEnumerable<Article> SearchSelByRef(string reference)
        {
            reference = reference.ToLower();
            return selReader.Find(a => a.Ref.ToLower().Contains(reference));
        }


        public IEnumerable<Article> SearchBanaliseByLabel(string label)
        {
            label = label.ToLower();
            return banaliseReader.Find(a => a.Libelle.ToLower().Contains(label));
        }

        public IEnumerable<Article> SearchEpiByLabel(string label, TypeEpi type)
        {
            label = label.ToLower();
            return epiReader.Find(a => a.Libelle.ToLower().Contains(label) && a.TypeEpi.Libelle == type.Libelle);
        }

        public IEnumerable<Article> SearchSelByLabel(string label)
        {
            label = label.ToLower();
            return selReader.Find(a => a.Libelle.ToLower().Contains(label));
        }
    }
}
