<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TotalesUF.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.ExpensasUF.TotalesUF" %>

<table>
    <tr>
        <td style="width: 201px; height: 28px;">Coeficiente    
        </td>
        <td align="right" style="width: 70px; height: 28px">
            <asp:label id="lblCoeficiente" runat="server" style="color: #003399" width="60px"></asp:label>
        </td>
    </tr>
    <tr>
        <td style="width: 201px">
            Subtotal Gastos Ordinario
        </td>
        <td align="right" style="width: 70px">
            <asp:label id="lblSubtotalGastoOrdinario" runat="server" style="color: #003399" width="60px"></asp:label>
        </td>
    </tr>
    <tr>
        <td style="width: 201px; height: 20px;">Subtotal Gastos Extraordinario
        </td>
        <td align="right" style="width: 70px; height: 20px">
            <asp:label id="lblSubtotalGastoExt" runat="server" style="color: #003399" width="60px"></asp:label>
        </td>
    </tr>
    <tr>
        <td style="width: 201px; height: 20px;">Subtotal Gastos Cochera Ord.</td>
        <td style="height: 20px; width: 70px;" align="right">
            <asp:label id="lblSubtotalGastoCocherarOrd" runat="server" style="color: #003399" width="60px"></asp:label>
        </td>
    </tr>                        
    <tr>
        <td style="width: 201px">Subtotal Gastos Cochera Ext.
        </td>
        <td align="right" style="width: 70px">
            <asp:label id="lblSubtotalGastoCocheraExt" runat="server" style="color: #003399" width="60px"></asp:label>
        </td>
    </tr>
    <tr>
        <td style="width: 201px">Subtotal Gasto Particular</td>
        <td align="right" style="width: 70px">
            <asp:label id="lblSubtotalGastoParicular" runat="server" style="color: #003399" width="60px"></asp:label>
        </td>
    </tr>
    <tr style="font-weight: bold">
        <td style="width: 201px; height: 30px;">Importe 1° Vencimiento
        </td>
        <td align="right" style="width: 70px; height: 30px">
            <asp:label id="lblVencimiento1" runat="server" style="color: #003399" width="60px"></asp:label>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 29px"></td>
    </tr>
</table>
