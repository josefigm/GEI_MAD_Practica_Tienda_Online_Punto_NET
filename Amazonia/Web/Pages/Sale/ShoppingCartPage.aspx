<%@ Page Title="" Language="C#" EnableEventValidation="false"  MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="ShoppingCartPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Sale.ShoppingCartPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <center>
        <h1>
            <asp:Localize ID="lclEmptyShoppingCart" runat="server" meta:resourcekey="lclEmptyShoppingCart" Visible="false"></asp:Localize>
            <asp:Localize ID="lclShoppingCart" runat="server" meta:resourcekey="lclShoppingCart" Visible="false"></asp:Localize>
        </h1>
        <br />
        <asp:Label ID="lblInsufficientStock" runat="server" ForeColor="Red" Style="position: relative" Visible="False" meta:resourcekey="lblInsufficientStock"></asp:Label>
        <form id="form1" runat="server">
            <asp:GridView ID="GvShoppingCart" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnRowCommand="GvShoppingCart_RowCommand">
                <Columns>
                    <asp:BoundField DataField="product.id" Visible="false"/>
                    <asp:BoundField DataField="product.productTitle" HeaderText="<%$ Resources:, product %>"/>
                    <asp:TemplateField HeaderText="<%$ Resources:, units %>">
                        <ItemTemplate>
                            <asp:Textbox ID="tbUnits" runat="server" TextMode="Number" Text='<%# Bind("units") %>' HeaderText="<%$ Resources:, units %>"></asp:Textbox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="price" HeaderText="<%$ Resources:, price %>"/>
                    <asp:TemplateField HeaderText="<%$ Resources:, gift %>">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbGift" runat="server" Checked='<%# bool.Parse(Eval("gift").ToString()) %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btnModify" meta:resourcekey="btnModify" runat="server" CausesValidation="false"  CommandName="Modify" CommandArgument='<%# Eval("product.id")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" meta:resourcekey="btnDelete" runat="server" CausesValidation="false" CommandName="Delete" CommandArgument='<%# Eval("product.id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
            </asp:GridView>
        </form>
        <br />
        <div>
            <span class="lblTotalPrice">
                <asp:Localize ID="lclTotalPrice" runat="server" meta:resourcekey="lclTotalPrice" Visible="false"></asp:Localize>
            </span>
            <span class="totalPrice">
                <asp:Localize ID="totalPrice" runat="server"></asp:Localize>
            </span>
        </div>
        <br />
        <asp:HyperLink class="endSaleBtn" ID="lnkEndSale" runat="server" NavigateUrl="~/Pages/Sale/FinishSalePage.aspx" meta:resourcekey="lnkEndSale" Visible="false"/>           
        <br /><br />
    </center>
</asp:Content>
