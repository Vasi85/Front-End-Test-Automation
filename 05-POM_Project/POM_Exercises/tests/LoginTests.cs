using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POM_Exercises.pages;

namespace POM_Exercises.tests
{
    public class LoginTests : BaseTest
    {
        [Test]
        public void TestLoginWithValidData()
        {
            Login("standard_user", "secret_sauce");
            
            Assert.That(inventoryPage.IsInventoryPageLoaded(), Is.True, "The inventory page is not loaded after successful login");
        }
        [Test]
        public void TestLoginWithInvalidData()
        {
            Login("invalid_user", "secret_sauce");
           
            string error = loginPage.GetErrorMessage();
            Assert.That(error.Contains("Username and password do not match any user in this service"), "Error message is not correct");  
        }
        [Test]
        public void TestLoginWithLockUser()
        {
            Login("locked_out_user", "secret_sauce");
           
            string error = loginPage.GetErrorMessage();
            Assert.That(error.Contains("Epic sadface: Sorry, this user has been locked out."), "Error message is not correct"); ;
        }
    }
}
