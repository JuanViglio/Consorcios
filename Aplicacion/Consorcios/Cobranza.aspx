<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cobranza.aspx.cs" Inherits="WebSistemmas.Consorcios.Cobranza"  MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register TagPrefix="uc2" TagName="acordeonBuscar" Src="~/Consorcios/UserControls/Cobranza/AcordeonBuscar.ascx"%>
<%@ Register TagPrefix="uc3" TagName="gridPagar" Src="~/Consorcios/UserControls/Cobranza/GridPagar.ascx"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server" style="height: 814px">
    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <link href="../css/Paginas.css" rel="Stylesheet" />

    <asp:scriptmanager id="Scriptmanager1" runat="server">
    </asp:scriptmanager>

    <uc1:tituloPagina ID="tituloPaginaID" runat="server" />

    <table>
        <tr>
            <td>
                <uc2:acordeonBuscar ID="acordeonBuscarID" runat="server" />
            </td>
            <td style="width: 443px; vertical-align: top;">
                <uc3:gridPagar ID="gridPagarID" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</asp:Content>