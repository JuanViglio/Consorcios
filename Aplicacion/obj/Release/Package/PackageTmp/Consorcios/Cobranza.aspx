<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cobranza.aspx.cs" Inherits="WebSistemmas.Consorcios.Cobranza"  MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">
    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <link href="../css/Paginas.css" rel="Stylesheet" />

    <uc1:tituloPagina ID="tituloPaginaID" runat="server" />

    <div id="accordion" style="width: 631px">

    <h3>Buscar por UF</h3>
    <div>
       <table>
           <tr>
               <td style="width: 93px; height: 33px">Consorcio</td>
               <td style="height: 33px">
                   <asp:DropDownList ID="ddlConsorcios" runat="server" Width="250px">
                   </asp:DropDownList>
               </td>
           </tr>
           <tr>
               <td style="width: 93px; height: 33px">UF</td>
               <td style="height: 33px">
                   <asp:DropDownList ID="ddlUF" runat="server" Height="20px" Width="250px">
                   </asp:DropDownList>
               </td>
           </tr>
       </table>
    </div>

    <h3>Buscar por Propietario</h3>
    <div style="height: 169px;">
        Buscar por propietario
</div>

    </div>
    </form>
</asp:Content>