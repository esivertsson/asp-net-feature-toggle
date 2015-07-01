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
            <% if (FeatureToggle.Check("featureA"))
               { %>
                On
            <% } else { %>
                Off
            <% } %>
             </h1>
            (turned on in web.config)
            
            <h1>Feature B:
            <% if (FeatureToggle.Check("featureB"))
               { %>
                On
            <% } else { %>
                Off
            <% } %>
             </h1>
            (turned off in web.config)
            
            <h1>Feature C:
            <% if (FeatureToggle.Check("featureC"))
               { %>
                On
            <% } else { %>
                Off
            <% } %>
             </h1>
             (defined in web.config, but no toggle-value set)
            
            <h1>Undefined feature:
            <% if (FeatureToggle.Check("featureUndefined"))
               { %>
                On
            <% } else { %>
                Off
            <% } %>
             </h1>
             (undefined in web.config)
            
            <asp:Label id="featureD" runat="server">
                <h1>Feature D</h1>
                (turned on in web.config, but toggle is checked in code-behind)
            </asp:Label>
        </form>
    </body>
</html>
