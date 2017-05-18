<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpensaUFNueva.aspx.cs" Inherits="WebSistemmas.Consorcios.ExpensaUFNueva" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <p style="color: #003399; font-size: large">
            <asp:ScriptManager ID="scrActualizarGastos" runat="server">
            </asp:ScriptManager>
        </p>
        <p style="color: #003399; font-size: large">
            &nbsp;</p>
        <p style="color: #003399; font-size: large">
        Nueva Expensa</p>
        <p style="color: #003399; font-size: large">
            &nbsp;&nbsp;</p>
        <p style="color: #003399; font-size: large"></p>
       
        <table>
            <tr>
                <td> 
                    <div id="accordion">
                    <h3>Ingreso de Gastos Ordinarios</h3>                
                        <div>
                            <table>
                                <tr>
                                    <td style="width: 643px; height: 226px;">
                                        <asp:GridView ID="grdGastosOrdinarios" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdGastosOrdinarios_RowCommand" OnRowDataBound="grdGastosOrdinarios_RowDataBound" style="margin-top: 0px; margin-left: 0px;" Width="622px">
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
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server" Font-Size="Large" style="color: #003399" Text="Previsión para gastos Extraordinarios     $" Width="335px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="txtGastosExtraordinarios" runat="server" Font-Size="Large" style="color: #003399" Width="154px"></asp:Label>
                                                </td>
                                                <td align="right" style="width : 100px">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
        
                    <h3>Ingreso de Gastos Eventuales</h3>
                        <div>
                            <table>
                                <tr>
                                    <td style="width: 643px; height: 196px;">
                                        <asp:GridView ID="grdGastosEventuales" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" style="margin-top: 0px; margin-left: 0px;" Width="622px" OnRowCommand="grdGastosEventuales_RowCommand" OnRowDataBound="grdGastosEventuales_RowDataBound">
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
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                        <br />
                                        <br />
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Total:   $" Font-Size="Large" style="color: #003399; font-size: medium;"></asp:Label>
                                                </td>
                                                <td style="width: 97px">
                                                    <asp:Label ID="lblTotalGastosEventuales" runat="server" Font-Size="Large" style="color: #003399; font-size: medium;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </td>                
                                </tr>
                            </table>           
                        </div>

                    <h3>Ingreso de Gastos Extraordinarios</h3>
                            <div style="width: 652px">        
                                <table>
                                    <tr>
                        <td style="width: 643px">
                            <asp:GridView ID="grdGastosExtraordinarios" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" style="margin-top: 0px; margin-left: 0px;" Width="622px" OnRowCommand="grdGastosExtraordinarios_RowCommand" OnRowDataBound="grdGastosExtraordinarios_RowDataBound">
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
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                            <br />
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td style="width: 97px">
                                        <asp:Label ID="lblTotalGastosExtraordinarios" runat="server" Font-Size="Large" style="color: #003399; font-size: medium;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                                </table>           
                            </div>
                    </div>
                </td>
                <td style="width: 325px">

                    Coeficiente
                    <asp:TextBox ID="txtCoeficiente" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    Importe 1 Vencimietno&nbsp;<asp:TextBox ID="txtVencimiento1" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    Importe Extraordinario <asp:TextBox ID="txtImporteExtraordinario" runat="server"></asp:TextBox>

                </td>
            </tr>
        </table>


        <table>
            <tr>
                <td style="height: 52px; text-align: right; width: 70px;">
                    <asp:Label ID="Label5" runat="server" Text="Total:   $" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td style="width: 153px; height: 52px;">
                    <asp:Label ID="lblTotalGastosOrdinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td style="width: 173px">
                    <asp:Button runat="server" Text="Volver" ID="btnVolver" Height="35px" Width="150px"></asp:Button>
                </td>
                <td style="width: 190px">
                    <asp:Button runat="server" Text="Aceptar Expensa" ID="btnAceptar" Height="34px" Width="150px"></asp:Button>
                </td>

            </tr>
        </table>
               
        </form>

</asp:Content>


