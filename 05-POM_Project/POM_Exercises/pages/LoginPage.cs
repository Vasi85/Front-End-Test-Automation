using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace POM_Exercises.pages
{
    public class LoginPage : BasePage
    {
        private readonly By userNameField = By.XPath("//input[@id='user-name']");

        private readonly By passwordField = By.XPath("//input[@id='password']");

        private readonly By loginBtn = By.XPath("//input[@id='login-button']");

        private readonly By errorMessage = By.XPath("//div[@class='error-message-container error']");

        public LoginPage(IWebDriver driver) : base(driver)
        {

        }

        public void FillUserName(string userName)
        {
            Type(userNameField, userName);
        }
            
        public void FillPassword(string password)
        {
            Type(passwordField, password);
        }

        public void ClickLoginBtn()
        {
            Click(loginBtn);
        }

        public string GetErrorMessage()
        {
            return GetText(errorMessage);
        }

        public void LoginUser(string userName, string password)
        {
            FillUserName(userName);
            FillPassword(password);
            ClickLoginBtn();
        }
    }
}
