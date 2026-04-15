using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
namespace POM_Exercises.pages
{
    public class HiddenMenuPage : BasePage
    {
        protected readonly By hiddenMenuBtn = By.Id("react-burger-menu-btn");

        protected readonly By logoutBtn = By.XPath("//a[text()='Logout']");
        public HiddenMenuPage(IWebDriver driver) : base(driver)
        {
        }
        public void ClickMenuBtn()
        {
            Click(hiddenMenuBtn);
        }
        public void ClickHiddenLogoutBtn()
        {
            Click(logoutBtn);
        }

        public bool IsMenuOpen()
        {
            return FindElement(logoutBtn).Displayed;
        }
    }
}
