<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModificarProveedores.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Proveedores.ModificarProveedores" %>

<style type="text/css">
    .auto-style1 {
        width: 375px;
        margin-top: 0px;
        height: 218px;
    }
    .auto-style2 {
        height: 23px;
        width: 117px;
    }
    .auto-style3 {
        height: 53px;
        width: 117px;
    }
    .auto-style4 {
        height: 23px;
        width: 248px;
    }
    .auto-style5 {
        height: 53px;
        width: 248px;
    }
    .auto-style6 {
        height: 22px;
    }
</style>

<%-- --%>
<div id="divProveedorModificar" style="display: none">
    <table class="auto-style1">
        <tr style="visibility:hidden"> 
            <td class="auto-style2">
                <asp:Label ID="Label2" runat="server" Text="Codigo"></asp:Label>
            </td>
            <td class="auto-style4">
                <asp:TextBox ID="txtCodigoModificar" runat="server" Width="156px"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label7" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td class="auto-style4">
                <asp:TextBox ID="txtNombreModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label8" runat="server" Text="Direccion"></asp:Label>
            </td>
            <td class="auto-style4">
                <asp:TextBox ID="txtDireccionModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Mail"></asp:Label>
            </td>
            <td class="auto-style4">
                <asp:TextBox ID="txtMailModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label3" runat="server" Text="Telefono"></asp:Label>
            </td>
            <td class="auto-style4">
                <asp:TextBox ID="txtTelefonoModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label4" runat="server" Text="Tipo"></asp:Label>
            </td>
            <td class="auto-style6">
                <asp:DropDownList ID="ddlTipoModificar" runat="server" Height="18px" Width="235px"></asp:DropDownList>
            </td>
        </tr>        
        <tr>
            <td class="auto-style3">
                <asp:Button ID="btnAceptarModificar" runat="server" Height="30px" OnClick="btnAceptarModificar_Click" Text="Agregar" Width="90px" />
            </td>
            <td class="auto-style5">
                <asp:Button ID="btnCancelarModificar" runat="server" Height="30px" Text="Cancelar" Width="90px" OnClientClick="CerrarDivProveedorModificar(); return false;" />
            </td>
        </tr>

    </table>
</div>

