<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpensaUFNueva.aspx.cs" Inherits="WebSistemmas.Consorcios.ExpensaUFNueva" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <link href="../css/Paginas.css" rel="Stylesheet" />

    <form id="form1" runat="server">
        <p style="color: #003399; font-size: large">
            <asp:scriptmanager id="scrActualizarGastos" runat="server">
            </asp:scriptmanager>
        </p>
        <p style="color: #003399; font-size: large">
            &nbsp;
        </p>
        <p style="color: #003399; font-size: large">
            Nueva Expensa de la Unidad Funcional
            <asp:Label ID="lblNombreUF" runat="server"></asp:Label>
        </p>
        <div id="divError" runat="server" style="color: #003399; font-size: large; height: 45px;" align="left">
            <br />
            <asp:label id="lblError" runat="server" forecolor="Red" style="text-align: left; font-size: large;"></asp:label>
        </div>
        <table style="margin-top: 24px">
            <tr>
                <td style="width: 641px;vertical-align:top;">
                    <div id="accordion" style="width: 631px">
                        <h3>Gastos Fijos</h3>
                        <div>
                            <table style="width: 579px">
                                <tr>
                                    <td style="width: 643px; height: 226px;">
                                        <asp:gridview id="grdGastosOrdinarios" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowcommand="grdGastosOrdinarios_RowCommand" onrowdatabound="grdGastosOrdinarios_RowDataBound" style="margin-top: 0px; margin-left: 0px;" width="576px">
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
                        </div>

                        <h3>Gastos Eventuales Ordinarios</h3>
                        <div>
                            <table>
                                <tr>
                                    <td style="width: 643px; height: 196px;">
                                        <asp:gridview id="grdGastosEventuales" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" style="margin-top: 0px; margin-left: 0px;" width="582px" onrowcommand="grdGastosEventuales_RowCommand" onrowdatabound="grdGastosEventuales_RowDataBound">
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
                                        <br />
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
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <h3>Gastos Eventuales Extraordinarios</h3>
                        <div style="height: 169px;">
                            <table>
                                <tr>
                                    <td style="width: 625px">
                                        <asp:gridview id="grdGastosExtraordinarios" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" style="margin-top: 0px; margin-left: 0px;" width="580px" onrowcommand="grdGastosExtraordinarios_RowCommand" onrowdatabound="grdGastosExtraordinarios_RowDataBound">
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
                                <br />
                                </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                    </div>

                    <br />
                    <table style="width: 615px">
                        <tr>
                            <td style="height: 45px; text-align: left; width: 142px;">
                                <asp:Label ID="Label5" runat="server" Text="Subtotal Ord.:" Font-Size="Large" style="color: #003399"></asp:Label>
                            </td>
                            <td style="width: 85px; height: 45px;" align="right">
                                <asp:Label ID="lblTotalGastosOrdinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                            </td>
                            <td style="width: 157px; height: 45px;">
                                <asp:button runat="server" text="Volver" id="btnVolver" height="35px" width="137px" onclick="btnVolver_Click" style="margin-top: 8px; margin-left: 55px;"></asp:button>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 142px; height: 40px;">
                                <asp:Label ID="Label1" runat="server" Text="Subtotal Extraord." Font-Size="Large" style="color: #003399"></asp:Label>
                            </td>
                            <td style="height: 40px; width: 85px;" align="right">
                                <asp:Label ID="lblTotalGastosExtraordinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                            </td>
                            <td style="height: 40px"></td>
                        </tr>
                        <tr>
                            <td style="width: 142px; height: 38px;">
                                <asp:Label ID="Label10" runat="server" Text="Total Gastos:" Font-Size="Large" style="color: #003399"></asp:Label>
                            </td>
                            <td  style="height: 38px; width: 85px;" align="right">
                                <asp:Label ID="lblTotalGastos" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td style="width: 14px"></td>
                <td style="width: 504px">
                    <table>
                        <tr>
                            <td style="width: 201px; height: 28px;">Coeficiente    
                            </td>
                            <td align="right" style="width: 70px; height: 28px">
                                <asp:label id="lblCoeficiente" runat="server" style="color: #003399" width="60px"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 201px">
                                Subtotal Gastos Ordinario
                            </td>
                            <td align="right" style="width: 70px">
                                <asp:label id="lblSubtotalGastoOrdinario" runat="server" style="color: #003399" width="60px"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 201px; height: 20px;">Subtotal Gastos Extraordinario
                            </td>
                            <td align="right" style="width: 70px; height: 20px">
                                <asp:label id="lblSubtotalGastoExt" runat="server" style="color: #003399" width="60px"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 201px; height: 20px;">Subtotal Gastos Cochera Ord.</td>
                            <td style="height: 20px; width: 70px;" align="right">
                                <asp:label id="lblSubtotalGastoCocherarOrd" runat="server" style="color: #003399" width="60px"></asp:label>
                            </td>
                        </tr>                        
                        <tr>
                            <td style="width: 201px">Subtotal Gastos Cochera Ext.
                            </td>
                            <td align="right" style="width: 70px">
                                <asp:label id="lblSubtotalGastoCocheraExt" runat="server" style="color: #003399" width="60px"></asp:label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 201px">Subtotal Gasto Particular</td>
                            <td align="right" style="width: 70px">
                                <asp:label id="lblSubtotalGastoParicular" runat="server" style="color: #003399" width="60px"></asp:label>
                            </td>
                        </tr>
                        <tr style="font-weight: bold">
                            <td style="width: 201px; height: 30px;">Importe 1 Vencimietno
                            </td>
                            <td align="right" style="width: 70px; height: 30px">
                                <asp:label id="lblVencimiento1" runat="server" style="color: #003399" width="60px"></asp:label>
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
                                <asp:button id="btnActualizar" runat="server" text="Actualizar" height="29px" width="99px" onclick="btnActualizar_Click" UseSubmitBehavior="False" />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 51px">

                                <asp:Button ID="btnAnterior" runat="server" Height="28px" OnClick="btnAnterior_Click" Text="Anterior" UseSubmitBehavior="False" Width="95px" style="margin-right: 15px" />

                            </td>
                            <td style="height: 51px">
                                <asp:Button ID="btnProximo" runat="server" Height="28px" OnClick="btnProximo_Click" Text="Proximo" UseSubmitBehavior="False" Width="95px" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:gridview id="grdGastosParticularesOrd" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" style="margin-top: 0px; margin-left: 0px;" width="582px" onrowcommand="grdGastosParticularesOrd_RowCommand" onrowdatabound="grdGastosParticularesOrd_RowDataBound" Caption="Gastos Particulares Ordinarios">
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
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="div_parent">
                                        <asp:ImageButton ID="EliminarGastoOrd" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Eliminar" />
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:gridview>
                    <br />
                    <asp:gridview id="grdGastosParticularesExt" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" style="margin-top: 0px; margin-left: 0px;" width="582px" onrowcommand="grdGastosParticularesExt_RowCommand" onrowdatabound="grdGastosParticularesExt_RowDataBound" Caption="Gastos Particulares Extraordinarios">
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
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="div_parent">
                                        <asp:ImageButton ID="EliminarGastoExt" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Eliminar" />
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
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

    </form>

</asp:Content>


