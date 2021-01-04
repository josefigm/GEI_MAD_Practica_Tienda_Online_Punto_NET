<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="StockErrorPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Sale.StockErrorPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <center>
        <h1>
            <asp:localize ID="lclStockError" runat="server" meta:resourcekey="lclStockError"></asp:localize>
            <asp:Localize ID="lclProductName" runat="server"></asp:Localize>
            <span>, </span>
            <asp:localize ID="lclAvailableStock" runat="server" meta:resourcekey="lclAvailableStock"></asp:localize>
            <asp:Localize ID="lclStock" runat="server"></asp:Localize>
        </h1>

        <asp:HyperLink ID="lclPageTitle" runat="server" NavigateUrl="~/Pages/MainPage.aspx" meta:resourcekey="lclPageTitle" />
    </center>
</asp:Content>
