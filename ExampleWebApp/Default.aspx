<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ExampleWebApp.Default" %>
<%@ Import Namespace="AspNetFeatureToggle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>FeatureToggle test page</title>
    </head>
    <body>
        <form id="HtmlForm" runat="server">
            
            <h1>Feature A:
            <% if (FeatureToggle.IsEnabled("featureA"))
               { %>
                On
            <% } else { %>
                Off
            <% } %>
             </h1>
            (turned on in web.config)
            
            <h1>Feature B:
            <% if (FeatureToggle.IsEnabled("featureB"))
               { %>
                On
            <% } else { %>
                Off
            <% } %>
             </h1>
            (turned off in web.config)
            
            <h1>Feature C: <asp:Label id="featureCLabel" runat="server"></asp:Label></h1>
            (turned on in web.config, but with a list of specific users and is checked in code-behind)<br/>
            Try a username (UserA or UserB):
            <asp:TextBox ID="featureCTextBox" AutoPostBack="true" runat="server"></asp:TextBox>
            
            <asp:Label id="featureD" runat="server">
                <h1>Feature D: On</h1>
                (turned on in web.config, but value is checked in code-behind)
            </asp:Label>
            
            <h1>Feature E:
            <% if (FeatureToggle.IsEnabled("featureE"))
               { %>
                On
            <% } else { %>
                Off
            <% } %>
             </h1>
             (turned on and defined with random factor 50% in web.config, reload page to see value change)
            
            <h1>Undefined feature:
            <% if (FeatureToggle.IsEnabled("featureUndefined"))
               { %>
                On
            <% } else { %>
                Off
            <% } %>
             </h1>
             (undefined in web.config)
        </form>
    </body>
</html>
