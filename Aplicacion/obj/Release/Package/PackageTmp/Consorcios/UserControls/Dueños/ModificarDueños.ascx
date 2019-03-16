<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModificarDueños.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Dueños.ModificarDueños" %>

<%-- --%>
<div id="divDueñoModificar" class="auto-style9" style="display: none">
    <table class="auto-style1">             
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label3" runat="server" Text="Nombre"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtNombreModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label1" runat="server" Text="Apellido"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtApellidoModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label4" runat="server" Text="Telefono"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTelefonoModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Direccion"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDireccionModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Mail"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMailModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnAceptarModificar" runat="server" Height="30px" OnClick="btnAceptarModificar_Click" Text="Modificar" Width="90px" />
            </td>
            <td style="height: 53px" colspan="2">
                <asp:Button ID="btnCancelarModificar" runat="server" Height="30px" Text="Cancelar" Width="90px" OnClientClick="CerrarDivDueñoModificar(); return false;" />
            </td>
        </tr>
        <tr style="visibility:hidden"> 
            <td style="width: 130px; height: 23px;">
                <asp:Label ID="Label2" runat="server" Text="Codigo"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCodigoModificar" runat="server" Width="230px"></asp:TextBox>
            </td>
        </tr>  
    </table>
</div>

