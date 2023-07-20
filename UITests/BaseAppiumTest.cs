using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using UITests.Drivers;
using UITests.PageModels.Base;
using Xunit.Abstractions;

namespace UITests;

public abstract class BaseAppiumTest : IDisposable
{
    protected IAppiumDriver AppiumDriver { get; }
    private readonly ITestOutputHelper _testOutputHelper;

    protected PlatformType Platform { get; }

    protected BaseAppiumTest(IAppiumDriver appiumDriver,
                             ITestOutputHelper testOutputHelper)
    {
        AppiumDriver = appiumDriver;
        _testOutputHelper = testOutputHelper;
    }

    protected T CreatePageModel<T>() where T : BasePageModel
    {
        var page = (T)Activator.CreateInstance(typeof(T), _testOutputHelper)!;
        page.LazyDriver = new Lazy<AppiumDriver<AppiumWebElement>>(() => AppiumDriver.Driver!);
        return page;
    }

    protected void LaunchApplication()
    {
        AppiumDriver.StartApp();
    }

    public void Dispose()
    {
        AppiumDriver.Dispose();
    }
}