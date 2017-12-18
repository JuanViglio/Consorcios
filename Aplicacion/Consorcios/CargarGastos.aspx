<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CargarGastos.aspx.cs" Inherits="WebSistemmas.Consorcios.CargarGastos" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

<head>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <span style="font-size: large; color: #003399">Cargar Gastos<br /></span>
        <br />
        <table>
            <tr>
                <td style="width: 109px; height: 39px;">
                    Consorcio</td>
                <td style="height: 39px">
                    <asp:dropdownlist id="ddlConsorcios" runat="server" height="19px" width="253px" autopostback="True" style="margin-right: 24px; margin-left: 0px;" OnSelectedIndexChanged="ddlConsorcios_SelectedIndexChanged">
                    </asp:dropdownlist>
                </td>
            </tr>
            <tr>
                <td>Periodo</td>
                <td style="height: 39px">
                    <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 39px; width: 109px;">Gasto</td>
                <td style="height: 39px">
                    <asp:Label ID="lblGasto" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="height: 39px; width: 109px;">Detalle</td>
                <td style="height: 39px">
                    <asp:TextBox ID="txtDetalle" runat="server" Height="20px" Width="352px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 39px; width: 109px;">Importe</td>
                <td style="height: 39px">
                    <asp:TextBox ID="txtImporte" runat="server" Height="20px" Width="171px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 109px; height: 80px">
                    <asp:Button ID="btnGuardar" runat="server" Height="32px" Text="Guardar" Width="90px" OnClick="btnGuardar_Click" />
                </td>
                <td style="height: 80px">
                    <asp:Button ID="btnVolver" runat="server" Height="32px" Text="Volver" Width="90px" OnClick="btnVolver_Click" />
                </td>
            </tr>
        </table>
    
        <br />
            <asp:label id="lblError" runat="server" forecolor="Red" style="text-align: left; font-size: large;"></asp:label>
    
    </div>
    </form>
</body>

</asp:Content>