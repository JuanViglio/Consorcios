<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Seguros.aspx.cs" Inherits="WebSistemmas.Consorcios.Seguros" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register src="~/Consorcios/UserControls/Error.ascx" tagname="errorUC" tagprefix="uc2" %>
<%@ Register src="~/Consorcios/UserControls/Seguros/GridSeguros.ascx" tagname="UserControlGridSeguros" tagprefix="uc3" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">

        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />

        <table>
            <tr>
                <td style="width: 675px">
                     <uc3:UserControlGridSeguros ID="UserControlGridSegurosID" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:button runat="server" text="Nuevo" ID="btnNuevoSeguro" Height="34px" OnClick="NuevoSeguro_Click" Width="109px" />
                </td>
            </tr>
        </table>

        <uc2:erroruc ID="UserControl2ID" runat="server" />

    </form>
</asp:Content>
