<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="SaleLinesPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Sales.SaleLinesPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

     <div> 
        <form runat="server">
            <section>
                <center>
                    <asp:GridView ID="GvListLinesSales" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" EmptyDataText="<%$ Resources:, EmptyGridViewSalesLines %>" >
                        <Columns>                
                            <asp:BoundField DataField="productName" HeaderText="<%$ Resources:, productName %>" />
                            <asp:BoundField DataField="units" HeaderText="<%$ Resources:, units %>" />
                            <asp:BoundField DataField="price" HeaderText="<%$ Resources:, price %>" />         
                            <asp:checkboxfield Datafield="gift" headertext="<%$ Resources:, gift %>"/>
                        </Columns>  
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </center>
            </section>
        </form>
    </div>

</asp:Content>
