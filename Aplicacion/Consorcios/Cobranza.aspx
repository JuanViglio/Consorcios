<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cobranza.aspx.cs" Inherits="WebSistemmas.Consorcios.Cobranza"  MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>

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

    <div id="accordion" style="width: 662px; height: 380px;">                    
        <h3>Buscar por UF</h3>
        <div style="height: 260px">
            <table style="height: 236px">
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
                        <asp:DropDownList ID="ddlUF" runat="server" Height="20px" Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
            </table>
        </div>

        <h3>Buscar por Propietario</h3>
        <div style="height: 255px;">

            <asp:updatepanel id="UpdatePanel5" runat="server">
            <ContentTemplate>   
                 
            <table>
                <tr>
                    <td style="width: 93px; height: 45px">Propietario</td>
                    <td style="height: 45px; width: 431px;">
                        <asp:DropDownList ID="ddlPropietario" runat="server" Width="250px" OnSelectedIndexChanged="ddlPropietario_SelectedIndexChanged" autopostback="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="grdUF" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="51px" style="margin-top: 0px; margin-left: 0px;" Width="574px">
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
                    <td colspan="2" style="height: 66px">

                        <asp:Button ID="btnAceptarPropietario" runat="server" Height="29px" OnClick="btnAceptarPropietario_Click" Text="Aceptar" Width="86px" />

                    </td>
                </tr>
            </table>

            </ContentTemplate>
            </asp:updatepanel>  

        </div>
    </div>
  
    <asp:updatepanel id="UpdatePanel1" runat="server">
    <ContentTemplate> 
    <asp:GridView ID="grdPagar" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="51px" style="margin-top: 0px; margin-left: 0px;" Width="574px">
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
    </Columns>
    <EditRowStyle BackColor="#999999" />
    <FooterStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#2166a9" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>        
    </ContentTemplate>
    </asp:updatepanel> 
 

    </form>
</asp:Content>