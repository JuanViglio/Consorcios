<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AgregarPagoProveedor.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.CtaCteProveedor.AgregarPagoProveedor" %>

<div id="divNuevoPago" style="height: 164px; width: 288px; display: none" >
    <table style="width: 251px">
        <tr>
            <td style="width: 103px">Fecha</td>
            <td>
                <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
            </td>
        </tr>                            
        <tr>
            <td style="width: 103px">Importe</td>
            <td>
                <asp:TextBox ID="txtImporte" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 103px">Detalle</td>
            <td>
                <asp:TextBox ID="txtDetalle" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 103px">Ord. Pago</td>
            <td>
                <asp:TextBox ID="txtOrdenDePago" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td style="width: 133px; height: 56px">
                <asp:Button ID="btnAgrgarPago" runat="server" Height="39px" Text="Agregar" Width="115px" OnClick="btnAgrgarPago_Click" UseSubmitBehavior="False"/>
            </td>
            <td style="height: 56px">
                <asp:Button ID="btnCancelarPago" runat="server" Height="39px" Text="Cancelar" Width="114px" OnClientClick="CerrarDivNuevoPago(); return false;" UseSubmitBehavior="False"/>
            </td>
        </tr>
    </table>
</div>
