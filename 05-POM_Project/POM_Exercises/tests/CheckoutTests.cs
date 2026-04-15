using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Exercises.tests
{
    public class CheckoutTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Login("standard_user", "secret_sauce");
            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();
            cartPage.ClickCheckout();
        }
        [Test]
        public void TestCheckoutPageLoaded()
        {
            Assert.True(checkoutPage.IsPageLoaded(), "Checkout page not loaded");
        }
        [Test]
        public void TestContinueNextStep()
        {
            checkoutPage.FillFirstName("Zaio");
            checkoutPage.FillLastName("Baio");
            checkoutPage.FillPostalCode("1000");
            checkoutPage.ClickContinueBtn();

            Assert.That(driver.Url.Contains("checkout-step-two.html"), Is.True, "Not navigated to the correct checkout page");
        }
        [Test]
        public void TestCompletedOrder()
        {
            checkoutPage.FillFirstName("Zaio");
            checkoutPage.FillLastName("Baio");
            checkoutPage.FillPostalCode("1000");
            checkoutPage.ClickContinueBtn();
            checkoutPage.ClickFinishBtn();

            Assert.True(checkoutPage.IsCheckoutComplete(), "Checkout was not completed");
        }
    }
}
