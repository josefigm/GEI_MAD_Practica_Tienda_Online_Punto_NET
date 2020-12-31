<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="ManageCardsPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.User.ManageCardsPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <asp:HyperLink ID="hlCreateCard" runat="server" meta:resourcekey="hlCreateCard" NavigateUrl="~/Pages/User/CreateNewCardPage.aspx">HyperLink</asp:HyperLink>
    <br />
    <form runat="server">
        <asp:GridView ID="GvListCards" runat="server" CssClass="listCards" AutoGenerateColumns="False" OnRowCommand="GvListCards_OnRowCommand">
            <Columns>
                <asp:BoundField DataField="Number" HeaderText="<%$ Resources:, Number %>" />
                <asp:BoundField DataField="CVV" HeaderText="<%$ Resources:, CVV %>" />
                <asp:BoundField DataField="ExpireDate" HeaderText="<%$ Resources:, ExpireDate %>" />
                <asp:BoundField DataField="Type" HeaderText="<%$ Resources:, Type %>" />
                <asp:checkboxfield Datafield="DefaultCard" headertext="<%$ Resources:, DefaultCard %>"/>     

                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:Button ID="Button1" runat="server" CausesValidation="false" CommandName="Update Card Details" Text="Update Card Details" CommandArgument='<%# Eval("Number") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                
            </Columns>  
        </asp:GridView>
        <br />
    </form>
    <br />
    <br />
</asp:Content>
