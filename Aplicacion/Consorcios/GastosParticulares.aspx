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
                <td style="width: 77px; height: 30px;">
                    <asp:Label ID="Label1" runat="server" Text="Detalle"></asp:Label>
                </td>
                <td style="height: 30px">
                    <asp:TextBox ID="txtDetalle" runat="server" Width="379px" Height="20px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 77px; height: 30px;">
                    <asp:Label ID="Label4" runat="server" Text="Importe"></asp:Label>
                </td>
                <td style="height: 30px">
                    <asp:TextBox ID="txtImporte" runat="server" Width="175px" BorderStyle="Solid"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnGuardarGasto" runat="server" Height="31px" OnClick="btnGuardarGasto_Click" style="margin-top: 19px; margin-right: 34px;" Text="Guardar Gasto" Width="112px" />
                </td>
                <td>
                    <asp:Button ID="btnAplicarCocheras" runat="server" Height="31px" OnClick="btnAplicarCocheras_Click" style="margin-top: 20px; margin-right: 34px;" Text="UF con Cochera" Width="112px" />
                </td>
                <td>
                    <asp:Button ID="btnVolver" runat="server" Height="31px" OnClick="btnVolver_Click" style="margin-top: 20px" Text="Volver" Width="112px" />
                </td>
            </tr>
        </table>    
    </form>


</asp:Content>
