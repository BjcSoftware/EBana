using NUnit.Framework;
using NSubstitute;
using System;
using EBana.Domain.Models;
using EBana.Services.Dialog;
using EBana.DesktopAppServices.ArticlePictures.EventHandlers;
using EBana.Domain.ArticlePictures.Events;

namespace EBana.DesktopAppServicesTests.ArticlePictures.EventHandlers.UnitTests
{
    [TestFixture]
    public class ArticlePictureUpdatedUserNotifierTests
    {
        [Test]
        public void Constructor_NullDialogServicePassed_Throws()
        {
            IMessageBoxDialogService nullDialogService = null;

            Assert.Catch<ArgumentNullException>(() =>
                new ArticlePictureUpdatedUserNotifier(
                    nullDialogService));
        }

        [Test]
        public void Handle_NullEventPassed_Throws()
        {
            var stubDialogService = Substitute.For<IMessageBoxDialogService>();
            var notifier = new ArticlePictureUpdatedUserNotifier(
                stubDialogService);
            ArticlePictureUpdated nullEvent = null;

            Assert.Catch<ArgumentNullException>(() =>
             notifier.Handle(nullEvent));
        }

        [Test]
        public void Handle_Always_NotifiesUser()
        {
            var stubDialogService = Substitute.For<IMessageBoxDialogService>();
            var notifier = new ArticlePictureUpdatedUserNotifier(
                stubDialogService);

            notifier.Handle(new ArticlePictureUpdated(new Article()));

            stubDialogService.Received().Show(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<DialogButton>());
        }
    }
}
