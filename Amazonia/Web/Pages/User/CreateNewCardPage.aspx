<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="CreateNewCardPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.User.CreateNewCardPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

   <form method="post" runat="server">

        <div class="field">
            <span class="label">
                <asp:Localize ID="lclNumber" runat="server" meta:resourcekey="lclNumber" />
            </span>
            <span class="entry">
                <asp:TextBox ID="tbNumber" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumber" runat="server" ControlToValidate="tbNumber" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvNumberResource1"></asp:RequiredFieldValidator>
                <asp:Label ID="lblNumberError" runat="server" ForeColor="Red" Style="position: relative" Visible="False" meta:resourcekey="lblNumberError"></asp:Label>
            </span>
        </div>

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
            <asp:Button ID="btnCreateCard" runat="server" Text="Button" meta:resourcekey="btnCreateCard" OnClick="btnCreateCard_Click" />
        </div>
    </form>

</asp:Content>
