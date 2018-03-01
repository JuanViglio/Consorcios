<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridGastosEvOrdUF.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.ExpensasUF.GridGastosEvOrdUF" %>

<table>
    <tr>
        <td style="width: 643px; height: 196px;">
            <asp:gridview id="grdGastosEventuales" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" style="margin-top: 0px; margin-left: 0px;" width="582px" onrowdatabound="grdGastosEventuales_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="Detalle" HeaderText="Detalle">
                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Importe" HeaderText="Importe">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
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
            </asp:gridview>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Label ID="Label8" runat="server" Text="Total:" Font-Size="Large" style="color: #003399; font-size: medium;"></asp:Label>
        </td>
        <td style="width: 97px">
            <asp:Label ID="lblTotalGastosEventuales" runat="server" Font-Size="Large" style="color: #003399; font-size: medium;"></asp:Label>
        </td>
    </tr>
</table>
