<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GastosFijos.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.ExpensaNueva.GastosFijos" %>
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
