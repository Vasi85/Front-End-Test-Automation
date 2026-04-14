using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace IdeaCenterTestsExamPrep.Tests
{
    public class IdeaCenterTests : BaseTest
    {
        public string lastCreatedIdeaTitle;
        public string lastCreatedIdeaDescription;
        public string image;
        [Test, Order(1)]
        public void CreateIdeaWithInvalidData()
        {
            createIdeaPage.OpenPage();
            createIdeaPage.CreateIdea("","","");
            createIdeaPage.AssertErrorMessages();
        }
        [Test, Order(2)]
        public void CreateRandomIdeaTest()
        {
            lastCreatedIdeaTitle = "Idea" + GenerateRandomString(10);
            image = "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com:83/images/IdeaIcon.jpg";
            lastCreatedIdeaDescription = "Description" + GenerateRandomString(10);
            createIdeaPage.OpenPage();
            createIdeaPage.CreateIdea(lastCreatedIdeaTitle, "", lastCreatedIdeaDescription);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/Ideas/MyIdeas"));
            //Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Url is not correct");
            Assert.That(driver.Url, Is.EqualTo(ideasReadPage.UrlMyIdeas), "Url is not correct");
            Assert.That(myIdeasPage.DescriptionLastIdea.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Descriptions not match");
        }
        [Test, Order(3)]
        public void ViewLastCreatedIdeaTest()
        {
            ideasReadPage.OpenPage();
            //MyIdeasLink.Click();
            myIdeasPage.ViewLastIdea.Click();
            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(lastCreatedIdeaTitle), "Title do not match");
            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Description do not match");

        }
        [Test, Order(4)]
        public void EditLastIdeaTitleTest()
        {
            ideasReadPage.OpenPage();

            //MyIdeasLink.Click();
            myIdeasPage.EditLastIdea.Click();
            string updatedTitle = "Change Title" + lastCreatedIdeaTitle;
            ideasEditPage.TitleField.Clear();
            ideasEditPage.TitleField.SendKeys(updatedTitle);
            ideasEditPage.EditBtn.Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/Ideas/MyIdeas"));
            //Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Not correctly redirecterd");
            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.UrlMyIdeas), "Not correctly redirecterd");
            myIdeasPage.ViewLastIdea.Click();

            Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(updatedTitle), "Title do not match");
            //Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(lastCreatedIdeaDescription), "Description do not match");

        }
        [Test, Order(5)]
        public void EditLastIdeaDescriptionTest()
        {
            ideasReadPage.OpenPage();

            //MyIdeasLink.Click();
            myIdeasPage.EditLastIdea.Click();
            string updatedDescription = "Change Ddescription" + lastCreatedIdeaDescription;
            ideasEditPage.DescriptionField.Clear();
            ideasEditPage.DescriptionField.SendKeys(updatedDescription);
            //ideasEditPage.DescriptionField.SendKeys(updatedDescription);
            ideasEditPage.EditBtn.Click();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Url.Contains("/Ideas/MyIdeas"));

            //Assert.That(driver.Url, Is.EqualTo(myIdeasPage.Url), "Not correctly redirecterd");
            Assert.That(driver.Url, Is.EqualTo(myIdeasPage.UrlMyIdeas), "Not correctly redirecterd");
            myIdeasPage.ViewLastIdea.Click();

            //Assert.That(ideasReadPage.IdeaTitle.Text.Trim(), Is.EqualTo(updatedTitle), "Title do not match");
            Assert.That(ideasReadPage.IdeaDescription.Text.Trim(), Is.EqualTo(updatedDescription), "Description do not match");

        }
        [Test, Order(6)]
        public void DeleteLastIdeaTest()
        {
            ideasReadPage.OpenPage();
            myIdeasPage.DeleteLastIdea.Click();
            bool isIdeaDeleted = myIdeasPage.IdeasCards.All(card => card.Text.Contains(lastCreatedIdeaDescription));
            Assert.IsFalse(isIdeaDeleted, "The idea was not deleted");

        }
    }
}
