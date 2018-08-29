<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnidadesFuncionalesCtaCte.aspx.cs" Inherits="WebSistemmas.Consorcios.UnidadesFuncionalesCtaCte" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register TagPrefix="ucGridCtaCteProveedores" TagName="gridUnidadesFuncionalesCtaCte" Src="~/Consorcios/UserControls/UnidadesFuncionalesCtaCte/GridUnidadesFuncionalesCtaCte.ascx" %>
<%@ Register TagPrefix="ucAgregarPagoUF" TagName="agregarPagoUF" Src="~/Consorcios/UserControls/UnidadesFuncionalesCtaCte/AgregarPagoUF.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script src="../js/Proveedores.js"></script>


    <form id="form1" runat="server">
        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
        <ucGridCtaCteProveedores:gridUnidadesFuncionalesCtaCte ID="gridUnidadesFuncionalesCtaCteID" runat="server" />
        <br />
        <table>
            <tr>
                <td style="width: 130px; height: 68px;">        
                    <asp:Button ID="btnVerDeuda" runat="server" Height="36px" OnClick="btnVerDeuda_Click" Text="Ver Deuda" Width="100px" />
                </td>
                <td style="width: 130px; height: 68px;">
                    <asp:Button ID="btnVolver" runat="server" Height="36px" OnClick="btnVolver_Click" Text="Volver" Width="100px" />
                </td>
                <td style="height: 68px">
                    <asp:Button ID="btnPagar" runat="server" Height="36px" Text="Pagar" Width="100px" OnClientClick="SlideDivNuevoPago(); return false;" UseSubmitBehavior="False" />
                </td>
            </tr>
        </table>
        <ucAgregarPagoUF:agregarPagoUF ID="agregarPagoUFSID" runat="server" />
    </form>
</asp:Content>