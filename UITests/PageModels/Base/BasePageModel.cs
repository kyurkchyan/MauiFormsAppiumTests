using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using UITests.Extensions;
using Xunit.Abstractions;

namespace UITests.PageModels.Base;

public abstract class BasePageModel
{
    private const string AndroidIdPrefix = "com.ewsgroup.msoisales.maui:id/";
    public Lazy<AppiumDriver<AppiumWebElement>> LazyDriver { get; set; }

    protected AppiumDriver<AppiumWebElement> Driver
    {
        get => LazyDriver.Value;
    }

    public ITestOutputHelper TestOutputHelper { get; }

    protected BasePageModel(ITestOutputHelper testOutputHelper)
    {
        TestOutputHelper = testOutputHelper;
    }

    protected AppiumWebElement FindElementByAccessibilityId(string accessibilityId, TimeSpan? waitDuration = null)
    {
        if(Driver.PlatformName == "Android")
        {
            return FindElement(driver => driver.FindElementById(accessibilityId), waitDuration);
        }

        return FindElement(driver => driver.FindElementByAccessibilityId(accessibilityId), waitDuration);
    }

    protected AppiumWebElement FindElementById(string id, TimeSpan? waitDuration = null)
    {
        return FindElement(driver => driver.FindElementById(id), waitDuration);
    }

    protected AppiumWebElement FindElementByName(string name, TimeSpan? waitDuration = null)
    {
        return FindElement(driver => driver.FindElementByName(name), waitDuration);
    }

    protected AppiumWebElement FindElementByXPath(string xpath, TimeSpan? waitDuration = null)
    {
        return FindElement(driver => driver.FindElementByXPath(xpath), waitDuration);
    }

    protected ReadOnlyCollection<AppiumWebElement> FindElementsByXPath(string xpath, TimeSpan? waitDuration = null)
    {
        return FindElements(driver => driver.FindElementsByXPath(xpath), waitDuration);
    }

    protected AppiumWebElement ScrollToElementIos(AppiumWebElement scrollContainer, string targetElementAccessibilityId)
    {
        while(true)
        {
            try
            {
                return Driver.FindElementByAccessibilityId(targetElementAccessibilityId);
            }
            catch(Exception)
            {
                var dictionary = new Dictionary<string, object>
                                 {
                                     { "direction", "up" },
                                     { "element", scrollContainer.Id }
                                 };
                Driver.ExecuteScript("mobile: swipe", dictionary);
            }
        }
    }

    protected AppiumWebElement ScrollToElementAndroid(AppiumWebElement scrollContainer,
                                                      string targetElementAccessibilityId)
    {
        string elementId = GetAndroidAccessibilityId(targetElementAccessibilityId);
        var uiScrollable =
            $"new UiScrollable(new UiSelector().scrollable(true).instance(0)).scrollIntoView(new UiSelector().resourceIdMatches(\"{elementId}\").instance(0))";

        AppiumWebElement? element = scrollContainer.FindElement(MobileBy.AndroidUIAutomator(uiScrollable));
        return element;
    }

    protected void AcceptDialog()
    {
        Driver.OnPlatform(() => Driver.SwitchTo().Alert().Accept(),
                          () => Driver.SwitchTo().Alert().Accept(),
                          () => FindElementByAccessibilityId("PrimaryButton", TimeSpan.FromSeconds(10)).Click(),
                          () => Driver.SwitchTo().Alert().Accept())();
    }

    protected string GetAndroidAccessibilityId(string accessibilityId)
    {
        return $"{AndroidIdPrefix}{accessibilityId}";
    }

    protected void HideKeyboard()
    {
        switch(Driver.PlatformName)
        {
            case "Android":
            case "iOS":
            {
                Driver.HideKeyboard();
                break;
            }
        }
    }

    protected bool IsElementPresentById(Func<AppiumWebElement> findElement)
    {
        try
        {
            findElement();
            return true;
        }
        catch(NoSuchElementException)
        {
            return false;
        }
        catch(WebDriverTimeoutException)
        {
            return false;
        }
    }

    private AppiumWebElement FindElement(Func<AppiumDriver<AppiumWebElement>, AppiumWebElement> findElement,
                                         TimeSpan? waitDuration = null)
    {
        if(waitDuration != null)
        {
            DefaultWait<AppiumDriver<AppiumWebElement>> wait = InitDefaultWait(waitDuration);
            wait.Until(findElement);
        }

        return findElement(Driver);
    }

    private ReadOnlyCollection<AppiumWebElement> FindElements(Func<AppiumDriver<AppiumWebElement>, ReadOnlyCollection<AppiumWebElement>> findElements,
                                                              TimeSpan? waitDuration = null)
    {
        if(waitDuration != null)
        {
            DefaultWait<AppiumDriver<AppiumWebElement>> wait = InitDefaultWait(waitDuration);
            wait.Until(findElements);
        }

        return findElements(Driver);
    }
    private DefaultWait<AppiumDriver<AppiumWebElement>> InitDefaultWait([DisallowNull] TimeSpan? waitDuration)
    {

        TimeSpan pollingInterval = TimeSpan.FromMicroseconds(waitDuration.Value.Microseconds / 10.0);
        var wait = new DefaultWait<AppiumDriver<AppiumWebElement>>(Driver)
                   {
                       Timeout = waitDuration.Value,
                       PollingInterval = pollingInterval
                   };
        wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        return wait;
    }
}