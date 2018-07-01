<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExpensaNueva.aspx.cs" Inherits="WebSistemmas.Consorcios.ExpensaNueva" MasterPageFile="~/Consorcios/MenuConsorcios.Master" %>
<%@ Register src="~/Consorcios/UserControls/Error.ascx" tagname="UserControl2" tagprefix="uc6" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.2.3/jquery-confirm.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.2.3/jquery-confirm.min.js"></script>
    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script type="text/javascript" src="../js/Confirm.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />

    <form id="form2" runat="server">

        <p style="color: #003399; font-size: large">
            <asp:scriptmanager id="Scriptmanager1" runat="server">
            </asp:scriptmanager>
        </p>
        <p style="color: #003399; font-size: large">
            &nbsp;
        </p>
        <p style="color: #003399; font-size: large; height: 39px;">
            <asp:Label ID="lblTitulo" runat="server" Text="Nueva Expensa"></asp:Label>
        </p>

        <asp:updatepanel id="UpdatePanel5" runat="server">
            <ContentTemplate>
                <uc6:UserControl2 ID="UserControl2ID" runat="server" />
            </ContentTemplate>
        </asp:updatepanel>        
        
        <div id="accordion">
            <h3>Ingreso de Gastos Fijos</h3>
            <div style="height: 159px">
                <asp:updatepanel id="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <table style="height: 149px; width: 1179px">
                        <tr>
                            <td style="width: 643px; height: 156px;" valign="top">
                                <asp:GridView ID="grdGastosOrdinarios" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="140px" OnRowCommand="grdGastosOrdinarios_RowCommand" OnRowDataBound="grdGastosOrdinarios_RowDataBound" style="margin-top: 0px; margin-left: 0px;" Width="622px">
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
                                        <asp:BoundField DataField="GastoID" HeaderText="GastoID">
                                        <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Sumar">  
                                            <HeaderTemplate>
                                                Sumar
                                            </HeaderTemplate>
                                            <ItemTemplate>  
                                                <asp:CheckBox runat="server" ID="chkSumar" 
                                                     AutoPostback="true" OnCheckedChanged="chkSumar_CheckedChanged" 
                                                     Checked='<%# Eval("Sumar")  %>' 
                                                     />  
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
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <div class="div_parent">
                                                    <asp:ImageButton ID="Eliminar" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Eliminar" />
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
                                </asp:GridView>
                            </td>
                            <td style="width: 530px; height: 156px;" valign="top">

                                    <div id="divExpensaNueva" style="margin-top: 7px;  ">
                                        <table style="margin-top: 0px; width: 500px; height: 29px;">
                                            <tr>
                                                <td style="width: 99px">
                                                    <asp:RadioButton ID="btnGuardado" runat="server" AutoPostBack="True" Checked="True" GroupName="groupGastosOrdinarios" OnCheckedChanged="btnGuardado_CheckedChanged" Text="Guardado" />
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="btnNuevo" runat="server" AutoPostBack="True" GroupName="groupGastosOrdinarios" OnCheckedChanged="btnNuevo_CheckedChanged" Text="Nuevo" />
                                                    &nbsp;</td>
                                            </tr>
                                        </table>

                                        <div id="divGastoOrdnarioGuardado" runat ="server">
                                        <table style="margin-top: 0px; width: 500px;">
                                            <tr>
                                                <td style="width: 77px; height: 23px;">Gasto&nbsp;&nbsp; </td>
                                                <td style="height: 23px">
                                                    <asp:DropDownList autopostback="true" ID="ddlGastos" runat="server" Height="23px" Width="380px" OnSelectedIndexChanged="ddlGastos_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>                               
                                            </tr>
                                            <tr>
                                                <td style="height: 22px">Detalle</td><td style="height: 22px">
                                                    <asp:TextBox ID="txtDetalle" runat="server" Width="379px" Height="20px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>  
                                        </div>

                                        <div id="divGastoOrdnarioNuevo" runat ="server">
                                        <table>
                                            <tr>
                                                <td style="width: 78px">
                                                    <asp:Label ID="Label2" runat="server" Text="Gasto"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGasto" runat="server" style="margin-left: 0px" Width="379px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        </div>

                                        <table>
                                            <tr>
                                                <td style="width: 77px; height: 23px;">
                                                    <asp:Label ID="Label4" runat="server" Text="Importe"></asp:Label>
                                                </td>
                                                <td style="height: 23px">
                                                    <asp:TextBox ID="txtImporte" runat="server" Width="149px" BorderStyle="Solid"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table>
                                            <tr>
                                                <td style="width: 100px; height: 51px">
                                                    <asp:Button ID="btnAgregarGastoOrdinario" runat="server" Height="30px" OnClick="btnAgregarGastoOrdinario_Click" Text="Agregar" Width="90px" />
                                                </td>
                                                <td style="height: 51px; width: 100px;">
                                                    <asp:Button ID="btnCancelarGastoOrdinario" runat="server" Height="30px" OnClick="btnCancelarGastoOrdinario_Click" Text="Cancelar" Width="90px" />
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </div>

                        </td>
                        </tr>                        
                    </table>
                    </ContentTemplate>
                    </asp:updatepanel>
            </div>


            <h3>Ingreso de Gastos Eventuales Ordinarios</h3>
            <asp:updatepanel id="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div>
                 <table>
        <tr>
            <td style="width: 643px; height: 153px;">
                <asp:GridView ID="grdGastosEventuales" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="140px" style="margin-top: 0px; margin-left: 0px;" Width="622px" OnRowCommand="grdGastosEventuales_RowCommand" OnRowDataBound="grdGastosEventuales_RowDataBound">
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
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="div_parent">
                                    <asp:ImageButton ID="EliminarGastoEvOrdinario" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Modificar" />
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
                </asp:GridView>
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
                <br />
            </td>                
            <td valign="top" style="width: 530px; height: 153px;">
                <div id="div1" style="margin-top: 17px;  " >
                    <table style="margin-top: 0px; width: 500px;">
                        <tr>
                            <td style="width: 207px">
                                <asp:Label ID="Label6" runat="server" Text="Detalle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDetalleEvOrd" runat="server" Width="380px" style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 207px">
                                <asp:Label ID="Label13" runat="server" Text="Proveedor"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProveedoresEvOrd" runat="server" autopostback="true" Height="23px" Width="380px" OnSelectedIndexChanged="ddlProveedores_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 207px; height: 23px;">
                                <asp:Label ID="Label14" runat="server" Text="Importe Compra"></asp:Label>
                            </td>
                            <td style="height: 23px">
                                <asp:TextBox ID="txtImporteCompraEvOrd" runat="server" Width="140px" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 207px; height: 23px;">
                                <asp:Label ID="Label7" runat="server" Text="Importe Venta"></asp:Label>
                            </td>
                            <td style="height: 23px">
                                <asp:TextBox ID="txtImporteEvOrd" runat="server" Width="140px" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 207px; height: 53px">
                                <asp:Button ID="btnAgregarGastoEvOrd" runat="server" Height="30px" OnClick="btnAgregarGastoEvOrd_Click" Text="Agregar" Width="90px" />
                            </td>
                            <td style="height: 53px">
                                <asp:Button ID="btnCancelarGastoEvOrdinario" runat="server" Height="30px" OnClick="btnCancelarGastoEvOrdinario_Click" Text="Cancelar" Width="90px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>

        </tr>
    </table>           
                </div>
            </ContentTemplate>
            </asp:updatepanel>

            <h3>Ingreso de Gastos Eventuales Extraordinarios</h3>
            <asp:updatepanel id="UpdatePanel3" runat="server">
            <ContentTemplate>
                <div>        
                    <table>
        <tr>
            <td style="width: 643px; height: 109px;">
                <asp:GridView ID="grdGastosExtraordinarios" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="141px" style="margin-top: 0px; margin-left: 0px;" Width="622px" OnRowCommand="grdGastosExtraordinarios_RowCommand" OnRowDataBound="grdGastosExtraordinarios_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Detalle" HeaderText="Detalle">
                        <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ImporteCompra" HeaderText="Importe Compra">
                        <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Importe" HeaderText="Importe Venta">
                        <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ID" HeaderText="ID">
                        <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="div_parent">
                                    <asp:ImageButton ID="ImageButton4" runat="server" CausesValidation="False" CommandName="Modificar" ImageUrl="~/css/img/ico_modificar.gif" ToolTip="Modificar" />
                                </div>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div class="div_parent">
                                    <asp:ImageButton ID="EliminarGastoEvExtraordinario" runat="server" CausesValidation="False" CommandName="Eliminar" ImageUrl="~/css/img/ico_eliminar.png" ToolTip="Modificar" />
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
                </asp:GridView>
                <br />
            </td>
            <td valign="top" style="width: 530px; height: 109px;">
                <div id="div2" style="margin-top: 17px;  " >
                    <table style="margin-top: 0px; width: 500px;">
                        <tr>
                            <td style="width: 203px">
                                <asp:Label ID="Label3" runat="server" Text="Detalle"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDetalleGastoExtraordinario" runat="server" Width="380px" style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 203px">
                                <asp:Label ID="Label11" runat="server" Text="Proveedor"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProveedoresEvExt" runat="server" autopostback="true" Height="23px" Width="380px" OnSelectedIndexChanged="ddlProveedores_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 203px">
                                <asp:Label ID="Label12" runat="server" Text="Importe Compra"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtImporteCompraGastoExt" runat="server" Width="140px" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 203px; height: 23px;">
                                <asp:Label ID="Label9" runat="server" Text="Importe Venta"></asp:Label>
                            </td>
                            <td style="height: 23px">
                                <asp:TextBox ID="txtImporteGastoExtraordinario" runat="server" Width="140px" BorderStyle="Solid"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 203px; height: 53px">
                                <asp:Button ID="btnAgregarGastoExt" runat="server" Height="30px" OnClick="btnAgregarGastoExt_Click" Text="Agregar" Width="90px" />
                            </td>
                            <td style="height: 53px">
                                <asp:Button ID="btnCancelarGastoEvExt" runat="server" Height="30px" OnClick="btnCancelarGastoEvExt_Click" Text="Cancelar" Width="90px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>

        </tr>
    </table>           
                </div>
            </ContentTemplate>
            </asp:updatepanel>
        </div>

        <asp:updatepanel id="UpdatePanel4" runat="server">
        <ContentTemplate>

        <table style="width: 879px">
            <tr>
                <td style="height: 45px; text-align: left; width: 168px;">
                    <asp:Label ID="Label5" runat="server" Text="Subtotal Ord.:" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td style="width: 28px; height: 45px;" align="right">
                    <asp:Label ID="lblTotalGastosOrdinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td style="width: 157px; height: 45px;">
                    <asp:Button runat="server" Text="Volver" ID="btnVolver" Height="30px" OnClick="btnVolver_Click" Width="120px" style="margin-left: 96px"></asp:Button>
                </td>
                <td style="width: 295px; height: 45px;">
                    <asp:Button runat="server" Text="Aceptar Expensa" ID="btnAceptar" Height="30px" OnClientClick = "Confirm('¿Esta seguro que quiere ACEPTAR las Expensas?')" OnClick="btnAceptar_Click" Width="120px"></asp:Button>
                </td>
            </tr>
            <tr>
                <td style="width: 168px; height: 40px;">
                    <asp:Label ID="Label1" runat="server" Text="Subtotal Extraord." Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td style="height: 40px; width: 28px;" align="right">
                    <asp:Label ID="lblTotalGastosExtraordinarios" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td colspan ="2" style="height: 40px"></td>
            </tr>
            <tr>
                <td style="width: 168px; height: 38px;">
                    <asp:Label ID="Label10" runat="server" Text="Total Gastos:" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td  style="height: 38px; width: 28px;" align="right">
                    <asp:Label ID="lblTotalGastos" runat="server" Font-Size="Large" style="color: #003399"></asp:Label>
                </td>
                <td colspan ="2"></td>
            </tr>
        </table>

        </ContentTemplate>
        </asp:updatepanel>
    </form>
</asp:Content>

