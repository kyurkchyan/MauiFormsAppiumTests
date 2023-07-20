using OpenQA.Selenium.Appium;

namespace UITests.Extensions;

public static class AppiumDriverExtensions
{
    public static T OnPlatform<T>(this AppiumDriver<AppiumWebElement> driver, T iOS, T android, T windows, T mac)
    {
        return driver.PlatformName switch
        {
            "iOS" => iOS,
            "Android" => android,
            "Windows" => windows,
            "Mac" => mac,
            _ => throw new NotSupportedException($"Platform {driver.PlatformName} is not supported")
        };
    }
}