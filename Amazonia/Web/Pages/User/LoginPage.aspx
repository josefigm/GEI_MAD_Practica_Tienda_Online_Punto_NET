<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.User.WebForm1" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:HyperLink ID="hl1" runat="server" meta:resourcekey="hl1" NavigateUrl="~/Pages/User/RegisterPage.aspx">HyperLink</asp:HyperLink>    
    <div>
        <form id="form1" runat="server">
            <div class ="field">
                <span class="label">
                    <asp:Localize ID="lclLogin" runat="server" meta:resourcekey="lclLogin"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbLogin" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLogin" runat="server" ControlToValidate="tbLogin" Text="<%$ Resources:Common, requiredField %>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative" Visible="False" meta:resourcekey="lblLoginError"></asp:Label>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPassword" runat="server" meta:resourcekey="lclPassword"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbPassword" Text="<%$ Resources:Common, requiredField %>" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:Label ID="lblPasswordError" runat="server" ForeColor="Red" Style="position: relative" Visible="False" meta:resourcekey="lblPasswordError"></asp:Label>

                </span>
            </div>
            <div class="checkbox">
                <asp:CheckBox ID="checkPassword" runat="server" meta:resourcekey="checkPassword"/>
            </div>
            <div class ="button">
                <asp:Button ID="btnLogin" runat="server" Text="Button" meta:resourcekey="btnLogin" OnClick="btnLogin_Click"/>
            </div>
        </form>
    </div>
</asp:Content>
