<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridSeguros.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Seguros.GridSeguros" %>

<style type="text/css">
    .auto-style1 {
        margin-right: 13px;
        margin-bottom: 23px;
    }
</style>

<asp:GridView ID="grdSeguros" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" Style="margin-top: 0px; margin-left: 0px; " Width="1226px" CssClass="auto-style1">
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
        <asp:BoundField DataField="Tipo" HeaderText="Tipo">
            <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>
        <asp:BoundField DataField="FechaInicio" HeaderText="Fecha Inicio" DataFormatString="{0:d}">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField> 
        <asp:BoundField DataField="FechaFin" HeaderText="Fecha Fin" DataFormatString="{0:d}">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
        </asp:BoundField>                
        <asp:BoundField DataField="Estado" HeaderText="Estado">
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center"/>
        </asp:BoundField>  
        <asp:BoundField DataField="Consorcio" HeaderText="Consorcio">
            <ControlStyle Width="60px" />
            <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center"/>
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
