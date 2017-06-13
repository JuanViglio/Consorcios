<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consorcios.aspx.cs" Inherits="WebSistemmas.Consorcios.Consorcios" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

<form id="form1" runat="server">
<body>
    <table>
        <tr>
            <td style="width: 676px">
                <span style="font-size: large; color: #003399">
                <br />
                Consorcios</span><br />
                <asp:GridView ID="grdConsorcios" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="150px" OnRowCommand="grdConsorcios_RowCommand" style="margin-top: 0px; margin-left: 0px;" Width="655px">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="Codigo">
                        <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                        </asp:BoundField>                
                        <asp:BoundField DataField="Direccion" HeaderText="Direccion" SortExpression="Direccion">
                        <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PrimerVencimiento" HeaderText="1er Vencimiento">
                        <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SegundoVencimiento" HeaderText="2do Vencimiento">
                        <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="InteresSegundoVencimiento" HeaderText="Interes">
                        <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="div_parent">
                                    <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
                                    <asp:ImageButton ID="UnidadesFuncionales" runat="server" CausesValidation="False" CommandName="UnidadesFuncionales" ImageUrl="~/css/img/home.png" ToolTip="Unidades Funcionales" />
                                    <asp:ImageButton ID="Expensas" runat="server" CausesValidation="False" CommandName="Expensas" ImageUrl="~/css/img/ico_pesos.png" ToolTip="Expensas" />
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
            </td>
            <td valign="top" style="width: 525px">
                <div id="divConsorciosModificar" style="margin-top: 30px; display: none " >
                    <table style="margin-top: 47px; width: 375px;">
                        <tr>
                            <td style="width: 130px">
                                <asp:Label ID="Label2" runat="server" Text="Codigo"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:TextBox ID="txtCodigo" runat="server" Width="155px" style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px; height: 23px;">
                                <asp:Label ID="Label4" runat="server" Text="Direccion"></asp:Label>
                            </td>
                            <td style="height: 23px" colspan="2">
                                <asp:TextBox ID="txtDireccion" runat="server" Width="156px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px; height: 23px;">
                                <asp:Label ID="Label1" runat="server" Text="1er Vencimiento"></asp:Label>
                            </td>
                            <td style="height: 23px" colspan="2">
                                <asp:TextBox ID="txtVencimiento1" runat="server" Width="156px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px; height: 23px;">
                                <asp:Label ID="Label3" runat="server" Text="2do Vencimiento"></asp:Label>
                            </td>
                            <td style="height: 23px" colspan="2">
                                <asp:TextBox ID="txtVencimiento2" runat="server" Width="156px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px; height: 23px;">
                                <asp:Label ID="Label5" runat="server" Text="Interes"></asp:Label>
                            </td>
                            <td style="height: 23px" colspan="2">
                                <asp:TextBox ID="txtInteres" runat="server" Width="156px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px; height: 53px">
                                <asp:Button ID="btnAceptarModificar" runat="server" Height="30px" OnClick="btnAceptarModificar_Click" Text="Modificar" Width="90px" />
                            </td>
                            <td style="height: 53px" colspan="2">
                                <asp:Button ID="btnCancelarModificar" runat="server" Height="30px" Text="Cancelar" Width="90px" OnClientClick="CerrarDivInquilinoModificar(); return false;" />                                
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnNuevoConsorcio" runat="server" Height="35px" OnClick="btnNuevoConsorcio_Click" Text="Nuevo" Width="107px" />
    <table><tr><td style="height: 59px">
        <asp:Label ID="lblError" runat="server" Font-Size="Large" ForeColor="#FF6600" style="margin-left: 13px;"></asp:Label>
    </td></tr></table>
</body>

</form>

</asp:Content>
