using NUnit.Framework;

namespace EBana.Domain.SearchEngine.UnitTests
{
    [TestFixture]
    public class SearchSettingsTests
    {
        [Test]
        public void Constructor_ByDefault_EmptyQuery()
        {
            SearchSettings settings = new SearchSettings();

            Assert.IsEmpty(settings.Query);
        }
    }
}
