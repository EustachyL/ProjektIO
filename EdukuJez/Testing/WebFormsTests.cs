using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using Assert = Xunit.Assert;

namespace Testing
{
    public class WebFormsTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public WebFormsTests()
        {
            // Ustawienie ścieżki do ChromeDriver
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("ignore-certificate-errors");
            _driver = new ChromeDriver(options);
        }
        [TestMethod]
        [Fact]
        public void HomePage_ShouldContainWelcomeText()
        {
            // Arrange
            _driver.Navigate().GoToUrl("https://localhost:44309");

            // Act
            var element = _driver.FindElement(By.TagName("body"));

            // Assert
            Assert.Contains("Zaloguj", element.Text);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}