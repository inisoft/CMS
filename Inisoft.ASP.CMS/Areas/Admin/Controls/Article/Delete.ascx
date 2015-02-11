<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delete.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Admin.Controls.Article.Delete" %>

<fieldset>
    <legend>Kasowanie artykułu</legend>
    <div class="editor-label">
        <%=Html.ValidationMessageFor()%>
    </div>

    <div class="display-label"><%=Html.LabelFor(model => model.Id) %></div>
    <div class="display-field"><%: Model.Id %></div>

    <div class="display-label"><%=Html.LabelFor(model => model.Title) %></div>
    <div class="display-field"><%: Model.Title %></div>
            
    <div class="display-label"><%=Html.LabelFor(model => model.CreateDate) %></div>
    <div class="display-field"><%: Model.CreateDate%></div>
    
    <div class="display-label"><%=Html.LabelFor(model => model.Short) %></div>
    <div class="display-field"><%: Model.Short%></div>
            
    <p>
        <%= Html.Link_Index("article")%>
        <input type="submit" value="Usuń" />
    </p>
</fieldset>
