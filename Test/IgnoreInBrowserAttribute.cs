using Core;
using NUnit.Framework;

namespace Test;

public class IgnoreInBrowserAttribute : PropertyAttribute
{
    public IgnoreInBrowserAttribute(BrowserType propertyName) : base("IgnoreInBrowser", propertyName.ToString())
    {
    }
}