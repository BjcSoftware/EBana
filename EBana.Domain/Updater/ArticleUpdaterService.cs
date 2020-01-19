using EBana.Domain.ArticleStorageUpdater;
using EBana.Domain.Updater.Exceptions;
using System;

namespace EBana.Domain.Updater
{
    public class ArticleUpdaterService :
        IArticleUpdaterService
    {
        private readonly IUpdateSourceValidator validator;
        private readonly IArticleProvider provider;
        private readonly IArticleStorageUpdater updater;

        public ArticleUpdaterService(
            IUpdateSourceValidator validator,
            IArticleProvider provider,
            IArticleStorageUpdater updater)
        {
            if (validator == null)
                throw new ArgumentNullException(nameof(validator));
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            if (updater == null)
                throw new ArgumentNullException(nameof(updater));

            this.validator = validator;
            this.provider = provider;
            this.updater = updater;
        }

        public void UpdateArticles(string updateSource)
        {
            if (updateSource == null)
                throw new ArgumentNullException(nameof(updateSource));

            Validate(updateSource);

            updater.ReplaceAvailableArticlesWith(
                provider.GetArticlesFrom(
                    updateSource));
        }

        private void Validate(string updateSource)
        {
            if (!validator.IsValid(updateSource))
            {
                throw new InvalidUpdateSourceException();
            }
        }
    }
}
