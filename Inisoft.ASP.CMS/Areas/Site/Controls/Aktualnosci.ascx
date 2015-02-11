<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Aktualnosci.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Site.Controls.Aktualnosci" %>
<div class="view1-content">
    <% foreach (var _loop in Model.WebPageData) { %>
    <% if (!string.IsNullOrEmpty(_loop.Title)) { %><h2><%= _loop.Title%></h2><%} %>
    <%= _loop.Data %>
    <div class="h-line"></div>
    <%} %>
</div>