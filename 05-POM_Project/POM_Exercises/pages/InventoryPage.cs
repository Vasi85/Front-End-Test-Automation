using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace POM_Exercises.pages
{
    public class InventoryPage : BasePage
    {
        protected readonly By cartLink = By.CssSelector(".shopping_cart_link");

        protected readonly By productsPageTitle = By.ClassName("title");

        protected readonly By productItems = By.CssSelector(".inventory_item");
        public InventoryPage(IWebDriver driver) : base(driver)
        { 
            
        }

        public void AddToCartByIndex(int itemIndex)
        {
            var itemByIndexBtn = By.XPath($"//div[@class='inventory_list']//div[@class = 'inventory_item'][{itemIndex}]//button");
            Click(itemByIndexBtn);
        }

        public void AddToCartByName (string name)
        {
            var itemByNameBtn = By.XPath($"//div[text()='{name}']/ancestor::div[@class='inventory_item_description']//button");
            Click(itemByNameBtn);
        }

        public void ClickCartLink()
        {
            Click(cartLink);
        }

        public bool IsInventoryPageHasItemsDisplayed()
        {
            return FindElements(productItems).Any();
        }

        public bool IsInventoryPageLoaded()
        {
            return GetText(productsPageTitle) == "Products" && driver.Url.Contains("inventory.html");
        }
    }
}