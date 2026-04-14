using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace IdeaCenterTestsExamPrep.pages
{
    public class IdeasEditPage : BasePage
    {
        public IdeasEditPage(IWebDriver driver) : base(driver) { }
        public IWebElement TitleField => driver.FindElement(By.XPath("//input[@name='Title']"));
        public IWebElement UrlField => driver.FindElement(By.XPath("//input[@name='Url']"));
        public IWebElement DescriptionField => driver.FindElement(By.XPath("//textarea[@name='Description']"));
        public IWebElement EditBtn => driver.FindElement(By.XPath("//button[@class='btn btn-primary btn-lg']"));

    }
}
