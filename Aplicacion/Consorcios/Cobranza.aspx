<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cobranza.aspx.cs" Inherits="WebSistemmas.Consorcios.Cobranza"  MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register TagPrefix="uc3" TagName="gridPagar" Src="~/Consorcios/UserControls/Cobranza/GridPagar.ascx"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <form id="form1" runat="server" style="height: 814px">
    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <link href="../css/Paginas.css" rel="Stylesheet" />

    <asp:scriptmanager id="Scriptmanager1" runat="server">
    </asp:scriptmanager>

    <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td>
                            <div style="height: 489px">
                                <asp:RadioButton ID="UF" runat="server" AutoPostBack="True" Checked="True" GroupName="BuscarDeuda" OnCheckedChanged="UF_CheckedChanged" Text="Busqueda por UF" />
                                <br />
                                <asp:RadioButton ID="Propietario" runat="server" AutoPostBack="True" GroupName="BuscarDeuda" OnCheckedChanged="Propietario_CheckedChanged" Text="Busqueda por Propietario" />
                                <br /><br />

                                <div id="divUF" runat="server">  
                                    <table class="auto-style5">
                                        <tr>
                                            <td colspan="2" style="height: 20px"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px; height: 40px">Consorcio</td>
                                            <td style="height: 40px">
                                                <asp:DropDownList ID="ddlConsorcios" runat="server" Width="250px" OnSelectedIndexChanged="ddlConsorcios_SelectedIndexChanged" autopostback="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px; height: 40px">UF</td>
                                            <td style="height: 40px">
                                                <asp:DropDownList ID="ddlUF" runat="server" Height="20px" Width="250px" OnSelectedIndexChanged="ddlUF_SelectedIndexChanged" autopostback="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 93px; height: 40px">Periodo</td>
                                            <td style="height: 40px">
                                                <asp:DropDownList ID="ddlPeriodo" runat="server" Height="20px" Width="250px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                    </table>                      
                                </div>
                                <div id="divPropietario" runat="server" style="height: 253px">
                                <table>
                                    <tr>
                                        <td style="width: 93px; height: 38px">Propietario</td>
                                        <td style="height: 38px; width: 431px;">
                                            <asp:DropDownList ID="ddlPropietario" runat="server" Width="250px" OnSelectedIndexChanged="ddlPropietario_SelectedIndexChanged" autopostback="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                <asp:GridView ID="grdUF" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px" style="margin-top: 0px; margin-left: 0px;" Width="574px">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:BoundField DataField="Direccion" HeaderText="Direccion">
                                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                                    </asp:BoundField>                
                                    <asp:BoundField DataField="UF" HeaderText="UF">
                                    <ItemStyle Font-Bold="False" Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Names="Calibri" Font-Size="Large" ForeColor="#8888A5" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Sumar">  
                                        <HeaderTemplate>
                                            Sumar
                                        </HeaderTemplate>
                                        <ItemTemplate>  
                                            <asp:CheckBox runat="server" ID="chkSumar" />  
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
                                    </tr>
                                </table>        
                                <table>
                                    <tr>
                                        <td style="height: 66px">
                                            <asp:Button ID="btnAceptarPropietario" runat="server" Height="29px" OnClick="btnAceptarPropietario_Click" Text="Aceptar" Width="86px" />
                                        </td>
                                    </tr>
                                </table>
                                    <br />
                                </div>                    
                            </div>
                        </td>
                        <td>
                            <uc3:gridPagar ID="gridPagarID" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</asp:Content>