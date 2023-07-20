using UITests.PageModels.Base;

namespace UITests.Extensions;

public static class BasePageModelExtensions
{
    public static TPageModel NavigateTo<TPageModel>(this BasePageModel pageModel) where TPageModel : BasePageModel
    {
        var basePageModel = (TPageModel)Activator.CreateInstance(typeof(TPageModel), pageModel.TestOutputHelper)!;
        basePageModel.LazyDriver = pageModel.LazyDriver;
        return basePageModel;
    }
}