using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Selenium_Wait
{
    public class ImplicitWaitTest
    {
        IWebDriver driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Url = "https://practice.bpbonline.com/";
        }

        [Test, Order(1)]
        public void Test_Search_Product_With_Implicit_Wait()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("keywords"))).SendKeys("keyboard");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@title=' Quick Find ']"))).Click();

            try
            {
                IWebElement buyNowLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Buy Now")));
                buyNowLink.Click();

                Assert.That(driver.PageSource, Does.Contain("keyboard"), "The product keyboard was not found");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("The Buy Now Link was not found this product");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected expection: " + ex.Message);
            }
        }
        [Test, Order(2)]
        public void Test_Search_Product_Junk_Should_Add()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("keywords"))).SendKeys("junk");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//input[@title=' Quick Find ']"))).Click();

            try
            {
                IWebElement buyNowLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Buy Now")));
                buyNowLink.Click();

                Assert.Fail(driver.PageSource, Does.Contain("keyboard"), "The product keyboard was not found");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Pass("Expected WebDriverTimeoutException was thrown");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception" + ex.Message);
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}