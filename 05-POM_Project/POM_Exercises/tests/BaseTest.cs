using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using POM_Exercises.pages;

namespace POM_Exercises.tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected LoginPage loginPage;
        protected InventoryPage inventoryPage;
        protected CartPage cartPage;
        protected CheckoutPage checkoutPage;
        protected HiddenMenuPage hiddenMenuPage;

        [SetUp]
        public void SetUp()

        {
            var chromeOptions = new ChromeOptions(); 
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false); 
            driver = new ChromeDriver(chromeOptions); 
            driver.Manage().Window.Maximize(); 
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); 
                
            /*var chromeOptions = new ChromeOptions();

            //
            // Disable Chrome popups, password warnings, first-run dialogs, notifications, etc.
            //
            chromeOptions.AddArgument("--incognito");
            chromeOptions.AddArgument("--disable-search-engine-choice-screen");
            chromeOptions.AddArgument("--no-default-browser-check");
            chromeOptions.AddArgument("--no-first-run");
            chromeOptions.AddArgument("--disable-infobars");
            chromeOptions.AddArgument("--disable-notifications");

            // Disable password manager + password leak checks
            chromeOptions.AddArgument("--disable-features=PasswordManagerOnboarding,PasswordLeakDetection");
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);

            // Optional: clean Chrome profile (highest reliability)
            chromeOptions.AddArgument($"--user-data-dir={Path.GetTempPath()}ChromeTestProfile");

            driver = new ChromeDriver(chromeOptions);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);*/

            loginPage = new LoginPage(driver);
            inventoryPage = new InventoryPage(driver);
            cartPage = new CartPage(driver);
            checkoutPage = new CheckoutPage(driver);
            hiddenMenuPage = new HiddenMenuPage(driver);
        }


        [TearDown]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
        protected void Login(string username, string password)
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            var loginPage = new LoginPage(driver);
            loginPage.LoginUser(username, password);
        }
    }
}
