<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CtaCteProveedor.aspx.cs" Inherits="WebSistemmas.Consorcios.CtaCteProveedor" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">
        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />

        <asp:GridView ID="grdCtaCteProveedores" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="140px" style="margin-top: 0px; margin-left: 0px;" Width="468px">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Debe" HeaderText="Debe">
                <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                </asp:BoundField>
                <asp:BoundField DataField="Haber" HeaderText="Haber">
                <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        </asp:GridView>
    </form>
</asp:Content>