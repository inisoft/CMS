<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Areas/Admin/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Inisoft.ASP.CMS._Default" ValidateRequest="false" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadJavaScripContent">
</asp:Content>

<asp:Content ID="HeadTitleContent" runat="server" ContentPlaceHolderID="HeadTitleContent">Smart CMS - <%=ControlHeadTitle %></asp:Content>

<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleContent"><%=ControlTitle %></asp:Content>


<asp:Content ID="NavbarTopLinksContent" runat="server" ContentPlaceHolderID="NavbarTopLinks">
    <asp:PlaceHolder runat="server" ID="NavbarTopLinksPlaceHolder" />
</asp:Content>

<asp:Content ID="NavbarDefaultContent" runat="server" ContentPlaceHolderID="NavbarDefault">
    <asp:PlaceHolder runat="server" ID="NavbarDefaultContainer" />
</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
     <asp:PlaceHolder runat="server" ID="contentContainer" />
</asp:Content>
