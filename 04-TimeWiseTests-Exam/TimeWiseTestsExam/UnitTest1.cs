using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TimeWiseTestsExam
{
    public class Tests
    {
        private IWebDriver driver;
        private readonly string BaseUrl = "https://d1dzr3dh7g0qgk.cloudfront.net/";

        private string lastCreatedTaskName;
        private string lastCreatedTaskDescription;
        private Random random;
        [SetUp]
        public void Setup()
        {
            random = new Random();
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            driver.Navigate().GoToUrl(BaseUrl + "User/LoginRegister");
            driver.FindElement(By.Id("tab-login")).Click();
            driver.FindElement(By.Id("loginName")).SendKeys("BabaVida1001@abv.bg");
            driver.FindElement(By.Id("loginPassword")).SendKeys("12345p");
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
        }

        [Test, Order(1)]
        public void AddTaskWithoutNameTest()
        {
            driver.FindElement(By.XPath("//span[text()='To-do']")).Click();
            driver.FindElement(By.XPath("//a[@class='btn btn-info']")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            Assert.That(driver.Url, Is.EqualTo(BaseUrl + "Task/Create"), "The url is not redirected correctly");

        }
        [Test, Order(2)]
        public void AddTaskWithRandomNameTest()
        {
            lastCreatedTaskName = "Task_" + random.Next(999, 9999);
            lastCreatedTaskDescription = "Random Description" + Guid.NewGuid().ToString();

            driver.FindElement(By.XPath("//span[text()='To-do']")).Click();
            driver.FindElement(By.XPath("//a[@class='btn btn-info']")).Click();
            driver.FindElement(By.XPath("//input[@id='form4Example1']")).SendKeys(lastCreatedTaskName);
            driver.FindElement(By.XPath("//textarea[@id='form4Example3']")).SendKeys(lastCreatedTaskDescription);
            driver.FindElement(By.XPath("//input[@id='datetimepicker1Input']")).SendKeys(DateTime.Now.AddDays(1).ToString("MM/dd/yyyy HH:mm"));
            driver.FindElement(By.XPath("//input[@id='datetimepicker2Input']")).SendKeys(DateTime.Now.AddDays(2).ToString("MM/dd/yyyy HH:mm"));
            var statusSelect = driver.FindElement(By.CssSelector("select.form-select"));
            var select = new SelectElement(statusSelect);
            select.SelectByValue("10");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.EndsWith("/Task/ToDo"));

            Assert.That(driver.Url, Is.EqualTo(BaseUrl + "Task/ToDo"), "The url is not redirected correctly");

            var lastCreatedTaskNameText = driver.FindElement(By.XPath("(//h5[@class='card-title'])[last()]")).Text;

            Assert.That(lastCreatedTaskNameText, Is.EqualTo(lastCreatedTaskName), "The task name doesn't match");

        }
        [Test, Order(3)]
        public void EditLastAddedTaskTest()
        {
            string editedTask = "Edited:" + lastCreatedTaskName;
            driver.FindElement(By.XPath("//span[text()='To-do']")).Click();
            driver.FindElement(By.XPath("(//a[@class='btn btn-info'])[last()]")).Click();
            driver.FindElement(By.XPath("//input[@id='form4Example1']")).Clear();
            driver.FindElement(By.XPath("//input[@id='form4Example1']")).SendKeys(editedTask);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.EndsWith("/Task/ToDo"));

            Assert.That(driver.Url, Is.EqualTo(BaseUrl + "Task/ToDo"), "The url is not redirected correctly");

            var lastCreatedTaskNameText = driver.FindElement(By.XPath("(//h5[@class='card-title'])[last()]")).Text;

            lastCreatedTaskName = editedTask;
            Assert.That(lastCreatedTaskNameText, Is.EqualTo(lastCreatedTaskName), "The task name doesn't match");

        }
        [Test, Order(4)]
        public void MoveLastAddedTaskTest()
        {
            driver.FindElement(By.XPath("//span[text()='To-do']")).Click();
            driver.FindElement(By.XPath("(//a[@class='btn btn-info'])[last()]")).Click();
            var statusSelect = driver.FindElement(By.CssSelector("select.form-select"));
            var select = new SelectElement(statusSelect);
            select.SelectByValue("20");

            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.EndsWith("/Task/ToDo"));

            Assert.That(driver.PageSource.Contains(lastCreatedTaskName), Is.False, $"The task '{lastCreatedTaskName}' is still visible in the To-Do list.");
        }
        [Test, Order(5)]
        public void DeleteLastAddedTaskTest()
        {
            driver.FindElement(By.XPath("//span[text()='In Progress']")).Click();
            driver.FindElement(By.XPath("(//a[@class='btn btn-danger'])[last()]")).Click();
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.EndsWith("/Task/InProgress"));

            Assert.That(driver.PageSource.Contains(lastCreatedTaskName), Is.False, $"The task '{lastCreatedTaskName}' is still visible in the In Progress list.");
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}