using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Exercises.tests
{
    public class CartTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Login("standard_user", "secret_sauce");
            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();
        }
        [Test]
        public void TestCartItemDisplayed()
        {
            Assert.True(cartPage.IsCartItemDisplayed(), "There ware no products in the cart");
        }
        [Test]
        public void TestClickCheckoutButton()
        {
            cartPage.ClickCheckout();
            Assert.True(checkoutPage.IsPageLoaded(), "Not navigated to the checkout page");
        }
    }
}
