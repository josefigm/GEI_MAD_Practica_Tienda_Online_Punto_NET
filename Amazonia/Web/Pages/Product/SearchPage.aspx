<%@ Page Title=""Language="C#" MasterPageFile="~/Amazonia.Master" AutoEventWireup="true" CodeBehind="SearchPage.aspx.cs" Inherits="Es.Udc.DotNet.Amazonia.Web.Pages.Product.SearchPage" %>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="form1" runat="server">
    <div>
        <section>
            <!-- Lista de categorias. Por defecto, desmarcada-->
            <asp:Label ID="lblCategories" runat="server" meta:resourcekey="lblCategories"></asp:Label>
            <asp:DropDownList ID="comboCategory" runat="server"></asp:DropDownList>
            <!-- Recuadro para introducir la palabra clave de búsqueda-->
            <asp:TextBox ID="tbSearchProduct" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSearchProduct" runat="server" ControlToValidate="tbSearchProduct" Display="Dynamic" Text="<%$ Resources:Common, requiredField %>"></asp:RequiredFieldValidator>
            <!-- Botón para buscar productos-->
            <asp:Button ID="btnSearchProduct" runat="server" Text="Button" OnClick="SearchButton_Click" meta:resourcekey="btnSearchProduct" />
        </section>
    </div>
    <br>

    </form>
</asp:Content>
