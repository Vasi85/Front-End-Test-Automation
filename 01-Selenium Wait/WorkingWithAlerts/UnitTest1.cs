using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;


namespace Selenium_Wait
{
    public class WorkingWithAlertsTest
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test, Order(1)]
        public void Test_Hadling_Basic_Alert()
        {
            driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            driver.FindElement(By.XPath("//button[contains (text(), 'Click for JS Alert')]")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Alert"), "Alert text is not as expected.");
            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You successfully clicked an alert"), "Result message is not as expected.");

        }
        [Test, Order(2)]
        public void Test_Handling_Confirm_Alert()
        {
            driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            driver.FindElement(By.XPath("//button[contains (text(), 'Click for JS Confirm')]")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected.");
            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Ok"), "Result message is not as expected after accepting the alert.");

            driver.FindElement(By.XPath("//button[contains (text(), 'Click for JS Confirm')]")).Click();
            Assert.That(alert.Text, Is.EqualTo("I am a JS Confirm"), "Alert text is not as expected.");
            alert.Dismiss();

            resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You clicked: Cancel"), "Result message is not as expected after accepting the alert.");
        }
        [Test, Order(3)]
        public void Test_Handling_JavaScript_Prompt()
        {
            driver.Url = "https://the-internet.herokuapp.com/javascript_alerts";
            driver.FindElement(By.XPath("//button[contains (text(), 'Click for JS Prompt')]")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text is not as expected.");
            string inputText = "Hello world";
            alert.SendKeys(inputText);
            alert.Accept();

            IWebElement resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You entered: " + inputText), "Result message is not as expected after entering text in the prompt.");

            driver.FindElement(By.XPath("//button[contains (text(), 'Click for JS Prompt')]")).Click();
            Assert.That(alert.Text, Is.EqualTo("I am a JS prompt"), "Alert text is not as expected.");
            alert.Dismiss();

            resultElement = driver.FindElement(By.Id("result"));
            Assert.That(resultElement.Text, Is.EqualTo("You entered: null"), "Result message is not as expected after accepting the alert.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Dispose();
        }
    }
}