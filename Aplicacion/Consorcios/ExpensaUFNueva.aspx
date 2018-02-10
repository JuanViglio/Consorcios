<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpensaUFNueva.aspx.cs" Inherits="WebSistemmas.Consorcios.ExpensaUFNueva" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <p style="color: #003399; font-size: large">
            <asp:scriptmanager id="scrActualizarGastos" runat="server">
            </asp:scriptmanager>
        </p>
        <p style="color: #003399; font-size: large">
            &nbsp;
        </p>
        <p style="color: #003399; font-size: large">
            Nueva Expensa
            de la Unidad Funcional
        </p>
        <p style="color: #003399; font-size: large">
            &nbsp;&nbsp;
        </p>
        <p style="color: #003399; font-size: large"></p>
        <div id="divError" runat="server" style="color: #003399; font-size: large; height: 37px;">
            <asp:label id="lblError" runat="server" forecolor="Red" style="text-align: left; font-size: large;"></asp:label>
        </div>
        <table>
            <tr>
                <td style="width: 668px">
                    <div id="accordion">
                        <h3>Ingreso de Gastos Ordinarios</h3>
                        <div>
                            <table>
                                <tr>
                                    <td style="width: 643px; height: 226px;">
                                        <asp:gridview id="grdGastosOrdinarios" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowcommand="grdGastosOrdinarios_RowCommand" onrowdatabound="grdGastosOrdinarios_RowDataBound" style="margin-top: 0px; margin-left: 0px;" width="622px">
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
<%--                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                                        </asp:gridview>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <h3>Ingreso de Gastos Eventuales Ordinarios</h3>
                        <div>
                            <table>
                                <tr>
                                    <td style="width: 643px; height: 196px;">
                                        <asp:gridview id="grdGastosEventuales" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" style="margin-top: 0px; margin-left: 0px;" width="622px" onrowcommand="grdGastosEventuales_RowCommand" onrowdatabound="grdGastosEventuales_RowDataBound">
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
                                            <%--<SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                                        </asp:gridview>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <h3>Ingreso de Gastos Eventuales Extraordinarios</h3>
                        <div style="width: 652px">
                            <table>
                                <tr>
                                    <td style="width: 643px">
                                        <asp:gridview id="grdGastosExtraordinarios" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" style="margin-top: 0px; margin-left: 0px;" width="622px" onrowcommand="grdGastosExtraordinarios_RowCommand" onrowdatabound="grdGastosExtraordinarios_RowDataBound">
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
                                <%--<SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />--%>
                            </asp:gridview>
                                <br />
                                </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
                <td style="width: 14px"></td>
                <td style="width: 504px">
                    <table>
                        <tr>
                            <td style="width: 165px">Coeficiente    
                            </td>
                            <td>
                                <asp:label id="txtCoeficiente" runat="server" style="color: #003399" width="154px"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 165px">Importe 1 Vencimietno
                            </td>
                            <td>
                                <asp:label id="txtVencimiento1" runat="server" style="color: #003399" width="154px"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 165px">Importe Extraordinario
                            </td>
                            <td>
                                <asp:label id="txtImporteExtraordinario" runat="server" style="color: #003399" width="154px"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 29px"></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="3" style="height: 43px">Gastos Particulares
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 18px; width: 98px">Descripcion
                            </td>
                            <td style="height: 18px" colspan="2">

                                <asp:textbox id="txtDetalleGastoParticular" runat="server" width="385px"></asp:textbox>

                            </td>
                        </tr>
                        <tr>
                            <td style="width: 98px; height: 38px;">Importe
                            </td>
                            <td style="width: 207px; height: 38px">
                                <asp:textbox id="txtImporteGastoParticular" runat="server" width="108px"></asp:textbox>
                            </td>
                            <td style="width: 233px; height: 38px;">
                                <asp:button id="btnActualizar" runat="server" text="Actualizar" height="29px" width="99px" onclick="btnActualizar_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


        <table>
            <tr>
                <td style="height: 52px; text-align: right; width: 70px;">
                    <asp:label id="Label5" runat="server" text="Total:   $" font-size="Large" style="color: #003399"></asp:label>
                </td>
                <td style="width: 153px; height: 52px;">
                    <asp:label id="lblTotalGastosOrdinarios" runat="server" font-size="Large" style="color: #003399"></asp:label>
                </td>
                <td style="width: 173px">
                    <asp:button runat="server" text="Volver" id="btnVolver" height="35px" width="150px" onclick="btnVolver_Click"></asp:button>
                </td>
                <td style="width: 190px">&nbsp;</td>

            </tr>
        </table>

    </form>

</asp:Content>


