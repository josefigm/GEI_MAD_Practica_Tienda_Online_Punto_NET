<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="ViewProductPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Product.ViewProductPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <div>
        <h1>
            <asp:Localize ID="productName" runat="server"></asp:Localize>
        </h1>
    </div>
    <br />
    <div class="viewProductContainer">
        <br /><br />
        <div class="productImg">
            <asp:Image ID="productImage" runat="server" />
        </div>
        <div class="productDetails">

            <div>
                <span class="attributeName">
                    <asp:Localize ID="lclProductCategory" runat="server" meta:resourcekey="lclProductCategory"></asp:Localize>
                </span>
                <asp:Localize ID="productCategory" runat="server"></asp:Localize>
            </div>
            <div>
                <span class="attributeName">
                    <asp:Localize ID="lclEntryDate" runat="server" meta:resourcekey="lclEntryDate"></asp:Localize>
                </span>
                <asp:Localize ID="entryDate" runat="server"></asp:Localize>
            </div>
            <div>
                <span class="attributeName">
                    <asp:Label ID="lblNoDescription" runat="server" Visible="false" meta:resourcekey="lblNoDescription"></asp:Label>
                    <asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription" Visible="false"></asp:Localize>
                </span>
                <asp:Localize ID="productDescription" runat="server"></asp:Localize>
            </div>
             <div>
                <span class="attributeName">
                    <asp:Localize ID="lclStock" runat="server" meta:resourcekey="lclStock"></asp:Localize>
                </span>
                <asp:Localize ID="stock" runat="server"></asp:Localize>
            </div>
            <br>
            <div>
                <span class="attributeName">
                    <asp:Localize ID="lclPrice" runat="server" meta:resourcekey="lclPrice"></asp:Localize>
                </span>
                <span class="price">
                    <asp:Localize ID="productPrice" runat="server"></asp:Localize>
                </span>
            </div>
            <form id="form1" runat="server">
                <div class="button">
                    <span>
                        <asp:Label ID="lblProductId" runat="server" visible="false" ></asp:Label>
                        <asp:Button ID="btnEditProduct" runat="server" visible="false" meta:resourcekey="btnEditProduct" OnClick="btnEditProduct_Click"/>
                        <asp:Button ID="btnAddComent" runat="server" visible="true" meta:resourcekey="btnAddComent" OnClick="btnAddComent_Click" />
                        <asp:Button ID="btnManageComment" runat="server" visible="true" meta:resourcekey="btnManageComment" OnClick="btnManageComment_Click" />
                    </span>
                </div>
            </form>
        </div>
    </div>
</asp:Content>
