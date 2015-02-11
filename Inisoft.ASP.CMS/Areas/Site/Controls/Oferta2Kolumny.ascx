<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Oferta2Kolumny.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Site.Controls.Oferta2Kolumny" %>
<div class="view1-oferta2kolumny">
    <% var _counter = 0; %>
    <% var _width = Model.WebPageData.Count > 0 ? 97 / Math.Min(Model.WebPageData.Count, 2) : 97; %>
    <% foreach (var _loop in Model.WebPageData) { %>
        <%_counter++; %>
        <div style="width:<%=_width %>%;"><% if (!string.IsNullOrEmpty(_loop.Title)) { %><h2><%= _loop.Title%></h2><%} %><%= _loop.Data %></div>
        <%if ((_counter % 2) == 0)
          { %><div class="cls"></div><%} %>
    <%} %>
    <div class="cls"></div>
</div>