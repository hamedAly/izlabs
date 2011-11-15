<%@ Page Language="C#" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Threading" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JavaScriptResourceHandler Demo</title>
    <script type="text/javascript" src="JavaScriptResourceHandler.ashx?classKey=StringResources&locale=<%=CultureInfo.CurrentUICulture %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <p>
    Swith locale: <a href="Default.aspx?l=en-US">English</a> <a href="Default.aspx?l=ru-RU">Russian</a>
    </p>
    <p>
    <asp:Literal runat="server" Text='<%$ Resources:StringResources, ClickTheButton %>'></asp:Literal>
    <button type="button"" onclick="alert(Resources.StringResources.Hello)"><%= HttpContext.GetGlobalResourceObject("StringResources", "SayHello") %></button>
    </p>
    </form>
</body>
</html>
