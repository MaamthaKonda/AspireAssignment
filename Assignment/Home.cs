using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Home
    {
        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {            
            driver = new ChromeDriver();
        
        }

        [Test]
        public void home()
        {
            //Browsing URL
            driver.Url = "https://www.amazon.com";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            WebDriverWait wait1 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement searchbox = driver.FindElement(By.XPath("//*[@id=\"twotabsearchtextbox\"]"));
            searchbox.Click();
            //Searching for iphone 14
            searchbox.SendKeys("iphone 14");
            searchbox.SendKeys(Keys.Enter);
           
        }
        [Test]
        public void TestCase1()
        {
            /*select price 100 to 200
              sort high to low
              validate sort and price in all pages
             */
            home();
            IWebElement lowprice = driver.FindElement(By.XPath("//*[@id=\"low-price\"]"));
            lowprice.Click();
            lowprice.SendKeys("100");
            IWebElement highprice = driver.FindElement(By.XPath("//*[@id=\"high-price\"]"));
            highprice.Click();
            highprice.SendKeys("2000");
            highprice.SendKeys(Keys.Enter);
            IWebElement next = driver.FindElement(By.LinkText("Next"));
                     
            do
            {
                List<int> items = new List<int>();
                IList<IWebElement> allitems = driver.FindElements(By.ClassName("a-price-whole"));
                foreach (IWebElement item in allitems)
                {
                    int itemval = int.Parse(item.Text, System.Globalization.NumberStyles.AllowThousands);
                    items.Add(itemval);
                    if (itemval >= 100 && itemval <= 2000)
                    {
                        Assert.Pass("Test Case 1 Passed");
                        
                    }
                    else
                    {                       
                        Assert.Fail("Test Case 1 Failed" + itemval);
                    }


                }
                next.Click();
            }
            while(next.Enabled);
            
        }
        [Test]
        public void TestCase2()
        {
            /*
             * select random item
                Add to cart
                Go to cart
                proceed to checkout
                sign in
             */
            home();
            IWebElement lowprice = driver.FindElement(By.XPath("//*[@id=\"low-price\"]"));
            lowprice.Click();
            lowprice.SendKeys("100");
            IWebElement highprice = driver.FindElement(By.XPath("//*[@id=\"high-price\"]"));
            highprice.Click();
            highprice.SendKeys("200");
            highprice.SendKeys(Keys.Enter);
            IWebElement sortselect = driver.FindElement(By.XPath("//*[@id=\"a-autoid-0-announce\"]"));
            sortselect.Click();
            IWebElement hightolow = driver.FindElement(By.XPath("//*[@id=\"s-result-sort-select_2\"]"));
            hightolow.Click();
            IWebElement next = driver.FindElement(By.LinkText("Next"));

            do
            {
                List<int> items = new List<int>();
                IList<IWebElement> allitems = driver.FindElements(By.ClassName("a-price-whole"));
                foreach (IWebElement item in allitems)
                {
                    int itemval = int.Parse(item.Text, System.Globalization.NumberStyles.AllowThousands);
                    items.Add(itemval);
                    items.Sort();
                    items.Reverse();
                    Console.WriteLine("Sorted:"+String.Join(",",itemval));
                }
                next.Click();
            }
            while (next.Enabled);

        }
        [Test]
        public void TestCase3()
        {
            home();
            IWebElement lowprice = driver.FindElement(By.XPath("//*[@id=\"low-price\"]"));
            lowprice.Click();
            lowprice.SendKeys("100");
            IWebElement highprice = driver.FindElement(By.XPath("//*[@id=\"high-price\"]"));
            highprice.Click();
            highprice.SendKeys("200");
            highprice.SendKeys(Keys.Enter);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement selectitem = driver.FindElement(By.XPath("//*[@id=\"search\"]/div[1]/div[1]/div/span[3]/div[2]/div[2]/div/div/div/div/div/div[2]/div/div/div[1]/h2/a/span"));
            selectitem.Click();
            IWebElement deliverTo = driver.FindElement(By.XPath("//*[@id=\"contextualIngressPtLabel_deliveryShortLine\"]/span[1]"));
            deliverTo.Click();
            String ParentBrowserWindow = driver.CurrentWindowHandle;
            IList<string> ListOfWindowHandles = driver.WindowHandles;
            foreach (var window in ListOfWindowHandles)
            {
                try
                {
                    
                    if (driver.SwitchTo().Window(window).PageSource.Contains("Choose your location"))
                    {                       
                        IWebElement deliverTocng = driver.FindElement(By.XPath("//*[@id=\"GLUXCountryListDropdown\"]/span/span"));
                        deliverTocng.Click();
                        IWebElement deliverTo1 = driver.FindElement(By.XPath("//*[@id=\"GLUXCountryList_0\"]"));
                        deliverTo1.Click();
                        //   IWebElement done = driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[2]/span/span/span/button"));
                        IWebElement done = driver.FindElement(By.XPath("//*[@id=\"a-popover-2\"]/div/div[2]/span/span/span/button"));
                        done.Click();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           
            System.Threading.Thread.Sleep(1000);
            driver.SwitchTo().Window(ParentBrowserWindow);
            System.Threading.Thread.Sleep(1000);
            IWebElement addtocart = driver.FindElement(By.XPath("//*[@id=\"add-to-cart-button\"]"));
            addtocart.Click();
            IWebElement gotocart = driver.FindElement(By.XPath("//*[@id=\"sw-gtc\"]/span/a"));
            gotocart.Click();
            IWebElement proceedtochkout = driver.FindElement(By.XPath("//*[@id=\"sc-buy-box-ptc-button\"]/span/input"));
            proceedtochkout.Click();
            IWebElement signinpg = driver.FindElement(By.XPath("//*[@id=\"authportal-main-section\"]/div[2]/div/div[1]/form/div/div/div/h1"));
            string text = signinpg.Text;
          
            if(text.Contains("Sign in"))
            {
                Assert.Pass("TestCase 3 Passed");
            }
            else
            {
                Assert.Fail("TestCase 3 Failed");
            }
           
        }
        [TearDown]
        public void closeBrowser()
        {
          // driver.Close();
        }

    }
}



