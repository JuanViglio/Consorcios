<%@ Control Language="C#"  AutoEventWireup="true" CodeBehind="GridGastosFijosUF.ascx.cs" Inherits="WebSistemmas.Consorcios.PaginasParciales.GridGastosFijosUF" %>

<table style="width: 579px">
    <tr>
        <td style="width: 643px; height: 226px;">
            <asp:gridview id="grdGastosOrdinarios" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowdatabound="grdGastosOrdinarios_RowDataBound" style="margin-top: 0px; margin-left: 0px;" width="576px">
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