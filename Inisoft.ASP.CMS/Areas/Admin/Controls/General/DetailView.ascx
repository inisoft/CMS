<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailView.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Admin.Controls.General.DetailView" %>

<div class="panel panel-default detail-view">
    <% if (this.Data != null) { %>
        <div class="panel-heading">
            <%= this.ObjectDefinition.Title %> (<%= this.Data.Id %>)
            <% if (!string.IsNullOrEmpty(this.ObjectDefinition.Description))
               { %>
                <button type="button" class="btn btn-default btn-circle" data-toggle="tooltip" data-placement="left" title="" data-original-title="<%= this.ObjectDefinition.Description %>"><i class="fa fa-info"></i></button>
            <% } %>
            <a href="<%=Inisoft.Web.UrlHelper.GetListUrl(this.UrlContext) %>" class="btn btn-default btn-circle"><i class="fa fa-list"></i></a>
        </div>
        <div class="panel-body">
                <%foreach (Inisoft.Core.PropertyDefinition propertyDefinition in this.GetDisplayedProperties())
                    {%>
            <div class="row">
                <div class="col-xs-12 col-sm-4 col-md-2"><%=propertyDefinition.Title%></div>
                <div class="col-xs-6 col-md-10"><%=this.Data.GetValue(propertyDefinition.Name, string.Empty)%></div>
            </div>
                <%} %>
        </div>
    <% } else { %>
    <div class="row">
        <div class="col-xs-12 col-sm-4 col-md-12">Brak danych</div>
    </div>
    <%} %>
</div>