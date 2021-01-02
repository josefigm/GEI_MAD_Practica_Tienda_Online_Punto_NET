<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="MySalesPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Sales.MySalesPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    
    <div>
        
        <form runat="server">
            <section>
                <center>
                    <asp:GridView ID="GvListSales" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" EmptyDataText="<%$ Resources:, EmptyGridViewSales %>">
                        <Columns>
                            <asp:BoundField DataField="descName" HeaderText="<%$ Resources:, descName %>" />
                            <asp:BoundField DataField="date" HeaderText="<%$ Resources:, date %>" />
                            <asp:BoundField DataField="totalPrice" HeaderText="<%$ Resources:, totalPrice %>" />               
                        </Columns>  
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </center>
            </section>
        </form>
    </div>


</asp:Content>
