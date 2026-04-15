using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace POM_Exercises.pages
{
    public class CheckoutPage : BasePage
    {
        /*protected readonly By firstNameInput = By.XPath("//input[@id='first-name']");

        protected readonly By lastNameInput = By.XPath("//div[@class='form_group']//input[@id='last-name']");

        protected readonly By postalCodeInput = By.Id("postal-code");

        protected readonly By continueBtn = By.XPath("//input[@type='submit']");

        protected readonly By finishBtn = By.CssSelector("#finish");

        protected readonly By completeHeader = By.ClassName("complete-header");*/

        private readonly By firstNameInput = By.Id("first-name");
        private readonly By lastNameInput = By.Id("last-name");
        private readonly By postalCodeInput = By.Id("postal-code");
        private readonly By continueBtn = By.CssSelector(".cart_button");
        private readonly By finishBtn = By.Id("finish");
        private readonly By completeHeader = By.CssSelector(".complete-header");
        public CheckoutPage(IWebDriver driver) : base(driver)
        {

        }

        public void FillFirstName(string firstName)
        {
            Type(firstNameInput, firstName);
        }
        public void FillLastName(string lastName)
        {
            Type(lastNameInput, lastName);
        }

        public void FillPostalCode(string postalCode)
        {
            Type(postalCodeInput, postalCode);
        }

        public void ClickContinueBtn()
        {
            Click(continueBtn);
        }

        public void ClickFinishBtn()
        {
            Click(finishBtn);
        }

        public bool IsPageLoaded()
        {
            return driver.Url.Contains("checkout-step-one.html") ||
                driver.Url.Contains("checkout-step-two.html");
        }

        public bool IsCheckoutComplete()
        {
            return GetText(completeHeader) == "Thank you for your order!";
        }
    }
}
