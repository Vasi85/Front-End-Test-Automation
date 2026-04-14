using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace IdeaCenterTestsExamPrep.pages
{
    public class IdeasReadPage : BasePage
    {
        public IdeasReadPage(IWebDriver driver) : base(driver) { }
        //public string Url = BaseUrl + "/Ideas/Read";
        public string UrlMyIdeas = BaseUrl + "/Ideas/MyIdeas";
        public IWebElement MyIdeasLink => driver.FindElement(By.XPath("//a[@class='nav-link' and text()='My Ideas']"));

        public IWebElement IdeaTitle => driver.FindElement(By.XPath("//h1[@class='mb-0 h4']"));
        public IWebElement IdeaDescription => driver.FindElement(By.XPath("//section[@class='row']//p"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(UrlMyIdeas);
            MyIdeasLink.Click();
        }
    }
    
}
