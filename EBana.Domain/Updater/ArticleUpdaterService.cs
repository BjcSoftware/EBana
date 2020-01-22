using EBana.Domain.ArticleStorageUpdater;
using EBana.Domain.Commands;
using EBana.Domain.Updater.Exceptions;
using System;

namespace EBana.Domain.Updater
{
    public class ArticleUpdaterService :
        ICommandService<UpdateArticles>
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

        public void Execute(UpdateArticles command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            Validate(command.UpdateSource);

            updater.ReplaceAvailableArticlesWith(
                provider.GetArticlesFrom(
                    command.UpdateSource));
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
