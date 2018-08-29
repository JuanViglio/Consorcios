<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dueños.aspx.cs" Inherits="WebSistemmas.Consorcios.Dueños" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register src="~/Consorcios/UserControls/Dueños/GridDueños.ascx" tagname="UserControlGridDueños" tagprefix="uc3" %>

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

    </form>
</asp:Content>