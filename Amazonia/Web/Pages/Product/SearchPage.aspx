<%@ Page Title=""Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Product.SearchPage" %>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
    <div>
        <section>
            <!-- Lista de categorias. Por defecto, desmarcada-->
            <asp:Label ID="lblCategories" runat="server" meta:resourcekey="lblCategories"></asp:Label>
            <asp:DropDownList ID="comboCategory" runat="server"></asp:DropDownList>
            <!-- Recuadro para introducir la palabra clave de búsqueda-->
            <asp:TextBox ID="tbSearchProduct" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSearchProduct" runat="server" ControlToValidate="tbSearchProduct" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"></asp:RequiredFieldValidator>
            <!-- Botón para buscar productos-->
            <asp:Button ID="btnSearchProduct" runat="server" Text="Button" OnClick="SearchButton_Click" meta:resourcekey="btnSearchProduct" />
        </section>
    </div>
    <br>
    <div class="div_text_center">
        <section>
            <center>
                <asp:GridView ID="gvProducts" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Name" meta:resourcekey="bfName" HeaderText="Name"/>
                        <asp:BoundField DataField="Category" meta:resourcekey="bfCategory" HeaderText="Category"/>
                        <asp:BoundField DataField="Entry date" meta:resourcekey="bfDate" HeaderText="Entry date"/>
                        <asp:BoundField DataField="Price" meta:resourcekey="bfPrice" HeaderText="Price"/>
                        <asp:BoundField DataField="Link add to cart" meta:resourcekey="bfAddToCart" HeaderText="Link add to cart"/>
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
            </center>
        </section>
    </div>

    <br>
    <!-- "Previous" and "Next" links. -->
    <div>
        <section>
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" Text="<%$ Resources:Common, Previous %>" runat="server"
                    Visible="False"></asp:HyperLink>
            </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" Text="<%$ Resources:Common, Next %>" runat="server" Visible="False"></asp:HyperLink>
            </span>
        </section>
    </div>

    </form>
</asp:Content>
