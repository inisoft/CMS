<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Blank.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Inisoft.ASP.CMS.Areas.Admin.Login" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" >
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <div class="container">
                <div class="row">
                    <div class="col-md-4 col-md-offset-4">
                        <div class="login-panel panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Logowanie</h3>
                            </div>
                            <div class="panel-body">
                                <fieldset>
                                    <div class="form-group">
                                        <asp:TextBox ID="UserName" runat="server" CssClass="form-control" placeholder="E-mail" name="email" type="email" autofocus="true"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="Password" runat="server" CssClass="form-control" TextMode="Password" placeholder="Hasło"></asp:TextBox>
                                    </div>
                                    <div class="checkbox">
                                        <label>
                                            <asp:CheckBox ID="RememberMe" runat="server" Text="Zapamiętaj mnie"/>
                                        </label>
                                    </div>
                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Zaloguj" CssClass="btn btn-lg btn-success btn-block"/>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </LayoutTemplate>
    </asp:Login>
</asp:Content>
