using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaCenterTestsExamPrep.pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace IdeaCenterTestsExamPrep.Tests
{
    public class BaseTest
    {
        public IWebDriver driver;
        public LoginPage loginPage;
        public CreateIdeaPage createIdeaPage;
        public MyIdeasPage myIdeasPage;
        public IdeasReadPage ideasReadPage;
        public IdeasEditPage ideasEditPage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        
            loginPage = new LoginPage(driver);
            createIdeaPage = new CreateIdeaPage(driver);
            myIdeasPage = new MyIdeasPage(driver);
            ideasReadPage = new IdeasReadPage(driver);
            ideasEditPage = new IdeasEditPage(driver);
            loginPage.OpenPage();
            loginPage.Login("BabaVida1001@abv.bg", "StigaMi");
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
        public string GenerateRandomString(int length)
        {
            const string chars = "abcdefg";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
