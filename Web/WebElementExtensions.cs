using OpenQA.Selenium;

namespace Web;

public static class WebElementExtensions
{
    public static void Highlight(this IWebElement element)
    {
        if (element == null)
            return;
        var px = 3;
        var color = "red";
        try
        {
            ((IJavaScriptExecutor) element)
                .ExecuteScript("arguments[0].style.border='" + px + "px solid " + color + "'", element);
        }
        catch
        {
        }
    }

    public static bool IsDisplayedInside(this IWebElement me, IWebElement target)
    {
        return
            me.Displayed &&
            me.Location.X >= target.Location.X &&
            me.Location.Y >= target.Location.Y &&
            me.Location.X + me.Size.Width <= target.Location.X + target.Size.Width &&
            me.Location.Y + me.Size.Height <= target.Location.Y + target.Size.Height;
    }

    public static bool IsDisplayedToTheLeftOf(this IWebElement me, IWebElement target)
    {
        return
            me.Displayed &&
            me.Location.X < target.Location.X;
    }

    public static bool IsDisplayedToTheRightOf(this IWebElement me, IWebElement target)
    {
        return
            me.Displayed &&
            me.Location.X > target.Location.X;
    }
}