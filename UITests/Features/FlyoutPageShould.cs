using UITests.Drivers;
using UITests.PageModels;
using Xunit.Abstractions;

namespace UITests.Features;

public class FlyoutPageShould : BaseAppiumTest
{
    private readonly FlyoutPageModel _flyoutPageModel;

    public FlyoutPageShould(IAppiumDriver appiumDriver, ITestOutputHelper testOutputHelper) : base(appiumDriver, testOutputHelper)
    {
        _flyoutPageModel = CreatePageModel<FlyoutPageModel>();
    }
    
    [Fact]
    public void BeAbleToNavigateToPage1()
    {
        LaunchApplication();
        _flyoutPageModel.GoToPage1();
        _flyoutPageModel.WaitForPage1();
    }
    
    [Fact]
    public void BeAbleToNavigateToPage2()
    {
        LaunchApplication();
        _flyoutPageModel.GoToPage2();
        _flyoutPageModel.WaitForPage2();
    }
}