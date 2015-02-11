<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Site.Controls.Home" %>

<div class="big-baner">
    <img src="/res/img/baner_1.png" alt="QOffice" title="QOffice" />
</div>

<div class="view1-content">
    <% foreach (var _loop in Model.WebPageData) { %>
    <% if (!string.IsNullOrEmpty(_loop.Title)) { %><h2><%= _loop.Title%></h2><%} %>
    <%= _loop.Data %>
    <%} %>
</div>