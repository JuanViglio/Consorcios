<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnidadesFuncionalesCtaCte.aspx.cs" Inherits="WebSistemmas.Consorcios.UnidadesFuncionalesCtaCte" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register TagPrefix="ucGridCtaCteProveedores" TagName="gridUnidadesFuncionalesCtaCte" Src="~/Consorcios/UserControls/UnidadesFuncionalesCtaCte/GridUnidadesFuncionalesCtaCte.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">
        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
        <ucGridCtaCteProveedores:gridUnidadesFuncionalesCtaCte ID="gridUnidadesFuncionalesCtaCteID" runat="server" />
    </form>
</asp:Content>