using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace POM_Exercises.pages
{
    public class CartPage : BasePage
    {
        protected readonly By cartItem = By.CssSelector(".cart_item");

        protected readonly By checkoutBtn = By.Id("checkout");

        public CartPage(IWebDriver driver) : base(driver)
        {

        }

        public bool IsCartItemDisplayed()
        {
            return FindElement(cartItem).Displayed;
        }

        public void ClickCheckout()
        {
            Click(checkoutBtn);
        }
    }
}
