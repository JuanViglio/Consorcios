<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridSeguroDetalle.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Seguros.GridSeguroDetalle" %>
<script src="../../../js/Consorcios/Seguros.js"></script>

<style type="text/css">

    .auto-style1 {
        margin-right: 0px;
        margin-bottom: 23px;
        margin-left: 40px;
    }
    .auto-style2 {
        width: 98px;
    }
    .auto-style3 {
        height: 55px;
        width: 98px;
    }
    .auto-style4 {
        height: 55px;
    }
    .auto-style5 {
        margin-left: 1px;
    }
    .auto-style7 {
        height: 91px;
    }
</style>

<asp:GridView ID="grdSeguros" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" Style="margin-top: 0px; " Width="346px" CssClass="auto-style1" OnRowCommand="grdSeguros_RowCommand">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="Cuota" HeaderText="Cuota">
            <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>
        <asp:BoundField DataField="Periodo" HeaderText="Periodo">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField>        
        <asp:BoundField DataField="Importe" HeaderText="Importe">
            <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>

        <asp:TemplateField>
            <ItemTemplate>
                <div class="div_parent">
                    <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>

<div id="divImporteSeguros" style="display: none">
    <table class="auto-style7">
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Importe"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtImporte" runat="server" CssClass="auto-style5" Width="80px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <asp:Button ID="btnAceptar" runat="server" Height="31px" Text="Aceptar" Width="90px" OnClick="btnAceptar_Click" />
            </td>
            <td class="auto-style4">
                <asp:Button ID="btnCancelar" runat="server" Height="31px" Text="Cancelar" Width="90px" OnClientClick="OcultarDivImporteDatos(); return false;" />
            </td>
        </tr>
    </table>
</div>
