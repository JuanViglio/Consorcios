<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpensaNueva.aspx.cs" Inherits="WebSistemmas.Consorcios.ExpensaNueva" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>
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
       
        <div id="accordion">
        <h3>Ingreso de Gastos Ordinarios</h3>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
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
                                                    <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
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
                                <table>
                                    <tr>
                                        <td style="height: 45px">
                                            <a href="GastoOrdinario.aspx#consorcios"><img src="../css/img/ico_mas.jpg" style="height: 27px; width: 26px" title="Agrear nuevo Gasto" /></a>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Font-Size="Large" style="color: #003399" Text="Previsión para gastos Extraordinarios" Width="420px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGastosExtraordinarios" runat="server" style="text-align: right" Width="95px">0</asp:TextBox>
                                        </td>
                                        <td align="right" style="width : 100px">
                                            <asp:Button ID="btnActualizarTotalExtraordinario" runat="server" Height="30px" OnClick="btnAgregarGastoExtraordinario_Click" Text="Actualizar" Width="87px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

        
        <h3>Ingreso de Gastos Eventuales</h3>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
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
                <a href="GastoEventual.aspx#consorcios">
                <img src="../css/img/ico_mas.jpg" style="height: 27px; width: 26px" title="Agrear nuevo Gasto" />
                </a>
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
            <%--<td valign="top" style="width: 530px; height: 196px;">
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
            </td>--%>

        </tr>
    </table>           
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>

        <h3>Ingreso de Gastos Extraordinarios</h3>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div>        
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
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="div_parent">
                                    <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Modificar" />
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="div_parent">
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
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
                <a href="GastoExtraordinario.aspx#consorcios">
                <img src="../css/img/ico_mas.jpg" style="height: 27px; width: 26px" title="Agrear nuevo Gasto" />
                </a>
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
            <%--<td valign="top" style="width: 530px">
                <div id="div2" style="margin-top: 17px;  " >
                    <table style="margin-top: 0px; width: 500px;">
                        <tr>
                            <td style="width: 100px">
                                <asp:Label ID="Label3" runat="server" Text="Detalle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDetalleGastoExtraordinario" runat="server" Width="380px" style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 23px;">
                                <asp:Label ID="Label9" runat="server" Text="Importe"></asp:Label>
                            </td>
                            <td style="height: 23px">
                                <asp:TextBox ID="txtImporteGastoExtraordinario" runat="server" Width="380px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 53px">
                                <asp:Button ID="btnAgregarGastoExt" runat="server" Height="30px" OnClick="btnAgregarGastoExt_Click" Text="Agregar" Width="90px" />
                            </td>
                            <td style="height: 53px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </div>
            </td>--%>

        </tr>
    </table>           
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>

        <table>
            <tr>
                <td style="height: 52px; text-align: right; width: 70px;">
                    <asp:Label ID="Label5" runat="server" Text="Total:   $" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td style="width: 153px; height: 52px;">
                    <asp:Label ID="lblTotalGastosOrdinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td style="width: 173px">
                    <asp:Button runat="server" Text="Volver" ID="btnVolver" Height="35px" OnClick="btnVolver_Click" Width="150px"></asp:Button>
                </td>
                <td style="width: 190px">
                    <asp:Button runat="server" Text="Aceptar Expensa" ID="btnAceptar" Height="34px" OnClick="btnAceptar_Click" Width="150px"></asp:Button>
                </td>

            </tr>
        </table>

        </ContentTemplate>
        </asp:UpdatePanel>
               
        </form>

</asp:Content>

