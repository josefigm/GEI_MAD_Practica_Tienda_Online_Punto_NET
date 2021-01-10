<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="FinishedSalePage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Sale.FinishedSalePage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <center>
        <h1>
            <asp:localize ID="lclFinishedMsg" runat="server" meta:resourcekey="lclFinishedMsg"></asp:localize>
            <asp:Localize ID="lclSaleId" runat="server"></asp:Localize>
        </h1>

        <asp:HyperLink ID="lclPageTitle" runat="server" NavigateUrl="~/Pages/MainPage.aspx" meta:resourcekey="lclPageTitle" />
    </center>
    
</asp:Content>
