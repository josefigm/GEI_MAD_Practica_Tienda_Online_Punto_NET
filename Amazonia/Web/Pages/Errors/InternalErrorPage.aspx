<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="InternalErrorPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Errors.InternalErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder_MenuLinks" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <br />
    <asp:Label ID="lblErrorTitle" runat="server" meta:resourcekey="lblErrorTitle"></asp:Label>
    &nbsp;
    <br />
    <br />
    <asp:Label ID="lblRetryLater" runat="server" meta:resourcekey="lblRetryLater"></asp:Label>
    <br />
    <br />    
</asp:Content>
