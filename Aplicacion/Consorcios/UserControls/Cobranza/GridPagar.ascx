<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridPagar.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Cobranza.GridPagar" %>
<div style="margin-left:50px">
    <asp:updatepanel id="UpdatePanel1" runat="server">
    <ContentTemplate> 
    <asp:GridView ID="grdPagar" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="51px" style="margin-top: 0px; margin-left: 0px; margin-right: 0px; margin-bottom: 25px;" Width="470px">
    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    <Columns>
        <asp:BoundField DataField="Direccion" HeaderText="Direccion">
        <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>                
        <asp:BoundField DataField="UF" HeaderText="UF">
        <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
        </asp:BoundField>
        <asp:BoundField DataField="ID" HeaderText="ID">
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
        <asp:Button ID="btnCobrar" runat="server" Text="Cobrar" Height="33px" OnClick="btnCobrar_Click" Width="92px" />
    </ContentTemplate>
    </asp:updatepanel> 
</div>
