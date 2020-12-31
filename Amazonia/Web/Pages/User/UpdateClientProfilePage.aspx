<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="UpdateClientProfilePage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.User.UpdateClientProfilePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder_MenuWelcome" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br />
    <div>
        <asp:HyperLink ID="lnkChangePassword" runat="server" NavigateUrl="~/Pages/User/ChangePasswordPage.aspx" meta:resourcekey="lnkChangePassword"/>
    </div>
    <br />
    <div>
        <asp:HyperLink ID="lnkManageCardsPage" runat="server" NavigateUrl="~/Pages/User/ManageCardsPage.aspx" meta:resourcekey="lnkManageCardsPage"/>
    </div>

    <div id="form">
        <form id="UpdateClientProfileForm" method="POST" runat="server">
            
            
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbFirstName" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                        ControlToValidate="tbFirstName" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclSurname" runat="server" meta:resourcekey="lclSurname" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbSurname" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSurname" runat="server"
                        ControlToValidate="tbSurname" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclAddress" runat="server" meta:resourcekey="lclAddress" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbAddress" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                        ControlToValidate="tbAddress" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbEmail" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ControlToValidate="tbEmail" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server"
                        ControlToValidate="tbEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="revEmail"></asp:RegularExpressionValidator>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" />
                </span>
                <span class="entry">
                    <asp:DropDownList ID="comboLanguage" runat="server" AutoPostBack="True" Width="100px" onselectedindexchanged="ComboLanguageSelectedIndexChanged"></asp:DropDownList>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclCountry" runat="server" meta:resourcekey="lclCountry" />
                </span>
                <span class="entry">
                    <asp:DropDownList ID="comboCountry" runat="server" Width="100px">
                    </asp:DropDownList>
                </span>
            </div>
            <div class="button">
                <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdateClick" meta:resourcekey="btnUpdate"/>
            </div>
        </form>
    </div>
</asp:Content>
