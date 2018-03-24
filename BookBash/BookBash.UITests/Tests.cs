using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace BookBash.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        private IApp _app;
        private readonly Platform _platform;

        private static readonly Func<AppQuery, AppQuery> HeaderText = c => c.Marked("HeaderText").Text("Account Create");
        private static readonly Func<AppQuery, AppQuery> CreateAccountButton = c => c.Marked("CreateAccountButton");
        private static readonly Func<AppQuery, AppQuery> ErrorMessage = c => c.Marked("ErrorMessage").Text("User name is required");


        public Tests(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [Test]
        public void AppLaunches()
        {
            _app.Repl();
            // Arrange - Nothing to do because the queries have already been initialized.
            var result = _app.Query(HeaderText);
            Assert.IsTrue(result.Any(), "The header message string isn't correct - maybe the app wasn't re-started?");

            // Act
            _app.Tap(CreateAccountButton);

            // Assert
            result = _app.Query(ErrorMessage);
            Assert.IsTrue(result.Any(), "The 'error message' message is not being displayed or is incorrect.");
        }
    }
}

