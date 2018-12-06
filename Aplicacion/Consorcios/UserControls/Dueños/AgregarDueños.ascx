<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgregarDueños.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Dueños.AgregarDueños" %>

<style type="text/css">
    .auto-style1 {
        width: 375px;
        margin-top: 0px;
    }
</style>

<div id="divDueñoDatos" style="display: none">
    <table class="auto-style1">
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label7" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td style="height: 23px" colspan="2">
                <asp:TextBox ID="txtNombreNuevo" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label8" runat="server" Text="Direccion"></asp:Label>
            </td>
            <td style="height: 23px" colspan="2">
                <asp:TextBox ID="txtDireccionNuevo" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label1" runat="server" Text="Mail"></asp:Label>
            </td>
            <td style="height: 23px" colspan="2">
                <asp:TextBox ID="txtMail" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style6">
                <asp:Label ID="Label4" runat="server" Text="Tipo"></asp:Label>
            </td>
            <td class="auto-style6">
                <asp:DropDownList ID="ddlTipoNuevo" runat="server" Height="18px" Width="235px"></asp:DropDownList>
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

