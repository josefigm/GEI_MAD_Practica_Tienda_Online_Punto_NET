<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="EditProductPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Product.EditProductPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <asp:Label ID="lblProductId" runat="server" visible="false" Text="Label"></asp:Label>
    <div id="form">
        <form id="UpdateClientProfileForm" method="POST" runat="server">
            <asp:HyperLink ID="lnkChangePassword" runat="server" NavigateUrl="~/Pages/User/ChangePassword.aspx" meta:resourcekey="lnkChangePassword"/>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclProductName" runat="server" meta:resourcekey="lclProductName" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbProductName" runat="server" Width="200" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvProductName" runat="server"
                        ControlToValidate="tbProductName" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclPrice" runat="server" meta:resourcekey="lclPrice" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbPrice" runat="server" Width="100" Columns="16"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPrice" runat="server"
                        ControlToValidate="tbPrice" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclDescription" runat="server" meta:resourcekey="lclDescription" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbDescription" runat="server" TextMode="MultiLine" Rows="10" Height="41px" Width="312px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescription" runat="server"
                        ControlToValidate="tbDescription" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                    <asp:RegularExpressionValidator ID="rfvLenght" runat="server" 
                        Display = "Dynamic"
                        ControlToValidate = "tbDescription"
                        ValidationExpression = "^[\s\S]{0,1000}$"
                        ErrorMessage="<%$ Resources:, maxLenght %>">
                    </asp:RegularExpressionValidator>
                </span>
            </div>
            <div class="field">
                <span class="label">
                    <asp:Localize ID="lclStock" runat="server" meta:resourcekey="lclStock" />
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbStock" runat="server"  TextMode="Number" Rows="10" Height="16px" Width="100px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStock" runat="server"
                        ControlToValidate="tbStock" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"/>
                    <asp:RegularExpressionValidator ID="revStock" runat="server" 
                        Display = "Dynamic"
                        ControlToValidate = "tbDescription"
                        ValidationExpression = "^[\s\S]{0,1000}$"
                        ErrorMessage="<%$ Resources:, maxStock %>">
                    </asp:RegularExpressionValidator>
                </span>
            </div>
            <div class="button">
                <asp:Button ID="btnUpdate" runat="server" meta:resourcekey="btnUpdate" OnClick="btnUpdate_Click"/>
            </div>
        </form>
    </div>
</asp:Content>
