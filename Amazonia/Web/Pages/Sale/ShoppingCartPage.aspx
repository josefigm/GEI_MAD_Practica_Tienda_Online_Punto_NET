<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="ShoppingCartPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Sale.ShoppingCartPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <center>
        <h1>
            <asp:Localize ID="lclEmptyShoppingCart" runat="server" meta:resourcekey="lclEmptyShoppingCart" Visible="false"></asp:Localize>
        </h1>
        <form id="form1" runat="server">
            <asp:GridView ID="GvShoppingCart" runat="server" AutoGenerateColumns="False" OnRowCommand="GvShoppingCart_RowCommand" >
                <Columns>
                    <asp:BoundField DataField="product.id" Visible="false"/>
                    <asp:BoundField DataField="product.productTitle" HeaderText="<%$ Resources:, product %>"/>
                    <asp:BoundField DataField="units" HeaderText="<%$ Resources:, units %>"/>
                    <asp:BoundField DataField="price" HeaderText="<%$ Resources:, price %>"/>
                    <asp:BoundField DataField="gift" HeaderText="<%$ Resources:, gift %>"/>
                    <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnDelete" meta:resourcekey="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" CommandArgument='<%# Eval("product.id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </form>
    </center>
</asp:Content>
