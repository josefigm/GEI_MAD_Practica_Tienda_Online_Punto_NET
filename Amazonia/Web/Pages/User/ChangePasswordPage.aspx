<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="ChangePasswordPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.User.ChangePasswordPage" %>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <div id="form">
        <form id="ChangePasswordForm" method="post" runat="server">
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclOldPassword" runat="server" meta:resourcekey="lclOldPassword" />
                </span>
                <span class="entry">
                        <asp:TextBox ID="tbOldPassword" TextMode="Password" runat="server" Width="100" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="tbOldPassword" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                        <asp:Label ID="lblOldPasswordError" runat="server" ForeColor="Red" Visible="False" meta:resourcekey="lblOldPasswordError"></asp:Label>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclNewPassword" runat="server" meta:resourcekey="lclNewPassword" />
                </span>
                <span class="entry">
                        <asp:TextBox TextMode="Password" ID="tbNewPassword" runat="server" Width="100" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="tbNewPassword" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                        <asp:CompareValidator ID="cvCreateNewPassword" runat="server" ControlToCompare="tbOldPassword" ControlToValidate="tbNewPassword" Operator="NotEqual" meta:resourcekey="cvCreateNewPassword"></asp:CompareValidator>
               </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRetypePassword" />
                </span>
                <span class="entry">
                        <asp:TextBox TextMode="Password" ID="tbRetypePassword" runat="server" Width="100" Columns="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="tbRetypePassword" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                        <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="tbNewPassword" ControlToValidate="tbRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator>
               </span>
            </div>
            <div class="button">
                <asp:Button ID="btnChangePassword" runat="server" OnClick="BtnChangePasswordClick" meta:resourcekey="btnChangePassword" />
            </div>
        </form>
    </div>
</asp:Content>
