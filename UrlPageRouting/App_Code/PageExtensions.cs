﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;

/// <summary>
/// Summary description for PageExtensions
/// </summary>
public static class PageExtensions
{
    public static RouteData GetRouteData(this Page page)
    {
        return (HttpContext.Current.Items[PageRouteHandler.RouteData] as RouteData) ?? new RouteData();
    }

    public static string GetRouteUrl(this Page page, string name, object values)
    {
        return GetRouteUrl(page, name, new RouteValueDictionary(values));
    }

    public static string GetRouteUrl(this Page page, string name, RouteValueDictionary values)
    {
        return RouteTable.Routes.GetVirtualPath(null, name, values).VirtualPath;
    }
}
