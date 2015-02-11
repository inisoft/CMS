<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Inisoft.ASP.CMS.Areas.Admin.Controls.Article.Edit" %>

<fieldset>
    <legend>Edycja artykułu</legend>
    <div class="editor-label">
        <%=Html.ValidationMessageFor()%>
    </div>
          
    <div class="editor-label">
        <%= Html.LabelFor(model => model.Title) %>
    </div>
    <div class="editor-field">
        <%= Html.TextBoxFor(model => model.Title) %>
        <%= Html.ValidationMessageFor(model => model.Title) %>
    </div>

    <div class="editor-label">
        <%= Html.LabelFor(model => model.Short) %>
    </div>
    <div class="editor-field">
        <%= Html.TextAreaFor(model => model.Short, 5, 90)%>
        <%= Html.ValidationMessageFor(model => model.Short) %>
    </div>
            
    <div class="editor-label">
        <%= Html.LabelFor(model => model.Data) %>
    </div>
    <div class="editor-field">
        <%= Html.TextAreaFor(model => model.Data, 20, 90)%>
        <%= Html.ValidationMessageFor(model => model.Data) %>
    </div>
            
    <p>
        <%= Html.Link_Index("article") %>
        <input type="submit" value="Zapisz" />
    </p>
</fieldset>
