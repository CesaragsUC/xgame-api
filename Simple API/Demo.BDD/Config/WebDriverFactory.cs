using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace Demo.BDD.Test.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Navegador browser, bool headless)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Navegador.Chrome:
                    var options = new ChromeOptions();
                    if (headless)
                        options.AddArgument("--headless");

                    webDriver = new ChromeDriver(options);
                    break;
                case Navegador.Firefox:
                    var optionsFireFox = new FirefoxOptions();
                    if (headless)
                        optionsFireFox.AddArgument("--headless");

                    webDriver = new FirefoxDriver(optionsFireFox);
                    break;
                default:
                    break;
            }
            return webDriver;
        }

    }
}
