﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gastos.aspx.cs" Inherits="WebSistemmas.Consorcios.Gastos" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script src="../js/Gastos.js"></script>

    <form id="form1" runat="server">
        <p style="font-size: large">
            <br />
            <span style="font-size: large; color: #003399">Gastos</span>
        </p>
        <p style="font-size: large">
            &nbsp;
        </p>
        <p>
            <asp:gridview id="grdGastos" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowcommand="grdGastos_RowCommand" style="margin-top: 0px; margin-left: 0px; margin-right: 30px;" width="395px">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="detalle" HeaderText="Detalle">
                        <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
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
                </asp:gridview>
        </p>
        <p>
            &nbsp;
        </p>
        <table style="width: 417px; height: 37px">
            <tr>
                <td style="width: 56px; height: 21px;">
                    <asp:label id="Label1" runat="server" text="Tipo"></asp:label>
                </td>
                <td style="width: 239px; height: 21px">
                    <asp:dropdownlist id="ddlTipoGastos" runat="server" height="17px" width="217px" autopostback="True" onselectedindexchanged="ddlTipoGastos_SelectedIndexChanged" style="margin-right: 24px; margin-left: 0px;">
                    </asp:dropdownlist>
                </td>
                <td colspan="2">
                    <asp:button id="btnNuevoGasto" runat="server" text="Nuevo Gasto" height="32px" onclientclick="SlideDivGastoDatos(); return false;" width="103px" style="margin-left: 0px" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="4">
                    <div id="divGastoDatos" style="margin-top: 30px; display: none">
                        <table style="margin-top: 0px; width: 513px; height: 85px;">
                            <tr>
                                <td style="width: 120px">Nombre del Gasto</td>
                                <td colspan="2" style="width: 200px">
                                    <asp:textbox id="txtDetalleGasto" runat="server" width="309px" style="margin-left: 0px"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 120px; height: 53px">
                                    <asp:button id="btnAceptarNuevoConsorcio" runat="server" height="30px" onclick="btnAceptarNuevoGasto_Click" text="Agregar" width="90px" />
                                </td>
                                <td style="height: 53px; width: 200px;" colspan="2">
                                    <asp:button id="btnCancelarDatos" runat="server" height="30px" text="Cancelar" width="90px" onclientclick="CerrarDivConsorcioDatos(); return false;" />
                                </td>
                            </tr>
                        </table>

                    </div>
                </td>
            </tr>
        </table>
    </form>

</asp:Content>