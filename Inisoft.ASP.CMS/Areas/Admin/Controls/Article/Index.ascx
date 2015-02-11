<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Index.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Admin.Controls.Article.Index" %>

<fieldset>
    <legend>Lista artykułów</legend>
<table>
    <tr>
        <th></th>
        <th>Id</th>
        <th>CreateDate</th>
        <th>Title</th>
        <th>Short</th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
           <%= Html.Link_Edit("article", item.Id)%> |
           <%= Html.Link_Delete("article", item.Id)%>
        </td>
        <td>
            <%: item.Id %>
        </td>
        <td>
            <%: String.Format("{0:g}", item.CreateDate) %>
        </td>
        <td>
            <%: item.Title %>
        </td>
        <td>
            <%: item.Short %>
        </td>
    </tr>
    
<% } %>

</table>

<p>
    <%= Html.Link_Create("article") %>
</p>
</fieldset>