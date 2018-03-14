<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridProveedores.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Proveedores.GridProveedores" %>
<script src="../../../js/Proveedores.js"></script>

<style type="text/css">
    .auto-style1 {
        margin-right: 25px;
        margin-bottom: 23px;
    }
</style>

<asp:GridView ID="grdProveedores" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdProveedores_RowCommand" Style="margin-top: 0px; margin-left: 0px; " Width="807px" CssClass="auto-style1" OnRowDataBound="grdProveedores_RowDataBound">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="Codigo">
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
        <asp:BoundField DataField="Saldo" HeaderText="Saldo">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
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
    </Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>