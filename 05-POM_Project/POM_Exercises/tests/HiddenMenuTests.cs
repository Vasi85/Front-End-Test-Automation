using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_Exercises.tests
{
    public class HiddenMenuTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Login("standard_user", "secret_sauce");
        }
        [Test]
        public void TestOpenMenu()
        {
            hiddenMenuPage.ClickMenuBtn();
            Assert.True(hiddenMenuPage.IsMenuOpen(), "Hidden menu was not open");
        }
        [Test]
        public void TestLogout()
        {
            hiddenMenuPage.ClickMenuBtn();
            hiddenMenuPage.ClickHiddenLogoutBtn();
            Assert.True(driver.Url.Equals("https://www.saucedemo.com/"), "User was not logget out");
        }
    }
}
