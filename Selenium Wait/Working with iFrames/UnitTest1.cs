using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Selenium_Wait
{
    public class IFramesTest
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test, Order(1)]
        public void Test_Search_Product_With_Implicit_Wait()
        {
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe")));
            var dropdownBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownBtn.Click();
            var dropdownLink = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLink)
            {
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected.");
            }
            driver.SwitchTo().DefaultContent();
        }
        [Test, Order(2)]
        public void Test_Search_Product_Junk_Should_Add()
        {
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt("result"));
            var dropdownBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownBtn.Click();
            var dropdownLink = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLink)
            {
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected.");
            }
            driver.SwitchTo().DefaultContent();
        }
        [Test, Order(3)]
        public void Test_iFrame_By_Web_Element()
        {
            driver.Url = "https://codepen.io/pervillalva/full/abPoNLd";
            var frameElement = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("#result")));
            driver.SwitchTo().Frame(frameElement);
            var dropdownBtn = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".dropbtn")));
            dropdownBtn.Click();
            var dropdownLink = wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(".dropdown-content a")));

            foreach (var link in dropdownLink)
            {
                Assert.That(link.Displayed, Is.True, "Link inside the dropdown is not displayed as expected.");
            }
            driver.SwitchTo().DefaultContent();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}