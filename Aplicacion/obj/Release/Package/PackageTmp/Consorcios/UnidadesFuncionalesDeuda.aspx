<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnidadesFuncionalesDeuda.aspx.cs" Inherits="WebSistemmas.Consorcios.UnidadesFuncionalesDeuda" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register TagPrefix="ucUnidadesFuncionalesDeuda" TagName="gridUnidadesFuncionalesDeuda" Src="~/Consorcios/UserControls/UnidadesFuncionalesDeuda/GridUnidadesFuncionalesDeuda.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">
    <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
        <ucUnidadesFuncionalesDeuda:gridUnidadesFuncionalesDeuda ID="gridUnidadesFuncionalesDeudaID" runat="server" />
        <br />
        <asp:Button ID="btnVolver" runat="server" Height="36px" OnClick="btnVolver_Click" Text="Volver" Width="100px" />
    </form>

</asp:Content>
