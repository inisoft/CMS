<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Artykul.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Site.Controls.Artykul" %>
<div class="view1-content">
    <% foreach (var _loop in Model.WebPageData) { %>
    <% if (!string.IsNullOrEmpty(_loop.Title)) { %><h2><%= _loop.Title%></h2><%} %>
    <%= _loop.Data %>
    <%} %>
</div>