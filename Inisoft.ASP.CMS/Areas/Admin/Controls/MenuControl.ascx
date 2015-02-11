<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuControl.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Admin.Controls.MenuControl" %>

 <div class="menu">            
    <ul>              
        <li><%=Html.Link("admin", "index", "Administracja", "selected") %></li>
        <li><%=Html.Link("admin", "website", "index", "Portal", "selected")%></li>
        <li><%=Html.Link("admin", "webpage", "index", "Strony", "selected")%></li>
        <li><%=Html.Link("admin", "article", "index", "Artykuły", "selected")%></li>
        <li><%=Html.Link("admin", "menu", "index", "Menu", "selected")%></li>
    </ul>            
</div>