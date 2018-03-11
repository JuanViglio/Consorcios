<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgregarProveedores.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Proveedores.AgregarProveedores" %>

<style type="text/css">
    .auto-style1 {
        width: 375px;
        margin-top: 0px;
    }
</style>

<div id="divProveedorDatos" style="display:none">
    <table class="auto-style1">
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label7" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td style="height: 23px" colspan="2">
                <asp:TextBox ID="txtNombreNuevo" runat="server" Width="156px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label8" runat="server" Text="Direccion"></asp:Label>
            </td>
            <td style="height: 23px" colspan="2">
                <asp:TextBox ID="txtDireccionNuevo" runat="server" Width="156px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label1" runat="server" Text="Mail"></asp:Label>
            </td>
            <td style="height: 23px" colspan="2">
                <asp:TextBox ID="txtMail" runat="server" Width="156px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 130px; height: 53px">
                <asp:Button ID="btnAceptarNuevoProveedor" runat="server" Height="30px" OnClick="btnAceptarNuevoProveedor_Click" Text="Agregar" Width="90px" />
            </td>
            <td style="height: 53px" colspan="2">
                <asp:Button ID="btnCancelarDatos" runat="server" Height="30px" Text="Cancelar" Width="90px" OnClientClick="CerrarDivProveedorDatos(); return false;" />
            </td>
        </tr>
    </table>
</div>

