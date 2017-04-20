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
        <p style="color: #003399; font-size: large">
            &nbsp;</p>
        <p style="color: #003399; font-size: large">
            <span style="font-size: medium">Ingreso de Gastos</span><table>
                <tr>
                    <td valign="top" style="width: 435px">
                        <div id="divExpensaNueva" style="margin-top: 17px;  " >
                            <table style="margin-top: 0px; width: 375px;">
                                <tr>
                                    <td style="width: 100px; height: 23px;">
                                        <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                                    </td>
                                    <td style="height: 23px">

                    <asp:DropDownList ID="ddlTipoGastos" runat="server" Height="17px" Width="250px" OnSelectedIndexChanged="ddlTipoGastos_SelectedIndexChanged" onchange="cambioTipoGastos(this)">
                    
                    </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Label ID="Label2" runat="server" Text="Detalle"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDetalle" runat="server" Width="248px" style="margin-left: 0px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; height: 23px;">
                                        <asp:Label ID="Label4" runat="server" Text="Importe"></asp:Label>
                                    </td>
                                    <td style="height: 23px">
                                        <asp:TextBox ID="txtImporte" runat="server" Width="248px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px; height: 53px">
                                        <asp:Button ID="btnAgregar" runat="server" Height="30px" OnClick="btnAgregarExpensa_Click" Text="Agregar" Width="90px" />
                                    </td>
                                    <td style="height: 53px">
                                        <asp:Button ID="btnCancelar" runat="server" Height="30px" Text="Volver" Width="90px" OnClientClick="JavaScript:window.history.back(1); return false;" OnClick="btnCancelar_Click" />                                
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td style="width: 643px">
                        <asp:GridView ID="grdExpensas" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdExpensas_RowCommand" style="margin-top: 0px; margin-left: 0px;" Width="622px">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:BoundField DataField="Detalle" HeaderText="Detalle">
                                <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Importe" HeaderText="Importe">
                                <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="TipoGasto_ID" HeaderText="Tipo">
                                <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="div_parent">
                                            <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/assets/img/ico_eliminar.png" ToolTip="Modificar" />
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
                                    <asp:Label ID="Label5" runat="server" Text="Total:   $" Font-Size="Large"></asp:Label>
                                </td>
                                <td style="width: 97px">
                                    <asp:Label ID="lblTotal" runat="server" Font-Size="Large"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnAceptar" runat="server" Height="30px" OnClick="btnAgregarExpensa_Click" Text="Aceptar" Width="90px" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </td>
                </tr>
            </table>
            </p>
        <p style="color: #003399; font-size: large">
            &nbsp;</p>
        <p style="color: #003399; font-size: large">
            &nbsp;</p>


    </form>


</asp:Content>

