using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace Selenium_Wait
{
    public class ImpliciteWaitTest
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Url = "https://practice.bpbonline.com/";
        }

        [Test, Order(1)]
        public void Test_Search_Product_With_Implicit_Wait()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("keyboard");
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();

                Assert.That(driver.PageSource, Does.Contain("keyboard"), "The product keyboard was not found");
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected expection: " + ex.Message);
            }
        }
        [Test, Order(2)]
        public void Test_Search_Product_Junk_Should_Add()
        {
            driver.FindElement(By.Name("keywords")).SendKeys("junk");
            driver.FindElement(By.XPath("//input[@title=' Quick Find ']")).Click();

            try
            {
                driver.FindElement(By.LinkText("Buy Now")).Click();

                Assert.That(driver.PageSource, Does.Contain("keyboard"), "The product keyboard was not found");
            }
            catch (NoSuchElementException ex)
            {
                Assert.Pass("Expected NoSuchElementExpection was thrown");
                Console.WriteLine("Timeout: " + ex.Message);
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