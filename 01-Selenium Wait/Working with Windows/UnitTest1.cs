using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;


namespace Selenium_Wait
{
    public class WorkingWithWindowTest
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test, Order(1)]
        public void Test_Hadling_Multiple_Window()
        {
            driver.Url = "https://the-internet.herokuapp.com/windows";
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            Assert.That(windowHandles.Count, Is.EqualTo(2), "There should be two windows open");
            driver.SwitchTo().Window(windowHandles[1]);

            string newWindowContent = driver.PageSource;
            Assert.That(newWindowContent, Does.Contain("New Window"));

        }
        [Test, Order(2)]
        public void Test_Handling_NoSuch_Window()
        {
            driver.Url = "https://the-internet.herokuapp.com/windows";
            driver.FindElement(By.LinkText("Click Here")).Click();

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;

            Assert.That(windowHandles.Count, Is.EqualTo(2), "There should be two windows open");
            driver.SwitchTo().Window(windowHandles[1]);

            driver.Close();

            try 
            { 
                driver.SwitchTo().Window(windowHandles[1]);
            }
            catch (NoSuchWindowException)
            {
                Assert.Pass("NoSuchWindowException was correctly handled.");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message.ToString());
            }
            finally
            {
                driver.SwitchTo().Window(windowHandles[0]);
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}