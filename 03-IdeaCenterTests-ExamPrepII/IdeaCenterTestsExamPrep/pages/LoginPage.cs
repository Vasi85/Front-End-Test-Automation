using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace IdeaCenterTestsExamPrep.pages
{
    public class LoginPage :BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {

        }
        public string Url = BaseUrl + "/Ideas/Create";
        public IWebElement EmailField => driver.FindElement(By.XPath("//input[@id='typeEmailX-2']"));
        public IWebElement PasswordField => driver.FindElement(By.XPath("//input[@id='typePasswordX-2']"));
        public IWebElement SignInBtn => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg btn-block']"));

        public void Login (string email, string password)
        {
            EmailField.SendKeys(email);
            PasswordField.SendKeys(password);
            SignInBtn.Click();
        }

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

    }
}
