using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Diagnostics;
using System.Net.Mail;
using OpenQA.Selenium.Edge;
using System.Windows.Media.Animation;
using OtpNet;
using System.Drawing.Imaging;
using System.Drawing.Design;
//using OpenQA.Selenium.DevTools.V113.Debugger;

namespace main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string baseDir;

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click_bk(object sender, RoutedEventArgs e)
        {
            var options = new ChromeOptions();

            string baseDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ref");
            baseDir += "\\";
            string outputPath = baseDir + "output.txt";

            options.AddArgument(@"user-data-dir=C:\Users\Surface\Desktop\fuel_chrome\mail\");
            options.AddArgument("profile-directory=Default");

            options.AddArguments("start-maximized");
            string[] dataa = System.IO.File.ReadAllLines(baseDir + "input.txt");



            ChromeDriver driver = new ChromeDriver(options);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            try
            {
                for (int i = 0; i < dataa.Length; i++)
                {
                    try
                    {
                        driver.Navigate().GoToUrl(dataa[i].Split(' ')[1]);
                        for (int j = 1; j < 6; j++)
                        {
                            IWebElement WlAddr = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/section/main/div/div/div/div[2]/div/div/section[1]/div/div[7]/div[2]/div/div[" + (j * 2).ToString() + "]/div/div/span[3]/div/div/a")));
                            File.AppendAllText(baseDir + "output.txt", i.ToString() + "|" + WlAddr.GetAttribute("href") + '\n');
                        }

                    }
                 


                  /*  try
                    {
                        driver.Navigate().GoToUrl(dataa[i].Split(' ')[1]);
                        IWebElement WlAddr = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/section/main/div/div/div/div[2]/div/div/section[1]/div/div[7]/div[2]/div/div[2]/div/div/span[3]/div/div/a")));
                        File.AppendAllText(baseDir + "output.txt", i.ToString() + "|" + WlAddr.GetAttribute("href") + '\n');
                    }*/
                    catch (Exception)
                    {

                        File.AppendAllText(baseDir + "output.txt", i.ToString() + '\n');

                    }

                }

            }
            catch (Exception)
            {

            }


            Thread.Sleep(2000);

            driver.Quit();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var options = new ChromeOptions();

            string baseDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ref");
            baseDir += "\\";
            string outputPath = baseDir + "output.txt";

            options.AddArgument(@"user-data-dir=C:\Users\Surface\Desktop\fuel_chrome\profile\");
            
            //options.AddArguments("--incognito");
            options.AddArguments(@"--load-extension=C:\Users\Surface\Desktop\fuel_chrome\fuel__testnet\main\bin\Debug\ref\metamask\nkbihfbeogaeaoehlefnkodbefgpgknn");


            options.AddArguments("start-maximized");
            string[] dataa = System.IO.File.ReadAllLines(baseDir + "input.txt");


            
            for (int i = 38; i < dataa.Length; i++)
            {
                options.AddArgument("profile-directory=Profile 0");
                ChromeDriver driver = new ChromeDriver(options);
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                Thread.Sleep(1500);
                try
                {
                    importWallet(driver, wait, dataa[i]);
                    closeOtherTabs(driver);
                    addChain(driver, wait);
                    faucetToken(driver, wait);
                    sendFund(driver, wait, "0x06a0f0fa38ae42b7b3c8698e987862afa58e90d9", "0xA376f8Ad2e14A54f688Ea9166398312Aa714DCA0");
                    exportPrivateKey(driver, wait, dataa[i]);
                }
                catch (Exception)
                {
                    File.AppendAllText(baseDir + "output.txt", i.ToString() + dataa[i] +'\n');
                }
                Thread.Sleep(2000);

                driver.Quit();
                System.IO.Directory.Delete(@"C:\Users\Surface\Desktop\fuel_chrome\profile\Profile 0\Local Extension Settings\hclapmmflpipgpepofmgdohlgmbnckhd", true);
                Thread.Sleep(2000);


            }


        }

        private void closeOtherTabs(ChromeDriver driver)
        {
            string currentWindow = driver.CurrentWindowHandle;
            foreach (string WH in driver.WindowHandles)
            {
                if (WH != currentWindow)
                {
                    driver.SwitchTo().Window(WH);
                    driver.Close();
                }
            }
            driver.SwitchTo().Window(currentWindow);

           
        }
        private void faucetToken(ChromeDriver driver, WebDriverWait wait)
        {
            bool isDone = false;
           
            try
            {
                while (!isDone)
                {
                    try
                    {
                        Thread.Sleep(2000);
                        driver.Navigate().GoToUrl("https://dashboard.testnet.nulink.org/");
                        Thread.Sleep(3000);
                        //wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("/html/body/div/div[3]/div[1]/div/svg")));
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[1]/div/div/div[2]"))).Click();
                        Thread.Sleep(1000);
                        confirmTrs(driver, wait, "/html/body/div[1]/div/div/div/div[3]/div[2]/footer/button[2]", "/html/body/div[1]/div/div/div/div[3]/div[2]/footer/button[2]");
                        Thread.Sleep(3000);
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#root > div:nth-child(1) > div > div > div.faucet-logo.mar-lr-8"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/div[1]/div/div[2]/div/div[2]"))).Click();
                        confirmTrs(driver, wait, "", "/html/body/div[1]/div/div/div/div[3]/div[3]/footer/button[2]");
                        Thread.Sleep(3000);
                        isDone = true;
                    }
                    catch (Exception)
                    {}
               
                }
            }
            catch (Exception)
            {}
        }
        private bool importWallet(ChromeDriver driver, WebDriverWait wait, string seed)
        {
            try
            {
                driver.Navigate().GoToUrl("chrome-extension://hclapmmflpipgpepofmgdohlgmbnckhd/home.html#onboarding/welcome");

                string currentWindow = driver.CurrentWindowHandle;
                driver.SwitchTo().Window(currentWindow);

                Thread.Sleep(500);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/ul/li[1]/div"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/ul/li[3]/button"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div/button[1]"))).Click();
                string[] seeds = seed.Split(' ');
                for (int index = 1; index < 13; index++)
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[4]/div/div/div[3]/div["+ index.ToString() +"]/div[1]/div/input"))).SendKeys(seeds[index-1]);
                }
                Thread.Sleep(500);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[4]/div/button"))).Click();

                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/form/div[1]/label/input"))).SendKeys("Lop09876@");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/form/div[2]/label/input"))).SendKeys("Lop09876@");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/form/div[3]/label"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/form/button"))).Click();
                Thread.Sleep(2000);
                wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[1]")));
                Thread.Sleep(1000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/button"))).Click();
                Thread.Sleep(1000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/button"))).Click();
                Thread.Sleep(1000);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div/div/div[2]/button"))).Click();


            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private bool sendFund(ChromeDriver driver, WebDriverWait wait, string tokenAddress, string wlAddress)
        {
            try
            {
                bool isDone = false;
                while (!isDone)
                {
                    try
                    {
                        driver.Navigate().GoToUrl("chrome-extension://hclapmmflpipgpepofmgdohlgmbnckhd/home.html");
                        try
                        {
                            Thread.Sleep(2000);
                            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/section/div[1]/div/button"))).Click();
                        }
                        catch (Exception)
                        { }
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div/div/div[2]/div/ul/li[1]/button"))).Click();

                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div/div/div[2]/div/div/div/div[3]/div/div[1]/button"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/section/div[2]/div/div/div/div/div[2]/div/div/input"))).SendKeys(tokenAddress);
                        Thread.Sleep(2000);
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[3]/div[3]/div/section/div[2]/div/div/div/div/div[2]/div[2]/div[2]/div/input")));

                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/section/div[3]/button"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/section/div[3]/button[2]"))).Click();


                    }
                    catch (Exception)
                    {
                        driver.Navigate().GoToUrl("chrome-extension://hclapmmflpipgpepofmgdohlgmbnckhd/home.html");
                    }

                    try
                    {
                        while (wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div/div/div[2]/div/div/div/div[2]/div/a/div[2]/p"))).Text.Split(' ')[0] == "0")
                        {

                        }
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div/div/div[2]/div/div/div/div[2]/div/a"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div[2]/div[2]/button[2]"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div[2]/div/input"))).SendKeys(wlAddress);
                        Thread.Sleep(2000);

                        wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[1]/div/div[3]/div/div[3]/div/div[1]/div[2]/div/div/div/div[2]/div[2]/div")));

                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div[3]/div/div[2]/div[1]/button/div"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div[4]/footer/button[2]"))).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[3]/div/div[3]/div[3]/footer/button[2]"))).Click();
                        isDone = true;
                    }
                    catch (Exception)
                    {}
                    
                }

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private void addChain(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                driver.Navigate().GoToUrl("https://testnet.bscscan.com/");
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/footer/div/div[1]/div[2]/button"))).Click();
                }
                catch (Exception)
                {}
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/footer/div/div[1]/div[2]/button"))).Click();
                confirmTrs(driver, wait, "", "/html/body/div[1]/div/div/div/div[2]/div/button[2]");
                Thread.Sleep(1500);
                confirmTrs(driver, wait, "", "/html/body/div[1]/div/div/div/div[2]/div/button[2]");

            }
            catch (Exception)
            {}
            
        }
        private void confirmTrs(ChromeDriver driver, WebDriverWait wait, string button1, string button2)
        {
            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                string currentWindow = driver.CurrentWindowHandle;

                bool isConfirm = false;
                while (!isConfirm)
                {
                    foreach (string WH in driver.WindowHandles)
                    {
                        if (WH != currentWindow)
                        {
                            driver.SwitchTo().Window(WH);
                            try
                            {
                                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
                                /*try
                                {

                                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div/div/form/div/div/input"))).SendKeys("Lop09876@");
                                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div/div/div/button"))).Click();
                                    Thread.Sleep(1000);
                                }
                                catch (Exception)
                                { }*/
                                try
                                {
                                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(button1))).Click();
                                    Thread.Sleep(1500);
                                }
                                catch (Exception)
                                { }
                                try
                                {
                                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(button2))).Click();
                                    isConfirm = true;
                                }
                                catch (Exception)
                                { }

                            }
                            catch (Exception)
                            {
                            }
                            if (isConfirm)
                                break;
                        }


                    }
                }
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                driver.SwitchTo().Window(currentWindow);
            }
            catch (Exception)
            {  }
            
        }
        private void exportPrivateKey(ChromeDriver driver, WebDriverWait wait, string seed)
        {
            try
            {
                driver.Navigate().GoToUrl("chrome-extension://hclapmmflpipgpepofmgdohlgmbnckhd/home.html");
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/section/div[1]/div/button"))).Click();
                }
                catch (Exception)
                { }
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div[2]/div/div/button"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[1]/div/div[2]/div/div[2]/div[2]/button[1]"))).Click();
                Thread.Sleep(100);
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/section/div[2]/button"))).Click();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/section/div[3]/div/input"))).SendKeys("Lop09876@");
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/section/div[5]/button[2]"))).Click();
                WebElement clickable = (WebElement)wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/section/div[2]/button/span")));
                new Actions(driver)
                        .ClickAndHold(clickable)
                        .Perform();
                string privateKey = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[3]/div[3]/div/section/div[3]/p"))).Text;
                File.AppendAllText(@"C:\Users\Surface\Desktop\fuel_chrome\fuel__testnet\main\bin\Debug\ref\output.txt", seed + "|" + privateKey + '\n');

            }
            catch (Exception)
            {}
        }

    }
}
        