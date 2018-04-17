<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeguroNuevo.aspx.cs" Inherits="WebSistemmas.Consorcios.SeguroNuevo" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script src="../js/Proveedores.js"></script>

    <form id="form1" runat="server">

        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
    
    </form>
</asp:Content>
