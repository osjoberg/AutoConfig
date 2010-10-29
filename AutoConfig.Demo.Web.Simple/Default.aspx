<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AutoConfig.Demo.Web.Simple._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>This demo is running in the <%=HttpUtility.HtmlEncode(EnvironmentName)%> environment.</h1> 
        <p>The application setting TestSetting is set to "<%=TestSettingValue%>".</p>
    </div>
    </form>
</body>
</html>
