<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridSeguros.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Seguros.GridSeguros" %>

<style type="text/css">
    .auto-style1 {
        margin-right: 13px;
        margin-bottom: 23px;
    }
</style>

<asp:GridView ID="grdProveedores" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdProveedores_RowCommand" Style="margin-top: 0px; margin-left: 0px; " Width="1095px" CssClass="auto-style1" OnRowDataBound="grdProveedores_RowDataBound">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="ID" HeaderText="ID">
            <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>
        <asp:BoundField DataField="Compañia" HeaderText="Compañia">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField>        
        <asp:BoundField DataField="Poliza" HeaderText="Poliza">
            <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>
        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField> 
        <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField>                
        <asp:BoundField DataField="Estado" HeaderText="Estado">
            <ControlStyle Width="50px" />
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" Width="20%" />
        </asp:BoundField>  
        <asp:BoundField DataField="Consorcio" HeaderText="Consorcio">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" Width="10%"/>
        </asp:BoundField>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="div_parent">
                    <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
                </div>
            </ItemTemplate>
        </asp:TemplateField>
<%--        <asp:TemplateField>
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
        </asp:TemplateField>--%>
    </Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
</asp:GridView>
