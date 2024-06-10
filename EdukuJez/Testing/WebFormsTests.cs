using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;
using Assert = Xunit.Assert;

[TestClass]
public class WebFormsTests : IDisposable
{
    private readonly IWebDriver _driver;

    public WebFormsTests()
    {
        // Ustawienie ścieżki do ChromeDriver
        _driver = new ChromeDriver();
    }
    [TestMethod]
    [Fact]
    public void HomePage_ShouldContainWelcomeText()
    {
        // Arrange
        _driver.Navigate().GoToUrl("http://localhost:44309");

        // Act
        var element = _driver.FindElement(By.TagName("body"));

        // Assert
        Assert.Contains("Welcome", element.Text);
    }

    public void Dispose()
    {
        _driver.Quit();
        _driver.Dispose();
    }
}