<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GastosParticulares.aspx.cs" Inherits="WebSistemmas.Consorcios.GastosParticulares" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server">
        <p>
            &nbsp;</p>
        <p>
            <asp:gridview id="grdUnidades" runat="server" autogeneratecolumns="False" cellpadding="4" forecolor="#333333" gridlines="None" height="150px" onrowcommand="grdUnidades_RowCommand" style="margin-top: 0px; margin-left: 0px; margin-bottom: 24px;" width="622px" EnableModelValidation="True" OnRowDataBound="grdUnidades_RowDataBound">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                <asp:BoundField DataField="ID" HeaderText="Numero">
                <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                </asp:BoundField>
                <asp:BoundField DataField="Apellido" HeaderText="Apellido">
                <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="PagoId" HeaderText="PagoId">
                <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Aplicar">  
                    <HeaderTemplate>
                        Aplicar
                    </HeaderTemplate>
                    <ItemTemplate>  
                        <asp:CheckBox runat="server" ID="chkAplicar" 
                                AutoPostback="true" OnCheckedChanged="chkAplicar_CheckedChanged" 
                                Checked='<%# Eval("Aplicar")  %>' 
                                />  
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
        </p>
        <table>
            <tr>
                <td style="width: 115px; height: 40px;">
                    <asp:Label ID="Label2" runat="server" Text="Tipo de Gasto"></asp:Label>
                </td>
                <td style="width: 320px; height: 40px;">
                    <asp:DropDownList ID="DropDownList1" runat="server" Height="24px" Width="180px">
                        <asp:ListItem>Eventual Ordinario</asp:ListItem>
                        <asp:ListItem>Eventual Extraordinario</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 115px; height: 40px;">
                    <asp:Label ID="Label1" runat="server" Text="Detalle"></asp:Label>
                </td>
                <td style="height: 40px; width: 320px;">
                    <asp:TextBox ID="txtDetalle" runat="server" Width="300px" Height="20px" style="margin-right: 14px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 115px; height: 50px;">
                    <asp:Label ID="Label4" runat="server" Text="Importe Total"></asp:Label>
                </td>
                <td style="height: 50px; width: 320px;">
                    <asp:TextBox ID="txtImporte" runat="server" Width="170px" BorderStyle="Solid" Height="20px" style="margin-bottom: 0px"></asp:TextBox>
                    <asp:Button ID="btnActualizar" runat="server" Height="31px" OnClick="btnActualizar_Click" style="margin-top: 0px; margin-left: 18px;" Text="Actualizar" Width="112px" UseSubmitBehavior="False" />
                </td>
            </tr>
            <tr>
                <td style="height: 40px; width: 115px;">
                    <asp:Label ID="Label3" runat="server" Text="Importe por UF $"></asp:Label>
                </td>
                <td style="height: 40px; width: 320px;">
                    <asp:Label ID="lblImportePorUF" runat="server" Text="0"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnGuardarGasto" runat="server" Height="31px" OnClick="btnGuardarGasto_Click" style="margin-top: 19px; margin-right: 34px;" Text="Guardar Gasto" Width="112px" UseSubmitBehavior="False" />
                </td>
                <td>
                    <asp:Button ID="btnAplicarCocheras" runat="server" Height="31px" OnClick="btnAplicarCocheras_Click" style="margin-top: 20px; margin-right: 34px;" Text="UF con Cochera" Width="112px" UseSubmitBehavior="False" />
                </td>
                <td>
                    <asp:Button ID="btnVolver" runat="server" Height="31px" OnClick="btnVolver_Click" style="margin-top: 20px" Text="Volver" Width="112px" UseSubmitBehavior="False" />
                </td>
            </tr>
        </table>    
        <table>
            <tr>
                <td style="height: 73px">

                    <asp:Label ID="lblError" runat="server" Font-Size="Large" ForeColor="Red"></asp:Label>

                </td>
            </tr>
        </table>

    </form>


</asp:Content>
