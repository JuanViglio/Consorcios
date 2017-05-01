<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpensaNueva.aspx.cs" Inherits="WebSistemmas.Consorcios.ExpensaNueva" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <p style="color: #003399; font-size: large">
        &nbsp;</p>
        <p style="color: #003399; font-size: large">
        Nueva Expensa</p>
        <p style="color: #003399; font-size: large">
        &nbsp;</p>
        <p style="color: #003399; font-size: large"></p>
        <span style="font-size: medium; color: #003399;">Ingreso de Gastos Ordinarios</span>
        <table>
            <tr>
                <td valign="top" style="width: 530px; height: 226px;">
                    <div id="divExpensaNueva" style="margin-top: 17px;  " >
                        <table style="margin-top: 0px; width: 500px;">
                            <tr>
                                <td style="width: 100px; height: 23px;">
                                    <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                                </td>
                                <td style="height: 23px">
                                    <asp:DropDownList ID="ddlTipoGastos" runat="server" Height="17px" Width="385px" OnSelectedIndexChanged="ddlTipoGastos_SelectedIndexChanged" onchange="cambioTipoGastos(this)">
                    
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                    <asp:Label ID="Label2" runat="server" Text="Detalle"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetalle" runat="server" Width="380px" style="margin-left: 0px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 23px;">
                                    <asp:Label ID="Label4" runat="server" Text="Importe"></asp:Label>
                                </td>
                                <td style="height: 23px">
                                    <asp:TextBox ID="txtImporte" runat="server" Width="380px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 53px">
                                    <asp:Button ID="btnAgregarGastoOrdinario" runat="server" Height="30px" OnClick="btnAgregarGastoOrdinario_Click" Text="Agregar" Width="90px" />
                                </td>
                                <td style="height: 53px">
                                    <asp:Button ID="btnCancelar" runat="server" Height="30px" Text="Volver" Width="90px" OnClick="btnCancelar_Click" />                                
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width: 643px; height: 226px;">
                    <asp:GridView ID="grdGastosOrdinarios" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdGastosOrdinarios_RowCommand" style="margin-top: 0px; margin-left: 0px;" Width="622px" OnRowDataBound="grdGastosOrdinarios_RowDataBound">
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
                                        <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Eliminar" />
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="div_parent">
                                        <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar"/>
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
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                    <br />
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" Text="Total:   $" Font-Size="Large" style="color: #003399"></asp:Label>
                            </td>
                            <td style="width: 97px">
                                <asp:Label ID="lblTotalGastosOrdinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
        <span style="font-size: medium; color: #003399;">Ingreso de Gastos Eventuales </span>
        <table>
            <tr>
                <td valign="top" style="width: 530px">
                    <div id="div1" style="margin-top: 17px;  " >
                        <table style="margin-top: 0px; width: 500px;">
                            <tr>
                                <td style="width: 100px">
                                    <asp:Label ID="Label6" runat="server" Text="Detalle"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDetalleGastoEventual" runat="server" Width="380px" style="margin-left: 0px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 23px;">
                                    <asp:Label ID="Label7" runat="server" Text="Importe"></asp:Label>
                                </td>
                                <td style="height: 23px">
                                    <asp:TextBox ID="txtImporteGastoEventual" runat="server" Width="380px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100px; height: 53px">
                                    <asp:Button ID="btnAgregarGastoEventual" runat="server" Height="30px" OnClick="btnAgregarGastoEventual_Click" Text="Agregar" Width="90px" />
                                </td>
                                <td style="height: 53px">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width: 643px">
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
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="div_parent">
                                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Modificar" />
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="div_parent">
                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
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
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
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

    </form>

</asp:Content>

