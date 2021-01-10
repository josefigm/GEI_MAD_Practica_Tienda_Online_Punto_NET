<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="ManageCardsPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.User.ManageCardsPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <div>
        <asp:HyperLink ID="hlCreateCard" runat="server" meta:resourcekey="hlCreateCard" NavigateUrl="~/Pages/User/CreateNewCardPage.aspx">HyperLink</asp:HyperLink>
    </div>
    <br />
    <div>
        <form runat="server">
            <section>
                <center>
                    <asp:GridView ID="GvListCards" runat="server" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AutoGenerateColumns="False" OnRowCommand="GvListCards_OnRowCommand" EmptyDataText="<%$ Resources:, EmptyGridView %>">
                        <Columns>
                            <asp:BoundField DataField="CardId" Visible ="false" />
                            <asp:BoundField DataField="Number" HeaderText="<%$ Resources:, Number %>" />
                            <asp:BoundField DataField="CVV" HeaderText="<%$ Resources:, CVV %>" />
                            <asp:BoundField DataField="ExpireDate" DataFormatString = "{0:MM/yyyy}" ItemStyle-HorizontalAlign="Center" HeaderText="<%$ Resources:, ExpireDate %>" />
                            <asp:BoundField DataField="Type" HeaderText="<%$ Resources:, Type %>" />
                            <asp:checkboxfield Datafield="DefaultCard" headertext="<%$ Resources:, DefaultCard %>"/>     
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="Update Card Details" Text="<%$ Resources:, UpdateCardDetails %>" CommandArgument='<%# Eval("CardId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="Set Default Card" Text="<%$ Resources:, SetDefaultCard %>" CommandArgument='<%# Eval("Number") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>             
                        </Columns>  
                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                    </asp:GridView>
                </center>
            </section>
        </form>
    </div>
    <br />
</asp:Content>
