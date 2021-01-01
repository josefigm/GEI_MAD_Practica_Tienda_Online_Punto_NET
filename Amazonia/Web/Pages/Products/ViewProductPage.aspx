<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="ViewProductPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Products.ViewProductPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <div>
        <h2>
            <asp:Localize ID="lclProductName" runat="server" meta:resourcekey="lclProductName"></asp:Localize>
            <asp:Localize ID="productName" runat="server"></asp:Localize>
        </h2>
    </div>
    <br />
    <div class="viewProductContainer">
        <br /><br />
        <div class="productImg">
            <asp:Image ID="productImage" runat="server" />
        </div>
        <div class="productDetails">
            <div class="price">
                <u><asp:Localize ID="productPrice" runat="server"></asp:Localize></u>
            </div>
            <br />
            <div>
                <asp:Localize ID="lclProductCategory" runat="server" meta:resourcekey="lclProductCategory"></asp:Localize>
                <asp:Localize ID="productCategory" runat="server"></asp:Localize>
            </div>
            <div>
                <asp:Localize ID="lclEntryDate" runat="server" meta:resourcekey="lclEntryDate"></asp:Localize>
                <asp:Localize ID="entryDate" runat="server"></asp:Localize>
            </div>
            <div>
                <asp:Label ID="lblNoDescription" runat="server" Visible="false" meta:resourcekey="lblNoDescription"></asp:Label>
                <asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription" Visible="false"></asp:Localize>
                <asp:Localize ID="productDescription" runat="server"></asp:Localize>
            </div>
        </div>
    </div>
</asp:Content>
