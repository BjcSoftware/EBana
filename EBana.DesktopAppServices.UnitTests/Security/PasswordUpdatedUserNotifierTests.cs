using EBana.DesktopAppServices.Security.EventHandlers;
using EBana.Services.Dialog;
using NUnit.Framework;
using System;

namespace EBana.DesktopAppServicesTests.Security.UnitTests
{
    [TestFixture]
    public class PasswordUpdatedUserNotifierTests
    {
        [Test]
        public void Constructor_NullDialogServicePassed_Throws()
        {
            IMessageBoxDialogService nullDialogService = null;

            Assert.Catch<ArgumentNullException>(() =>
                new PasswordUpdatedUserNotifier(nullDialogService));
        }
    }
}