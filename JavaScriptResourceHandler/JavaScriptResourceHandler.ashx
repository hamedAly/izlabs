<%@ WebHandler Language="C#" Class="JavaScriptResourceHandler" %>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;

public class JavaScriptResourceHandler : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        var requestedCulture = new CultureInfo(context.Request.QueryString["locale"]);
        var classKey = context.Request.QueryString["classKey"];

        var dictionary = ReadResources(classKey, requestedCulture);

        var javaScriptSerializer = new JavaScriptSerializer();
        var script =
            @"
if (typeof(Resources) == ""undefined"") Resources = {};
Resources." + classKey + " = " +
            javaScriptSerializer.Serialize(dictionary) + ";";

        context.Response.ContentType = "application/javascript";

        context.Response.Expires = 43200; // 30 days
        context.Response.Cache.SetLastModified(DateTime.UtcNow);

        context.Response.Write(script);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private static Dictionary<object, object> ReadResources(string classKey,
                                                       CultureInfo requestedCulture)
    {
        var resourceManager = new ResourceManager("Resources." + classKey,
                                            Assembly.Load("App_GlobalResources"));
        using (var resourceSet =
            resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true))
        {

            return resourceSet
                .Cast<DictionaryEntry>()
                .ToDictionary(x => x.Key,
                     x => resourceManager.GetObject((string)x.Key, requestedCulture));
        }

    }
}