<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="AddCommentPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Product.AddCommentPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <br>
    <asp:Label 
        ID="lblAlreadyCommentedError" 
        runat="server" 
        ForeColor="Red" 
        Style="position: relative"             
        Visible="False" 
        meta:resourcekey="lblAlreadyCommentedError">                        
    </asp:Label>
    <form runat="server">
        <asp:Label ID="lblProductId" runat="server" visible="false" ></asp:Label>
        <div class="field">
            <span class="label">
                <asp:Localize 
                    ID="lclTitle" 
                    runat="server" 
                    meta:resourcekey="lclTitle"
                />
            </span>
            <span class="entry">
                <asp:TextBox 
                    ID="tbTitle" 
                    runat="server" 
                    Width="200" 
                    Columns="16">
                </asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvTitle" 
                    runat="server"
                    ControlToValidate="tbTitle" 
                    Display="Dynamic" 
                    Text="<%$ Resources:Common, requiredField %>"
                />
                <asp:RegularExpressionValidator 
                    ID="RegularExpressionValidator1" 
                    runat="server" 
                    Display = "Dynamic"
                    ControlToValidate = "tbTitle"
                    ValidationExpression = "^[\s\S]{0,60}$"
                    ErrorMessage="<%$ Resources:, maxLenghtTitle %>">
                </asp:RegularExpressionValidator>
            </span>
        </div>
        <div class="field">
            <span class="label">
                <asp:Localize 
                    ID="lclValue" 
                    runat="server" 
                    meta:resourcekey="lclValue" />
            </span>
            <span class="entry">
                <asp:TextBox 
                    ID="tbValue" 
                    runat="server" 
                    TextMode="MultiLine" 
                    Rows="10" 
                    Height="41px" 
                    Width="312px">
                </asp:TextBox>
                <asp:RequiredFieldValidator 
                    ID="rfvValue" 
                    runat="server"
                    ControlToValidate="tbValue" 
                    Display="Dynamic" 
                    Text="<%$ Resources:Common, requiredField %>"/>
                <asp:RegularExpressionValidator 
                    ID="rfvLenght" 
                    runat="server" 
                    Display = "Dynamic"
                    ControlToValidate = "tbValue"
                    ValidationExpression = "^[\s\S]{0,1000}$"
                    ErrorMessage="<%$ Resources:, maxLenghtValue %>">
                </asp:RegularExpressionValidator>
            </span>
        </div>
        <div class="button">
            <span>
                <asp:Button 
                    ID="btnAddComment" 
                    runat="server" 
                    visible="true" 
                    meta:resourcekey="btnAddComment" OnClick="btnAddComment_Click"/>
            </span>
        </div>
    </form>
</asp:Content>
