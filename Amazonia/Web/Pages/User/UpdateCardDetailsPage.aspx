<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="UpdateCardDetailsPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.User.UpdateCardDetailsPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <form method="post" runat="server">

        <asp:Label ID="Label1" runat="server" Visible ="false"></asp:Label>


        <div class="field">
            <span class="label">
                <asp:Localize ID="lclCVV" runat="server" meta:resourcekey="lclCVV" />
            </span>
            <span class="entry">
                <asp:TextBox ID="tbCVV" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCVV" runat="server" ControlToValidate="tbCVV" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvCVVResource1"> </asp:RequiredFieldValidator>
            </span>
        </div>

        <div class="field">
            <span class="label">
                <asp:Localize ID="lclExpireDate" runat="server" meta:resourcekey="lclExpireDate" />
            </span>
            <span class="entry">
                <asp:TextBox ID="tbExpireDateMonth" runat="server" Width="50px" Columns="16"></asp:TextBox>
                <asp:TextBox ID="tbExpireDateYear" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvExpireDateMonth" runat="server" ControlToValidate="tbExpireDateMonth" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvExpireDateResourceMonth"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="rfvExpireDateYear" runat="server" ControlToValidate="tbExpireDateYear" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvExpireDateResourceYear"></asp:RequiredFieldValidator>
            </span>
        </div>

       <div class="field">
            <span class="label">
                <asp:Localize ID="lclType" runat="server" meta:resourcekey="lclType" />
            </span>
            <span class="entry">
                <asp:DropDownList ID="ddlType" runat="server" Width="130px">
                        <asp:ListItem Value="creditCard" Text="<%$ Resources:Common, creditCard %>" />
                        <asp:ListItem Value="debitCard" Text="<%$ Resources:Common, debitCard %>" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvTypeResource1"></asp:RequiredFieldValidator>
            </span>
        </div> 

        <div class="button">
            <asp:Button ID="BtnUpdateCard" runat="server" meta:resourcekey="BtnUpdateCard" OnClick="BtnUpdateCard_Click" />
        </div>
    </form>


</asp:Content>
