<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubtotalesUF.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.ExpensasUF.SubtotalesUF" %>

<style type="text/css">
    .auto-style1 {
        height: 45px;
        width: 149px;
    }
    .auto-style2 {
        height: 40px;
        width: 149px;
    }
    .auto-style3 {
        height: 38px;
        width: 149px;
    }
    .auto-style4 {
        width: 564px;
    }
    .auto-style5 {
        height: 45px;
        width: 25px;
    }
    .auto-style6 {
        height: 40px;
        width: 25px;
    }
    .auto-style7 {
        height: 38px;
        width: 25px;
    }
</style>

<table class="auto-style4">
    <tr>
        <td style="text-align: left; width: 80px">
            <asp:Label ID="Label5" runat="server" Text="Subtotal Ord.:" Font-Size="Large" style="color: #003399"></asp:Label>
        </td>
        <td align="right" class="auto-style5">
            <asp:Label ID="lblTotalGastosOrdinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
        </td>
        <td style="width: 157px; height: 45px;">
            <asp:button runat="server" text="Volver" id="btnVolver" height="35px" width="137px" onclick="btnVolver_Click" style="margin-top: 8px; margin-left: 55px;"></asp:button>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Text="Subtotal Extraord." Font-Size="Large" style="color: #003399"></asp:Label>
        </td>
        <td align="right" class="auto-style6">
            <asp:Label ID="lblTotalGastosExtraordinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
        </td>
        <td style="height: 40px"></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label10" runat="server" Text="Total Gastos:" Font-Size="Large" style="color: #003399"></asp:Label>
        </td>
        <td align="right" class="auto-style7">
            <asp:Label ID="lblTotalGastos" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
        </td>
        <td></td>
    </tr>
</table>

