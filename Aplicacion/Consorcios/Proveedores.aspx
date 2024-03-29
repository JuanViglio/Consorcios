﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="WebSistemmas.Consorcios.Proveedores" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register src="~/Consorcios/UserControls/Error.ascx" tagname="errorUC" tagprefix="uc2" %>
<%@ Register src="~/Consorcios/UserControls/Proveedores/GridProveedores.ascx" tagname="UserControlGridProveedores" tagprefix="uc3" %>
<%@ Register src="~/Consorcios/UserControls/Proveedores/AgregarProveedores.ascx" tagname="UserControlAgregarProveedores" tagprefix="uc4" %>
<%@ Register src="~/Consorcios/UserControls/Proveedores/ModificarProveedores.ascx" tagname="UserControlModificarProveedores" tagprefix="uc5" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">

        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />

        <table>
            <tr>
                <td style="width: 675px">
                     <uc3:UserControlGridProveedores ID="UserControlGridProveedoresID" runat="server" />
                </td>
            </tr>
        </table>
        <table style="width: 1317px">
            <tr>
                <td style="text-align: left; vertical-align:top; width: 144px;">
                    <div id="divProveedorNuevo" style="height: 61px; width: 151px;">
                        <asp:Button ID="btnNuevoProveedor" runat="server" Height="36px" OnClientClick="SlideDivProveedorDatos(); return false;" Text="Nuevo" Width="115px" style="margin-top: 0px" />
                    </div>             
                </td>
                <td style="width: 986px">
                    <uc4:UserControlAgregarProveedores ID="UserControlAgregarProveedoresID" runat="server" />
                    <uc5:UserControlModificarProveedores ID="UserControlModificarProveedoresID" runat="server" />
                </td>
                <td style="width: 193px">
                </td>
            </tr>
        </table>

        <uc2:errorUC ID="UserControl2ID" runat="server" />

    </form>
</asp:Content>

