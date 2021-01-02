<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="ResultPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Product.ResultPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    

    <asp:Label ID="lblNoResults" runat="server" Text="Label" Visible="false" meta:resourcekey="lblNoResults"></asp:Label>


    <form id="form1" runat="server">
    <br>    
    <div class="div_text_center">
        <section>
            <center>
                <asp:GridView ID="gvProducts" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnRowCommand="GvProducts_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="productTitle" meta:resourcekey="bfName" HeaderText="Name"/>
                        <asp:BoundField DataField="id" Visible="false"/>
                        <asp:BoundField DataField="category.id" meta:resourcekey="bfCategory" HeaderText="Category"/>
                        <asp:BoundField DataField="entryDate" meta:resourcekey="bfDate" HeaderText="Entry date"/>
                        <asp:BoundField DataField="price" meta:resourcekey="bfPrice" HeaderText="Price"/>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnSeeDetail" meta:resourcekey="btnSeeDetail" runat="server" CausesValidation="false" CommandName="SeeDetail" CommandArgument='<%# Eval("id") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:Button ID="btnAddToCart" meta:resourcekey="btnAddToCart" runat="server" CausesValidation="false" CommandName="AddToCart" CommandArgument='<%# Eval("id") %>' />
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
