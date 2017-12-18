<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Detalles.aspx.cs" Inherits="WebSistemmas.Consorcios.Detalles" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <form id="form1" runat="server">
        <p>
        </p>
        <p style="font-size: large; color: #003399">
            &nbsp;</p>
        <p style="font-size: large; color: #003399">
            &nbsp;Detalle te Gastos del consorcio
            <asp:Label ID="lblConsorcio" runat="server"></asp:Label>
        </p>
        <p style="font-size: large; color: #003399">
            &nbsp;</p>
        <table>
            <tr>
                <td style="width: 77px; height: 39px;">Gasto&nbsp;&nbsp; </td>
                <td style="height: 39px">
                    <asp:DropDownList autopostback="true" ID="ddlGastos" runat="server" Height="26px" Width="360px" OnSelectedIndexChanged="ddlGastos_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>                               
            </tr>
            <tr><td style="height: 28px">Detalle</td><td style="height: 28px">
                <asp:TextBox ID="txtDetalle" runat="server" Width="151px" Height="22px"></asp:TextBox>
                </td></tr>
        </table>
        <table>
            <tr><td style="height: 104px; width: 120px;">
                <asp:Button ID="btnGuardar" runat="server" Height="35px" Text="Guardar" Width="99px" OnClick="btnGuardar_Click" />
&nbsp; </td><td style="height: 104px">
                    <asp:Button ID="btnVolver" runat="server" Height="35px" OnClick="btnVolver_Click" Text="Volver" Width="99px" />
                </td></tr>
        </table>
    </form>

</asp:Content>

