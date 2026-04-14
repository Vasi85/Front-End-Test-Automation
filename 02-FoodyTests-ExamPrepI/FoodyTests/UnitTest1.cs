using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Linq;
using SeleniumExtras.WaitHelpers;


namespace FoodyTests
{
    public class Tests
    {
        private IWebDriver driver;
        private readonly string BaseUrl = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:85/";

        private string lastCreatedFoodName;
        private string lastCreatedFoodDescription;
        private Random random;

        [OneTimeSetUp]
        public void SetUp()
        {
            random = new Random();
            driver = new ChromeDriver();

            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            driver.Navigate().GoToUrl(BaseUrl + "User/Login");

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // изчакай полето username да е видимо
            var username = wait.Until(d => d.FindElement(By.Id("username")));
            username.Clear();
            username.SendKeys("VasiMii");

            // password
            var password = wait.Until(d => d.FindElement(By.Id("password")));
            password.Clear();
            password.SendKeys("Parola12");

            // натисни бутона (match по текста "Log" за да покрие "Log\n in")
            var loginBtn = wait.Until(d => d.FindElement(By.XPath("//button[contains(normalize-space(.), 'Log')]")));
            loginBtn.Click();
            /*driver.FindElement(By.XPath("//a[text()='Log In']")).Click();

            driver.FindElement(By.Id("Username")).SendKeys("VasiMii");
            driver.FindElement(By.Id("Password")).SendKeys("Parola12");

            driver.FindElement(By.XPath("//a[text()='Log In']")).Click();*/
        }

        [Test, Order(1)]
        public void AddFoodWithInvalidDataTest()
        {
            driver.Navigate().GoToUrl(BaseUrl + "Food/Add");
            driver.FindElement(By.XPath("//a[text()='Add Food']")).Click();
            driver.FindElement(By.Id("name")).SendKeys(string.Empty);
            driver.FindElement(By.Id("description")).SendKeys(string.Empty);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            Assert.That(driver.Url,Is.EqualTo(BaseUrl + "Food/Add"));
            var errorMessage = driver.FindElement(By.XPath("//div[@class='text-danger validation-summary-errors']//li")).Text;
            Assert.That(errorMessage.Trim(), Is.EqualTo("Unable to add this food revue!"));
        }
        [Test, Order(2)]
        public void AddRandomFoodTest()
        {
            lastCreatedFoodName = "Title" + random.Next(99, 9999).ToString();
            lastCreatedFoodDescription = "Description" + random.Next(99, 9999).ToString();

            driver.Navigate().GoToUrl(BaseUrl + "Food/Add");
            driver.FindElement(By.XPath("//a[text()='Add Food']")).Click();

            driver.FindElement(By.Id("name")).SendKeys(lastCreatedFoodName);
            driver.FindElement(By.Id("description")).SendKeys(lastCreatedFoodDescription);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url == BaseUrl);

            Assert.That(driver.Url, Is.EqualTo(BaseUrl));
            Assert.That(driver.Title.Trim(), Is.EqualTo("Home Page - Foody.WebApp"));

            var lastCreatedFoodNameText = driver.FindElement(By.XPath("(//h2[@class='display-4'])[last()]")).Text;

            Assert.That(lastCreatedFoodNameText, Is.EqualTo(lastCreatedFoodName));
        }

        [Test, Order(3)]
        public void EditLastAddedFoodTest()
        {
            driver.FindElement(By.XPath("//a[text()='FOODY']")).Click();

            string editedTitle = "Edited";

            // locate last item edit button
            var lastFoodEditBtn = driver.FindElement(By.XPath("(//div[@class='row gx-5 align-items-center'])[last()]//a[text()='Edit']"));
            lastFoodEditBtn.Click();

            // try to change the name (will not change on save)
            driver.FindElement(By.Id("name")).Clear();
            driver.FindElement(By.Id("name")).SendKeys(editedTitle);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            // get the latest food title again
            var lastCreatedFoodNameText = driver.FindElement(By.XPath("(//div//h2[@class='display-4'])[last()]")).Text;

            Console.WriteLine("The title remains unchanged due to incomplete functionality.");

            // ❗ expected behaviour: title stays the same
            Assert.That(lastCreatedFoodNameText, Is.EqualTo(lastCreatedFoodName));
        }

        [Test, Order(4)]
        public void SearchForFoodTitleTest()
        {
            driver.FindElement(By.XPath("//a[text()='FOODY']")).Click();

            driver.FindElement(By.XPath("//input[@type='search']")).SendKeys(lastCreatedFoodName);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            var allFoofContainers = driver.FindElements(By.XPath("(//div[@class='row gx-5 align-items-center'])[last()]"));
            
            Assert.That(allFoofContainers.Count(), Is.EqualTo(1));
        }
        [Test, Order(5)]
        public void DeleteLastAddedFoodTest()
        {
            driver.FindElement(By.XPath("//a[text()='FOODY']")).Click();
            var initialCount = driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']")).Count();
            var lastFoodContainer = driver.FindElement(By.XPath("(//div[@class='row gx-5 align-items-center'])[last()]"));

            Actions actions = new Actions(driver);
            actions.MoveToElement(lastFoodContainer).Perform();

            Assert.That(lastFoodContainer.Enabled, Is.True);
            Assert.That(lastFoodContainer.Displayed, Is.True);

            driver.FindElement(By.XPath("(//div[@class='row gx-5 align-items-center'])[last()]//a[text()='Delete']")).Click();

            var coutAfterDeletion = driver.FindElements(By.XPath("//div[@class='row gx-5 align-items-center']")).Count();

            Assert.That(coutAfterDeletion, Is.EqualTo(initialCount - 1));
        }
        [Test, Order(6)]
        public void SearchForDeletedFoodTest()
        {
            driver.FindElement(By.XPath("//a[text()='FOODY']")).Click();

            driver.FindElement(By.XPath("//input[@type='search']")).SendKeys(lastCreatedFoodName);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            var noFoodErrorMessage = wait.Until(d=>d.FindElement(By.XPath("//h2[@class='display-4']")));
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//img[@class='img-fluid rounded-circle']")));
            var addFoodBtn = wait.Until(d=>d.FindElement(By.XPath("//a[text()='Add Food']")));

            Assert.That(addFoodBtn.Displayed, Is.True);
            Assert.That(noFoodErrorMessage.Text, Is.EqualTo("There are no foods :("));
            
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}