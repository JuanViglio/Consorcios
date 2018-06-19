<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CtaCteProveedor.aspx.cs" Inherits="WebSistemmas.Consorcios.CtaCteProveedor" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register TagPrefix="ucGridCtaCteProveedores" TagName="gridCtaCteProveedores" Src="~/Consorcios/UserControls/CtaCteProveedor/GridCtaCteProveedor.ascx" %>
<%@ Register TagPrefix="ucAgregarPagoProveedor" TagName="agregarPagoProveedor" Src="~/Consorcios/UserControls/CtaCteProveedor/AgregarPagoProveedor.ascx" %>
<%@ Register src="~/Consorcios/UserControls/Error.ascx" tagname="errorUC" tagprefix="uc2" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script src="../js/Proveedores.js"></script>

    <form id="form1" runat="server">
        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
        <uc2:errorUC ID="UserControl2ID" runat="server" />
        <ucGridCtaCteProveedores:gridCtaCteProveedores ID="gridCtaCteProveedoresID" runat="server" />

        <table style="height: 134px; margin-top: 14px">
            <tr>
                <td style="height: 135px; width: 151px" valign="top">
                    <div style="height: 61px; width: 151px;">
                        <asp:Button ID="btnVolver" runat="server" Height="39px" Text="Volver" Width="114px" UseSubmitBehavior="False" OnClick="btnVolver_Click"/>
                    </div>
                </td>
                <td style="height: 135px; width: 151px" valign="top">
                    <div id="divBotonPagoNuevo" style="height: 61px; width: 151px;">
                        <asp:Button ID="btnIngresarPago" runat="server" Height="39px" Text="Nuevo Pago" Width="114px" OnClientClick="SlideDivNuevoPago(); return false;" UseSubmitBehavior="False"/>
                    </div>
                </td>
                <td style="height: 135px; width: 128px">
                    <ucAgregarPagoProveedor:agregarPagoProveedor ID="agregarPagoProveedorID" runat="server" />
                </td>                
            </tr>
        </table>
    </form>
</asp:Content>