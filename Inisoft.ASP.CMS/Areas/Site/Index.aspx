<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Site/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Inisoft.ASP.CMS.Areas.Site.Index" %>

<asp:Content ID="Content8" ContentPlaceHolderID="HeadInlineJavaScriptContent" runat="server"><%= Model.HeadInlineJavaScript %></asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="HeadInlineStyleContent" runat="server"><%= Model.HeadInlineStyle %></asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="HeadMetaKeywordContent" runat="server"><%= Model.HeadMetaKeyword %></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadMetaDescriptionContent" runat="server"><%= Model.HeadMetaDescription %></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="HeadShortcutIconContent" runat="server"><%= Model.HeadShortcutIcon %></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadMetaDataContent" runat="server"><%= Model.HeadMetaData %></asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server"><%= Model.Title %></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-left">
        <div id="menucontainer"><asp:PlaceHolder runat="server" ID="menuContainer"  /></div>
        <div class="line-up"></div>
        <div id="note"><div class="note-text"><%=Model.YellowPage %></div></div>
        <div class="address">
        <strong>Qoffice</strong>
        Kompleksowa Obsługa Firmy<br />
        ul. Szwedzka 3 lok. 13<br />
        54-401 Wrocław<br />
        tel. 608-45-25-33
        </div>
    </div>
    <div class="col-right"><div id="content"><%=Model.YellowPage %><asp:PlaceHolder runat="server" ID="contentContainer" /></div></div>
    <div class="cls"></div>
</asp:Content>
