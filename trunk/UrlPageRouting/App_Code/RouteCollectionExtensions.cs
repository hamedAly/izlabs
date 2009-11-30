using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

/// <summary>
/// Summary description for RouteCollectionExtensions
/// </summary>
public static class RouteCollectionExtensions
{
	public static void MapPageRoute (this RouteCollection routes, string name, string url, string target) {
		routes.Add (name, new Route (url, new PageRouteHandler (target)));
	}
}
