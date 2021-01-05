<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="CommentsPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Comment.CommentsPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    
    <asp:Label ID="lblNoResults" runat="server" Text="Label" Visible="false" meta:resourcekey="lblNoResults"></asp:Label>


    <form id="form1" runat="server">
    <br>    
    <div class="div_text_center">
        <section>
            <center>
                <asp:GridView ID="gvComments" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="clientLogin" meta:resourcekey="bfClientLogin" HeaderText="Author"/>
                        <asp:BoundField DataField="date" meta:resourcekey="vfDate" HeaderText="Date"/>
                        <asp:BoundField DataField="value" meta:resourcekey="bfValue" HeaderText="Comment"/> 
                        <asp:BoundField DataField="formattedLabels" meta:resourcekey="bfLabels" HeaderText="Labels"/> 
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
