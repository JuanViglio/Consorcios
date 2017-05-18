<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GastoEventual.aspx.cs" Inherits="WebSistemmas.Consorcios.GastoEventual" MasterPageFile ="~/Consorcios/MenuConsorcios.Master" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="ContentPlaceHolder1"> 

    <script type="text/javascript" src="../js/Expensas.js"></script>
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link href="../css/jquery-ui.css" rel="stylesheet" />

    <form id="form2" runat="server">
        <p style="color: #003399; font-size: large">
            <asp:ScriptManager ID="scrActualizarGastos" runat="server">
            </asp:ScriptManager>
        </p>
        <p style="color: #003399; font-size: large">
            &nbsp;</p>
        <p style="color: #003399; font-size: large">
            Gasto Eventual</p>
        <p style="color: #003399; font-size: large">
            &nbsp;&nbsp;</p>
       
            <div>
                <table>
                    <tr>                            
                        <td style="width: 530px; height: 226px;" valign="top">
                            <div id="divExpensaNueva" style="margin-top: 17px;  ">
                                <table style="margin-top: 0px; width: 500px;">
                                    <tr>
                                        <td style="width: 100px">
                                            <asp:Label ID="Label2" runat="server" Text="Detalle"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDetalle" runat="server" style="margin-left: 0px" Width="379px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px; height: 23px;">
                                            <asp:Label ID="Label4" runat="server" Text="Importe"></asp:Label>
                                        </td>
                                        <td style="height: 23px">
                                            <asp:TextBox ID="txtImporte" runat="server" Width="380px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table>
                                    <tr>
                                        <td style="width: 100px; height: 51px">
                                            <asp:Button ID="btnAgregarGastoEventual" runat="server" Height="30px" OnClick="btnAgregarGastoEventual_Click" Text="Agregar" Width="90px" />
                                        </td>
                                        <td style="height: 51px; width: 100px;">
                                            <asp:Button ID="btnCancelarGastoEventual" runat="server" Height="30px" OnClick="btnCancelarGastoEventual_Click" Text="Cancelar" Width="90px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

         </form>
</asp:Content>
