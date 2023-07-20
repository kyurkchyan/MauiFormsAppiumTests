using OpenQA.Selenium.Appium;

namespace UITests.Drivers
{
    public interface IAppiumDriver : IDisposable
    {
        AppiumDriver<AppiumWebElement>? Driver { get; }
        void StartApp();
        void StopApp();
        void CloseApp();
    }
}
