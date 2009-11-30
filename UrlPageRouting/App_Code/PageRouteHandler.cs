using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.Compilation;

/// <summary>
/// Summary description for PageRouteHandler
/// </summary>
public class PageRouteHandler : IRouteHandler, IHttpHandler
{
    private readonly string _virtualPath;
    public static object RouteData = new object();

    public PageRouteHandler(string virtualPath)
    {
        _virtualPath = virtualPath;
    }



    #region IRouteHandler Members

    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
        HttpContext.Current.Items[RouteData] = requestContext.RouteData;
        return this;
    }

    #endregion

    #region IHttpHandler Members

    public bool IsReusable
    {
        get { return false; }
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Server.Transfer(_virtualPath, true);
    }

    #endregion
}
