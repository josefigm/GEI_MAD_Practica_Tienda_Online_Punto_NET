<%@ Page Title="" Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="FinishSalePage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Sale.FinishSalePage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <center>
        <h1>
            <asp:localize ID="lclFinishSale" runat="server" meta:resourcekey="lclFinishSale"></asp:localize>
        </h1>
        <form id="form" runat="server">
            <div>
                <span class="label">
                    <asp:Localize ID="lclDescName" runat="server" meta:resourcekey="lclDescName"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbDescName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbDescName" Text="<%$ Resources:Common, requiredField %>" Display="Dynamic"></asp:RequiredFieldValidator>
                </span>
            </div>
            <div>
                <span class="attributeName">
                    <asp:Localize ID="lclAdress" runat="server" meta:resourcekey="lclAdress"></asp:Localize>
                </span>
                <asp:Localize ID="defaultAddress" runat="server"></asp:Localize>
            </div>
            <br />
            <div>
                <span class="label">
                    <asp:Localize ID="lclChangeAddress" runat="server" meta:resourcekey="lclChangeAddress"></asp:Localize>
                </span>
                <span class="entry">
                    <asp:TextBox ID="tbNewAddress" runat="server"></asp:TextBox>
                </span>
            </div>
            <br />
            <div>
                <asp:Localize ID="lclPayWithDefault" runat="server" meta:resourcekey="lclPayWithDefault"></asp:Localize>
                <asp:Localize ID="lclDefaultCartNumber" runat="server"></asp:Localize>
            </div>
            <br />
            <div>
                <span class="label">
                    <asp:Localize ID="lclChooseCard" runat="server" meta:resourcekey="lclChooseCard" />
                </span>
                <span class="entry">
                    <asp:DropDownList ID="comboCards" runat="server" Width="100px" meta:resourcekey="comboCards"></asp:DropDownList>
                </span>
            </div>
            <div class="button">
                <asp:Button ID="btnFinishSale" runat="server" Text="Button" meta:resourcekey="btnFinishSale" OnClick="btnFinishSale_Click" />
            </div>
        </form>
    </center>
</asp:Content>
