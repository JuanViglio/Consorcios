﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnidadesFuncionales.aspx.cs" Inherits="WebSistemmas.Consorcios.UnidadesFuncionales" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>

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
                    <td class="auto-style6" style="height: 46px; width: 210px">
                        UF / Depto /
                        Nombre / Apellido</td>
                    <td class="auto-style5" style="height: 46px; width: 302px">

                        <asp:TextBox ID="txtFiltro" runat="server" Width="284px"></asp:TextBox>

                    </td>
                    <td class="auto-style4" style="height: 46px; width: 98px">
                        <asp:Button ID="btnBuscar" runat="server" Height="32px" OnClick="btnBuscar_Click" Text="Buscar" Width="88px" CssClass="auto-style7" />
                    </td>
                    <td style="height: 46px">
                        <asp:Button ID="btnLimpiar" runat="server" Height="32px" OnClick="btnLimpiar_Click" Text="Limpiar" Width="88px" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td colspan="2" style="height: 131px">
                        <asp:gridview id="grdUnidades" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowcommand="grdUnidades_RowCommand" style="margin-top: 0px; margin-left: 0px; margin-right: 32px;" width="698px" OnRowDataBound="grdUnidades_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="UF" HeaderText="UF">
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
                    <asp:BoundField DataField="Cochera" HeaderText="Cochera">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Coeficiente" HeaderText="Coeficiente">
                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ID" HeaderText="ID">
                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Dueños_ID" HeaderText="Dueños_ID">
                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="div_parent">
                                <asp:ImageButton ID="Modificar" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
                                <asp:ImageButton ID="CtaCte" runat="server" CausesValidation="False" CommandName="CtaCte" ImageUrl="~/css/img/ico_pesos.png" ToolTip="Cuenta Cte" />
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
                    </td>
                    <td valign="top" style="width: 525px; height: 131px;">
                        <div id="divUFModificar" style="margin-top: 30px; display: none">
                            <table style="margin-top: 47px; width: 375px;">
                                <tr>
                                    <td style="width: 130px">
                                        <asp:label id="Label2" runat="server" text="ID"></asp:label>
                                    </td>
                                    <td colspan="2">
                                        <asp:textbox id="txtID" runat="server" width="155px" style="margin-left: 0px"></asp:textbox>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td style="width: 130px">
                                        <asp:label id="Label5" runat="server" text="UF"></asp:label>
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
                                        <asp:label id="Label1" runat="server" text="Coeficiente"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:textbox id="txtCoeficiente" runat="server" width="156px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 23px;">
                                        <asp:label id="Label12" runat="server" text="Cochera"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:dropdownlist  runat="server" ID="ddlCochera">
                                            <asp:ListItem>NO</asp:ListItem>
                                            <asp:ListItem>SI</asp:ListItem>
                                        </asp:dropdownlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:label id="Label3" runat="server" text="Dueño"></asp:label>
                                    </td>
                                    <td>
                                        <asp:dropdownlist  runat="server" ID="ddlDueños">
                                        </asp:dropdownlist>                                        
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
                        <asp:Button ID="btnVolver" runat="server" Height="35px" Text="Volver" Width="107px" OnClick="btnVolver_Click" style="margin-left: 26px; margin-right: 18px;" />
                    </td>
                    <td style="width: 442px">
                        <div id="divUFDatos" style="margin-top: 30px; display: none">
                            <table style="margin-top: 47px; width: 375px;">
                                <tr>
                                    <td style="width: 130px">
                                        <asp:label id="Label6" runat="server" text="UF"></asp:label>
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
                                        <asp:label id="Label8" runat="server" text="Coeficiente"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:textbox id="txtCoeficienteNuevo" runat="server" width="156px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 130px; height: 23px;">
                                        <asp:label id="Label13" runat="server" text="Cochera"></asp:label>
                                    </td>
                                    <td style="height: 23px" colspan="2">
                                        <asp:dropdownlist  runat="server" ID="ddlCocheraNueva">
                                            <asp:ListItem>NO</asp:ListItem>
                                            <asp:ListItem>SI</asp:ListItem>
                                        </asp:dropdownlist>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:label id="Label14" runat="server" text="Dueño"></asp:label>
                                    </td>
                                    <td>
                                        <asp:dropdownlist  runat="server" ID="ddlDueñosNuevo">
                                        </asp:dropdownlist>                                        
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
