﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Caja.aspx.cs" Inherits="WebSistemmas.Consorcios.Caja" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">
        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
    </form>
</asp:Content>

