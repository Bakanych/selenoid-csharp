namespace Web.UIModel;

public abstract class BaseWebComponent
{
    public BaseWebComponent WaitUntilLoaded()
    {
        WaitUntilLoadedLogic();
        return this;
    }

    protected virtual void WaitUntilLoadedLogic()
    {
    }
}