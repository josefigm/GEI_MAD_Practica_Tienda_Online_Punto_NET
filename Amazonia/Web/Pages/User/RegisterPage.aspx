<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.User.Register" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">

    <form method="post" runat="server">
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclUserName" runat="server" meta:resourcekey="lclLogin" />
            </span>
            <span class="entry">
                <asp:TextBox ID="tbLogin" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="tbLogin" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvUserNameResource1"></asp:RequiredFieldValidator>
                <asp:Label ID="lblLoginError" runat="server" ForeColor="Red" Style="position: relative" Visible="False" meta:resourcekey="lblLoginError"></asp:Label>
            </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclPassword" runat="server" meta:resourcekey="lclPassword" />
            </span>
            <span class="entry">
                <asp:TextBox TextMode="Password" ID="tbPassword" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tbPassword" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvPasswordResource1"> </asp:RequiredFieldValidator>
            </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclRetypePassword" runat="server" meta:resourcekey="lclRepeatPassword" /></span><span class="entry">
                <asp:TextBox TextMode="Password" ID="tbRetypePassword" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="tbRetypePassword" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvRetypePasswordResource1"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvPasswordCheck" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbRetypePassword" meta:resourcekey="cvPasswordCheck"></asp:CompareValidator></span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclFirstName" runat="server" meta:resourcekey="lclFirstName" /></span><span class="entry">
                <asp:TextBox ID="tbFirstName" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="tbFirstName" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvFirstNameResource1"></asp:RequiredFieldValidator></span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclSurname" runat="server" meta:resourcekey="lclSurname" /></span><span class="entry">
                <asp:TextBox ID="tbSurname" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSurname" runat="server" ControlToValidate="tbSurname" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvSurnameResource1"></asp:RequiredFieldValidator></span>
        </div>  
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclAddress" runat="server" meta:resourcekey="lclAddress" /></span><span class="entry">
                <asp:TextBox ID="tbAddress" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbAddress" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"></asp:RequiredFieldValidator></span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize ID="lclEmail" runat="server" meta:resourcekey="lclEmail" /></span><span class="entry">
                <asp:TextBox ID="tbEmail" runat="server" Width="100px" Columns="16"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>" meta:resourcekey="rfvEmailResource1"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="revEmail"></asp:RegularExpressionValidator></span>
        </div>

        <div class="field">
                <span class="label">
                    <asp:Localize ID="lclLanguage" runat="server" meta:resourcekey="lclLanguage" />
                </span>
                <span class="entry">
                    <asp:DropDownList ID="comboLanguage" runat="server" Width="100px" meta:resourcekey="comboLanguage"></asp:DropDownList>
                </span>
        </div>

        <div class="button">
            <asp:Button ID="btnRegister" runat="server" Text="Button" meta:resourcekey="btnRegister" OnClick="btnRegister_Click" />
        </div>
    </form>
</asp:Content>
