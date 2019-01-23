<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridProveedores.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Proveedores.GridProveedores" %>
<script src="../../../js/Proveedores.js"></script>

<style type="text/css">
    .auto-style1 {
        margin-right: 13px;
        margin-bottom: 23px;
        margin-top: 0px;
    }
    .auto-style2 {
        width: 639px;
        height: 14px;
        margin-top: 0px;
        margin-bottom: 20px;
    }
    .auto-style4 {
        width: 122px;
    }
    .auto-style5 {
        width: 90px;
    }
    .auto-style6 {
        width: 58px;
    }
    .auto-style7 {
        margin-left: 15px;
    }
</style>

<table class="auto-style2">
    <tr>
        <td class="auto-style6">
            Nombre</td>
        <td class="auto-style5">

            <asp:TextBox ID="txtNombreBuscar" runat="server" Width="284px"></asp:TextBox>

        </td>
        <td class="auto-style4">
            <asp:Button ID="btnBuscar" runat="server" Height="32px" OnClick="btnBuscar_Click" Text="Buscar" Width="88px" CssClass="auto-style7" />
        </td>
        <td>
            <asp:Button ID="btnLimpiar" runat="server" Height="32px" OnClick="btnLimpiar_Click" Text="Limpiar" Width="88px" />
        </td>
    </tr>
</table>
<asp:GridView ID="grdProveedores" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdProveedores_RowCommand" Style="margin-left: 0px; " Width="1317px" CssClass="auto-style1" OnRowDataBound="grdProveedores_RowDataBound" AllowPaging="True" OnPageIndexChanging="grdProveedores_PageIndexChanging">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="Codigo" HeaderText="Codigo">
            <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>
        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField>        
        <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion">
            <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>
        <asp:BoundField DataField="Mail" HeaderText="Mail">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField> 
        <asp:BoundField DataField="Telefono" HeaderText="Telefono">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField>                
        <asp:BoundField DataField="Tipo" HeaderText="Tipo">
            <ControlStyle Width="50px" />
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" Width="20%" />
        </asp:BoundField>  
        <asp:BoundField DataField="Saldo" HeaderText="Saldo">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" Width="10%"/>
        </asp:BoundField>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="div_parent">
                    <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="div_parent">
                    <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Eliminar" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="div_parent">
                    <asp:ImageButton ID="CtaCte" runat="server" CausesValidation="False" CommandName="CtaCte" ImageUrl="~/css/img/ico_pesos.png" ToolTip="Cta. Corriente" />
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
<asp:Label ID="lblPagina" runat="server" Text="Pagina 1"></asp:Label>
<p>
    &nbsp;</p>
