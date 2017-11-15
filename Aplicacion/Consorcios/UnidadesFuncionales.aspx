<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnidadesFuncionales.aspx.cs" Inherits="WebSistemmas.Consorcios.UnidadesFuncionales" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script src="../js/UnidadesFuncionales.js"></script>

    <form id="form1" runat="server">
        <div>
            <span style="color: #003399; font-size: large">
                <br />
                Unidades Funcionales<br />
            </span>
            <table>
                <tr>
                    <td colspan="2" style="height: 131px">
                        <asp:gridview id="grdUnidades" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowcommand="grdUnidades_RowCommand" style="margin-top: 0px; margin-left: 0px; margin-right: 32px;" width="622px" OnRowDataBound="grdUnidades_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="UF" HeaderText="Numero">
                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Departamento" HeaderText="Departamento">
                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Apellido" HeaderText="Apellido">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Coeficiente" HeaderText="Coeficiente">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ID" HeaderText="ID">
                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                    </asp:BoundField>
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
            </asp:gridview>
                        <br />
                    </td>
                    <td valign="top" style="width: 525px; height: 131px;">
                        <div id="divUFModificar" style="margin-top: 30px; display: none">
                            <table style="margin-top: 47px; width: 375px;">
                                <tr style ="visibility:hidden">
                                    <td style="width: 130px">
                                        <asp:label id="Label2" runat="server" text="ID"></asp:label>
                                    </td>
                                    <td colspan="2">
                                        <asp:textbox id="txtID" runat="server" width="155px" style="margin-left: 0px"></asp:textbox>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="width: 130px">
                                        <asp:label id="Label5" runat="server" text="Numero"></asp:label>
                                    </td>
                                    <td colspan="2">
                                        <asp:textbox id="txtNumero" runat="server" width="155px" style="margin-left: 0px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">
                                        <asp:label id="Label11" runat="server" text="Departamento"></asp:label>
                                    </td>
                                    <td colspan="2">
                                        <asp:textbox id="txtDepartamento" runat="server" width="155px" style="margin-left: 0px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 23px;">
                                        <asp:label id="Label4" runat="server" text="Apellido"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:textbox id="txtApellido" runat="server" width="156px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 23px;">
                                        <asp:label id="Label3" runat="server" text="Nombre"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:textbox id="txtNombre" runat="server" width="156px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 23px;">
                                        <asp:label id="Label1" runat="server" text="Coeficiente"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:textbox id="txtCoeficiente" runat="server" width="156px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 53px">
                                        <asp:button id="btnAceptarModificar" runat="server" height="30px" onclick="btnAceptarModificar_Click" text="Modificar" width="90px" />
                                    </td>
                                    <td style="height: 53px" colspan="2">
                                        <asp:button id="btnCancelarModificar" runat="server" height="30px" text="Cancelar" width="90px" onclientclick="CerrarDivUFModificar(); return false;" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnNuevoUF" runat="server" Height="35px" OnClientClick="SlideDivUFDatos(); return false;" Text="Nuevo" Width="107px" />
                        <asp:Button ID="btnVolver" runat="server" Height="35px" Text="Vovler" Width="107px" OnClick="btnVolver_Click" style="margin-left: 26px" />
                    </td>
                    <td style="width: 470px">
                        <div id="divUFDatos" style="margin-top: 30px; display: none">
                            <table style="margin-top: 47px; width: 375px;">
                                <tr>
                                    <td style="width: 130px">
                                        <asp:label id="Label6" runat="server" text="Numero"></asp:label>
                                    </td>
                                    <td colspan="2">
                                        <asp:textbox id="txtNumeroNuevo" runat="server" width="155px" style="margin-left: 0px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px">
                                        <asp:label id="Label10" runat="server" text="Departamento"></asp:label>
                                    </td>
                                    <td colspan="2">
                                        <asp:textbox id="txtDepartamentoNuevo" runat="server" width="155px" style="margin-left: 0px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 23px;">
                                        <asp:label id="Label7" runat="server" text="Apellido"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:textbox id="txtApellidoNuevo" runat="server" width="156px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 23px;">
                                        <asp:label id="Label9" runat="server" text="Nombre"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:textbox id="txtNombreNuevo" runat="server" width="156px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 23px;">
                                        <asp:label id="Label8" runat="server" text="Coeficiente"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:textbox id="txtCoeficienteNuevo" runat="server" width="156px"></asp:textbox>
                                    </td>
                                </tr>
    
                                <tr>
                                    <td style="width: 130px; height: 53px">
                                        <asp:Button ID="btnAceptarUFConsorcio" runat="server" Height="30px" OnClick="btnAceptarNuevoUF_Click" Text="Agregar" Width="90px" />
                                    </td>
                                    <td style="height: 53px" colspan="2">
                                        <asp:Button ID="btnCancelarDatos" runat="server" Height="30px" Text="Cancelar" Width="90px" OnClientClick="CerrarDivUFDatos(); return false;" />
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </td>

                    <td>&nbsp;</td>
                </tr>
                <br />
            </table>
            <table>
                <tr>
                    <td style="height: 59px">
                        <asp:label id="lblError" runat="server" font-size="Large" forecolor="#FF6600"></asp:label>
                    </td>
                </tr>
            </table>
        </div>
    </form>

</asp:Content>
