<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GastosParticularesUF.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.ExpensasUF.GastosParticulares" %>

<table>
    <tr>
        <td colspan="3" style="height: 43px">Gastos Particulares
        </td>
    </tr>
    <tr>
        <td style="height: 18px; width: 98px">Descripcion
        </td>
        <td style="height: 18px" colspan="2">

            <asp:textbox id="txtDetalleGastoParticular" runat="server" width="385px"></asp:textbox>

        </td>
    </tr>
    <tr>
        <td style="width: 98px; height: 38px;">Importe
        </td>
        <td style="width: 207px; height: 38px">
            <asp:textbox id="txtImporteGastoParticular" runat="server" width="108px"></asp:textbox>
        </td>
        <td style="width: 233px; height: 38px;">
            <asp:button id="btnActualizar" runat="server" text="Actualizar" height="29px" width="99px" onclick="btnActualizar_Click" UseSubmitBehavior="False" />
        </td>
    </tr>
    <tr>
        <td style="height: 51px">

            <asp:Button ID="btnAnterior" runat="server" Height="28px" OnClick="btnAnterior_Click" Text="Anterior" UseSubmitBehavior="False" Width="95px" style="margin-right: 15px" />

        </td>
        <td style="height: 51px">
            <asp:Button ID="btnProximo" runat="server" Height="28px" OnClick="btnProximo_Click" Text="Proximo" UseSubmitBehavior="False" Width="95px" />
        </td>
    </tr>
</table>

