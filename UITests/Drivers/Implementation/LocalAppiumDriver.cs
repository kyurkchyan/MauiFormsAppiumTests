﻿using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Windows;
using UITests.Configuration;

namespace UITests.Drivers.Implementation
{
    internal class LocalAppiumDriver : IAppiumDriver
    {
        private readonly AutomationConfiguration _configuration;
        public AppiumDriver<AppiumWebElement>? Driver { get; private set; }

        public LocalAppiumDriver(IOptions<AutomationConfiguration> automationConfiguration)
        {
            _configuration = automationConfiguration.Value;
        }

        public void StartApp()
        {
            if(Driver != null) Dispose();

            var driverOptions = new AppiumOptions();

            var appPath = _configuration.SampleToRun == SampleType.Forms
                              ? "../../../../FlyoutPlayground.Forms/FlyoutPlayground.Forms.iOS/bin/iPhoneSimulator/Release/FlyoutPlayground.Forms.iOS.app"
                              : "../../../../FlyoutPlayground.Maui/bin/Release/net7.0-ios/iossimulator-x64/FlyoutPlayground.Maui.app";
            appPath = Path.Combine(Directory.GetCurrentDirectory(), appPath);
            driverOptions.AddAdditionalCapability($"appium:{MobileCapabilityType.PlatformName}", "iOS");
            driverOptions.AddAdditionalCapability($"appium:{MobileCapabilityType.PlatformVersion}",
                                                  _configuration.IosMac.PlatformVersion);
            driverOptions.AddAdditionalCapability($"appium:{MobileCapabilityType.DeviceName}", _configuration.IosMac.DeviceName);
            driverOptions.AddAdditionalCapability($"appium:{MobileCapabilityType.AutomationName}", "XCUITest");
            driverOptions.AddAdditionalCapability($"appium:{MobileCapabilityType.App}", appPath);
            driverOptions.AddAdditionalCapability("derivedDataPath", $"~/Library/Developer/Xcode/DerivedData/{Guid.NewGuid()}");
            driverOptions.AddAdditionalCapability("showXcodeLog", _configuration.IosMac.ShowXcodeLog);
            driverOptions.AddAdditionalCapability("showIOSLog", _configuration.IosMac.ShowIosLog);
            driverOptions.AddAdditionalCapability($"appium:{MobileCapabilityType.NoReset}", _configuration.IosMac.NoReset);
            driverOptions.AddAdditionalCapability($"appium:{MobileCapabilityType.FullReset}", _configuration.IosMac.FullReset);
            driverOptions.AddAdditionalCapability("wdaStartupRetries", "6");
            driverOptions.AddAdditionalCapability("iosInstallPause", "8000");
            driverOptions.AddAdditionalCapability("wdaStartupRetryInterval", "20000");
            Driver = new IOSDriver<AppiumWebElement>(new Uri("http://0.0.0.0:4723"), driverOptions);
        }

        public void StopApp()
        {
            Driver?.Quit();
        }

        public void CloseApp()
        {
            Driver?.CloseApp();
        }

        public void Dispose()
        {
            StopApp();
            Driver?.Dispose();
        }
    }
}