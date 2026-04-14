using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace IdeaCenterTestsExamPrep.pages
{
    public class MyIdeasPage : BasePage
    {
        public MyIdeasPage(IWebDriver driver) : base(driver) { }

        public string Url = BaseUrl + "/Ideas/Create";
        public string UrlMyIdeas = BaseUrl + "/Ideas/MyIdeas";

        public ReadOnlyCollection<IWebElement> IdeasCards => driver.FindElements(By.XPath("//div[@class='card mb-4 box-shadow']"));

        public IWebElement ViewLastIdea => IdeasCards.Last().FindElement(By.XPath(".//a[contains(@href,'/Ideas/Read')]"));
        public IWebElement EditLastIdea => IdeasCards.Last().FindElement(By.XPath(".//a[contains(@href,'/Ideas/Edit')]"));
        public IWebElement DeleteLastIdea => IdeasCards.Last().FindElement(By.XPath(".//a[contains(@href,'/Ideas/Delete')]"));
        public IWebElement DescriptionLastIdea => IdeasCards.Last().FindElement(By.XPath(".//p[@class='card-text']"));





        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }
    }
}
