<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpensaUFNueva.aspx.cs" Inherits="WebSistemmas.Consorcios.ExpensaUFNueva" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>
<%@ Register TagPrefix="uc" TagName="gridGastosFijosUF" Src="~/Consorcios/UserControls/ExpensasUF/GridGastosFijosUF.ascx" %>
<%@ Register TagPrefix="uc" TagName="gridGastosEvOrdUF" Src="~/Consorcios/UserControls/ExpensasUF/GridGastosEvOrdUF.ascx" %>
<%@ Register TagPrefix="uc" TagName="gridGastosEvExtUF" Src="~/Consorcios/UserControls/ExpensasUF/GridGastosEvExtUF.ascx" %>
<%@ Register TagPrefix="uc" TagName="subtotalesUF" Src="~/Consorcios/UserControls/ExpensasUF/SubtotalesUF.ascx" %>
<%@ Register TagPrefix="uc" TagName="totalesUF" Src="~/Consorcios/UserControls/ExpensasUF/TotalesUF.ascx" %>
<%@ Register src="~/Consorcios/UserControls/Error.ascx" tagname="UserControl2" tagprefix="uc6" %>

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
        <uc6:UserControl2 ID="UserControl2ID" runat="server" />
        <table style="margin-top: 24px">
            <tr>
                <td style="width: 641px;vertical-align:top;">
                    <div id="accordion" style="width: 631px">

                        <h3>Gastos Fijos</h3>
                        <div>
                            <uc:gridGastosFijosUF id="MyPartialView" runat="server" />
                        </div>

                        <h3>Gastos Eventuales Ordinarios</h3>
                        <div>
                            <uc:gridGastosEvOrdUF id="GridGastosEvOrdUF" runat="server" />
                        </div>

                        <h3>Gastos Eventuales Extraordinarios</h3>
                        <div style="height: 169px;">
                            <uc:gridGastosEvExtUF id="GridGastosEvExtUF" runat="server" />
                        </div>
                    </div>

                    <br />
                    <uc:subtotalesUF id="SubtotalesUF" runat="server" />
                </td>
                <td style="width: 14px"></td>
                <td style="width: 504px">
                    <uc:totalesUF id="totalesUF" runat="server" />


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


