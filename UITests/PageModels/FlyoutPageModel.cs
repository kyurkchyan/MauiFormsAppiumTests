using UITests.PageModels.Base;
using Xunit.Abstractions;

namespace UITests.PageModels;

public class FlyoutPageModel : BasePageModel
{
    public FlyoutPageModel(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }
    
    public void GoToPage1()
    {
        FindElementByAccessibilityId("Page1Button").Click();
    }
    
    public void GoToPage2()
    {
        FindElementByAccessibilityId("Page2Button").Click();
    }
    
    public void WaitForPage1()
    {
        FindElementByAccessibilityId("Page1Label", TimeSpan.FromSeconds(2));
    }
    
    public void WaitForPage2()
    {
        FindElementByAccessibilityId("Page2Label", TimeSpan.FromSeconds(2));
    }
}