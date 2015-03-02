<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Grid.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Admin.Controls.General.Grid" %>

<div class="panel panel-default">
    <div class="panel-heading">
        <%=this.TableHeader %>
    </div>
    <div class="panel-body">
        <div class="dataTable_wrapper">
            <table class="table table-striped table-bordered table-hover" id="grid_<%=this.ID %>">
                <thead>
                    <tr>
                    <%foreach (Inisoft.Core.PropertyDefinition propertyDefinition in this.GetDisplayedProperties())
                        {%>
                        <th><%=propertyDefinition.Title%></th>
                    <%} %>
                    <th></th>
                    </tr>
                </thead>
                <tbody>
                <%var index = 0; %>
                <%foreach (Inisoft.Core.Provider.GenericObject obj in this.Data)
                    {%>
                    <tr class='<%= ((index%2)==0) ? "odd" : "even" %> gradeX'>
                    <%foreach (Inisoft.Core.PropertyDefinition propertyDefinition in this.GetDisplayedProperties())
                        {%>
                        <td><%=obj.GetValue(propertyDefinition.Name, string.Empty)%></td>
                    <%} %>
                        <td>
                            <a href="<%=Inisoft.Web.UrlHelper.GetDetailUrl(this.UrlContext, obj) %>" class="btn btn-default btn-circle"><i class="fa fa-edit"></i></a>
                        </td>
                    </tr>
                <%} %>
                </tbody>
            </table>
        </div>
    </div>
</div>
