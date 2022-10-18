using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    class Program
    {
       
        static void Main(string[] args)
          {
            IWebDriver driver;
            //Console.WriteLine("First Assignment");
            // Console.ReadLine();
            driver = new ChromeDriver(@"../../" + "/Drivers/");
            driver.Url = "https://www.amazon.com";
        }
    }
}
