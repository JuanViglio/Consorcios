<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dueños.aspx.cs" Inherits="WebSistemmas.Consorcios.Dueños" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register src="~/Consorcios/UserControls/Dueños/GridDueños.ascx" tagname="UserControlGridDueños" tagprefix="uc3" %>
<%@ Register src="~/Consorcios/UserControls/Error.ascx" tagname="errorUC" tagprefix="uc2" %>
<%@ Register src="~/Consorcios/UserControls/Dueños/AgregarDueños.ascx" tagname="UserControlAgregarDueños" tagprefix="uc4" %>
<%--<%@ Register src="~/Consorcios/UserControls/Proveedores/ModificarDueños.ascx" tagname="UserControlModificarDueños" tagprefix="uc5" %>--%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">

        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />

        <table>
            <tr>
                <td style="width: 675px">
                     <uc3:UserControlGridDueños ID="UserControlGridDueñosID" runat="server" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="text-align: left; vertical-align:top; width: 4px;">
                    <div id="divDueñoNuevo" style="height: 61px; width: 151px;">
                        <asp:Button ID="btnNuevoDueño" runat="server" Height="36px" OnClientClick="SlideDivDueñoDatos(); return false;" Text="Nuevo" Width="115px" style="margin-top: 0px" />
                    </div>             
                </td>
                <td style="width: 453px">
                    <uc4:UserControlAgregarDueños ID="UserControlAgregarDueñosID" runat="server" />
                </td>
                <td style="width: 437px">
                    <%--<uc5:UserControlModificarProveedores ID="UserControlModificarProveedoresID" runat="server" />--%>
                </td>
            </tr>
        </table>

        <uc2:errorUC ID="UserControl2ID" runat="server" />
    </form>
</asp:Content>