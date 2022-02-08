using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using BrowserStack.WebDriver.Config;
using Platform = BrowserStack.WebDriver.Config.Platform;
using log4net;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace BrowserStack.WebDriver.Core
{

    public class DriverFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DriverFactory));
        private readonly string DEFAULT_CAPABILITIES_FILE = "capabilities.yml";
        private readonly string BROWSERSTACK_USERNAME = "BROWSERSTACK_USERNAME";
        private readonly string BROWSERSTACK_ACCESS_KEY = "BROWSERSTACK_ACCESS_KEY";
        private readonly string BUILD_ID = "BROWSERSTACK_BUILD_NAME";
        private readonly string DEFAULT_BUILD_NAME = "browserstack_examples_specflow";
        public readonly string CAPABILITIES_DIR = "/Browserstack/Webdriver/Resources/";
        private readonly WebDriverConfiguration WebDriverConfiguration;
        private readonly string DefaultBuildSuffix;
        private readonly bool IsLocal;
        static readonly object Lock = new ();

        private static DriverFactory instance;

        public DriverFactory()
        {
            this.DefaultBuildSuffix = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            this.WebDriverConfiguration = ParseWebDriverConfig();
            List<Platform> Platforms = WebDriverConfiguration.GetActivePlatforms();

            IsLocal = WebDriverConfiguration.CloudDriverConfig != null &&
                    WebDriverConfiguration.CloudDriverConfig.LocalTunnel.IsEnabled;

            if (IsLocal)
            {
                Dictionary<string, string> localOptions = WebDriverConfiguration.CloudDriverConfig.LocalTunnel.LocalOptions;

                string accessKey = Environment.GetEnvironmentVariable(BROWSERSTACK_ACCESS_KEY) ?? WebDriverConfiguration.CloudDriverConfig.Key;

                localOptions.Add("key", accessKey);

                LocalFactory.CreateInstance(WebDriverConfiguration.CloudDriverConfig.LocalTunnel.LocalOptions);
            }
            Log.Debug(("Running tests on {} active platforms.", Platforms,
                  Platforms.Count().ToString()));
        }

        public static DriverFactory GetInstance()
        {
            if (instance == null)
            {
                lock (DriverFactory.Lock)
                {
                    if (instance == null)
                    {
                        instance = new DriverFactory();
                    }
                }
            }
            return instance;
        }

        private WebDriverConfiguration ParseWebDriverConfig()
        {
            var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .Build();

            string capabilitiesConfig =
                    Environment.GetEnvironmentVariable("CAPABILITIES_FILENAME");
            

            if (capabilitiesConfig == null)
            {
                capabilitiesConfig = DEFAULT_CAPABILITIES_FILE;
                
            }

            //WebDriverConfiguration webDriverConfiguration = deserializer.Deserialize<WebDriverConfiguration>(File.ReadAllText(Directory.GetCurrentDirectory() + CAPABILITIES_DIR + capabilitiesConfig));
            WebDriverConfiguration webDriverConfiguration = deserializer.Deserialize<WebDriverConfiguration>(File.ReadAllText(Directory.GetCurrentDirectory() + CAPABILITIES_DIR + capabilitiesConfig));

            return webDriverConfiguration;
        }
        public DesiredCapabilities CreateWebPlatformCapabilities(Platform platform)
        {
            DesiredCapabilities webDriverOptions = null;

            switch (this.WebDriverConfiguration.DriverType)
            {
                case DriverType.OnPremDriver:
                    webDriverOptions = CreateOnPremWebCapabilities(platform);
                    break;
                case DriverType.CloudDriver:
                    webDriverOptions = CreateRemoteWebCapabilities(platform);
                    break;
                case DriverType.OnPremGridDriver:
                    webDriverOptions = CreateOnPremGridWebDriver(platform);
                    break;
                default:
                    break;

            }
            return webDriverOptions;
        }

        public List<Platform> GetPlatforms()
        {
            return this.WebDriverConfiguration.GetActivePlatforms();
        }

        public DesiredCapabilities CreateRemoteWebCapabilities(Platform platform)
        {
            RemoteDriverConfig remoteDriverConfig = this.WebDriverConfiguration.CloudDriverConfig;
            Capabilities commonCapabilities = remoteDriverConfig.CommonCapabilities;
            Capabilities sessionCapabilities = platform.SessionCapabilities;
            DesiredCapabilities webDriverOptions = new DesiredCapabilities();
            Dictionary<string, object> browserstackOptions = new();

            if (commonCapabilities.BStackOptions != null)
            {
                foreach (KeyValuePair<string, object> tuple in commonCapabilities.BStackOptions)
                {

                    browserstackOptions.Add(tuple.Key.ToString(), tuple.Value);

                }
            }

            if (commonCapabilities.PlatformOptions != null)
            {
                foreach (KeyValuePair<string, object> tuple in commonCapabilities.PlatformOptions)
                {
                    webDriverOptions.SetCapability(tuple.Key.ToString(), tuple.Value);
                }
            }

            if (sessionCapabilities.BStackOptions != null)
            {
                foreach (KeyValuePair<string, object> tuple in sessionCapabilities.BStackOptions)
                {
                    browserstackOptions.Add(tuple.Key.ToString(), tuple.Value);
                }
            }

            if (sessionCapabilities.PlatformOptions != null)
            {
                foreach (KeyValuePair<string, object> tuple in sessionCapabilities.PlatformOptions)
                {
                    webDriverOptions.SetCapability(tuple.Key.ToString(), tuple.Value);
                }
            }

            string user = Environment.GetEnvironmentVariable(BROWSERSTACK_USERNAME) ?? remoteDriverConfig.User;
            string accessKey = Environment.GetEnvironmentVariable(BROWSERSTACK_ACCESS_KEY) ?? remoteDriverConfig.Key;

            browserstackOptions.Add("userName", user);
            browserstackOptions.Add("accessKey", accessKey);

            if (IsLocal)
            {
                browserstackOptions.Add("localIdentifier", LocalFactory.GetInstance().GetLocalIdentifier());
            }

            object build = null;
            commonCapabilities.BStackOptions.TryGetValue("buildName", out build);

            if (build is not null)
            {
                browserstackOptions["buildName"] = CreateBuildName(build.ToString());
            }

            webDriverOptions.SetCapability("bstack:options", browserstackOptions);

            Console.WriteLine(webDriverOptions);

            return webDriverOptions;
        }

        public IWebDriver CreateRemoteWebDriver(DesiredCapabilities driverOptions)
        {
            IWebDriver driver;
            //object Os = driverOptions.ToCapabilities().GetCapability("platformName");
            //if (Os is not null && Os.ToString().ToLower().Equals(DeviceType.Desktop.ToString().ToLower()))
            //{
            driver = new RemoteWebDriver(new Uri(this.WebDriverConfiguration.CloudDriverConfig.HubUrl), driverOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
            return driver;
            //}
            //else
            //{
            //    Log.Error(String.Format("Platform {0} is not supported.Please specify one among {1} ", Os,
            //        Enum.GetValues(typeof(DeviceType))));
            //    throw new Exception(String.Format("Platform '{0}' is not supported. Please specify one among {1}", Os,
            //        Enum.GetValues(typeof(DeviceType))));
            //}
        }

        private String CreateBuildName(String buildPrefix)
        {
            if (String.IsNullOrEmpty(buildPrefix))
            {
                buildPrefix = DEFAULT_BUILD_NAME;
            }
            String buildName = buildPrefix;
            String buildSuffix = Environment.GetEnvironmentVariable(BUILD_ID);

            if (string.IsNullOrEmpty(buildSuffix))
            {
                buildSuffix = this.DefaultBuildSuffix;
            }

            return String.Format("{0}-{1}", buildName, buildSuffix);
        }

        private DesiredCapabilities CreateOnPremGridWebDriver(Platform platform)
        {

            throw new NotImplementedException("On Prem Grid IWebDriver driver is not yet implemented");
        }

        private DesiredCapabilities CreateOnPremWebCapabilities(Platform platform)
        {

            throw new NotImplementedException("On Prem IWebDriver driver is not yet implemented");
        }
    }
}