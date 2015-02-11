<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Site.Controls.Menu" %>

<ul id="menu">              
<% foreach (var _loopMenuData in this.Model.MenuDataList)
{%><li><% if (_loopMenuData.IsActive) { %> <a href="<%= _loopMenuData.URL %>"><img src="/res/img/<%= _loopMenuData.Icon %>_1.png" alt="<%= _loopMenuData.Title %>" /></a> <%} else {%><a href="<%= _loopMenuData.URL %>"><img src="/res/img/<%= _loopMenuData.Icon %>_0.png" alt="<%= _loopMenuData.Title %>" /></a> <%} %></li> <%} %>
</ul>
