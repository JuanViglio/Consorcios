<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeguroNuevo.aspx.cs" Inherits="WebSistemmas.Consorcios.SeguroNuevo" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>
<%@ Register TagPrefix="ucGridSeguroDetalle" TagName="gridSeguroDetalle" Src="~/Consorcios/UserControls/Seguros/GridSeguroDetalle.ascx" %>
<%@ Register src="~/Consorcios/UserControls/Error.ascx" tagname="tagError" tagprefix="ucError" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script src="../js/Proveedores.js"></script>

    <form id="form1" runat="server">
        <style>
            .botonSeguro{
                height: 38px;
                width: 120px;
            }
            .trSeguroNuevo {
                height: 40px;
            }
            .textBox {
                width: 209px;
                height:20px
            }
            .tdSeguroNuevo {
                width: 120px;
            }
            .auto-style1 {
                width: 165px;
                height: 78px;
            }
            .tdDerecha {
                width: 89px; 
                text-align: right;
            }
            .margenTdDerecho {
                margin-left: 20px;
            }
            .auto-style2 {
                width: 262px;
            }
            .auto-style3 {
                margin-left: 0px;
            }
            .auto-style5 {
                width: 134px;
                text-align: right;
            }
            .auto-style7 {
                margin-left: 0px;
            }
        </style>

        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
        <ucError:tagError ID="UserControlError" runat="server" />

        <table>
            <tr>
                <td class="auto-style6">
                    <table>
                        <tr class="trSeguroNuevo">
                            <td class="tdSeguroNuevo">Compañia
                            </td>
                            <td>
                                <asp:textbox id="txtCompañia" runat="server" class="textBox"></asp:textbox>
                            </td>
                        </tr>
                        <tr class="trSeguroNuevo">
                            <td class="tdSeguroNuevo">Poliza
                            </td>
                            <td>
                                <asp:textbox id="txtPoliza" runat="server" width="208px" class="textBox"></asp:textbox>
                            </td>
                            <td class="auto-style5">Tipo&nbsp;&nbsp;&nbsp;&nbsp; </td>
                            <td class="auto-style2">
                                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="auto-style7" Height="30px">
                                    <asp:ListItem>AC</asp:ListItem>
                                    <asp:ListItem>IC</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSeguroNuevo">Fecha Inicio
                            </td>
                            <td>
                                <asp:calendar id="dteFechaInicio" runat="server" backcolor="White" bordercolor="#3366CC" borderwidth="1px" cellpadding="1" daynameformat="Shortest" font-names="Verdana" font-size="8pt" forecolor="#003399" height="180px" width="205px">
                                <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                <WeekendDayStyle BackColor="#CCCCFF" />
                            </asp:calendar>
                            </td>

                            <td class="auto-style5">Fecha Fin&nbsp;&nbsp;&nbsp;
                            </td>
                            <td class="auto-style2">
                                <asp:calendar id="dteFechaFin" runat="server" backcolor="White" bordercolor="#3366CC" borderwidth="1px" cellpadding="1" daynameformat="Shortest" font-names="Verdana" font-size="8pt" forecolor="#003399" height="180px" width="205px">
                                <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                <WeekendDayStyle BackColor="#CCCCFF" />
                            </asp:calendar>
                            </td>
                        </tr>
                        <tr class="trSeguroNuevo">
                            <td class="tdSeguroNuevo">Consorcio
                            </td>
                            <td>
                                <asp:dropdownlist id="ddlConsorcios" runat="server" class="textBox">
                            </asp:dropdownlist>
                            </td>
                        </tr>
                        <tr class="trSeguroNuevo">
                            <td class="tdSeguroNuevo">Cantidad Cuotas
                            </td>
                            <td>
                                <asp:textbox id="txtCantCuotas" runat="server" class="textBox"></asp:textbox>
                            </td>
                            <td class="auto-style5">Cuotas de gracia&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td class="auto-style2">
                                <asp:textbox id="txtCuotasDeGracia" runat="server" class="textBox"></asp:textbox>
                            </td>
                        </tr>
                        <tr class="trSeguroNuevo">
                            <td class="tdSeguroNuevo">Importe de Cuota
                            </td>
                            <td>
                                <asp:textbox id="txtImporte" runat="server" class="textBox"></asp:textbox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <ucgridsegurodetalle:gridSeguroDetalle ID="gridSeguroDetalleID" runat="server" />
                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td class="auto-style1">
                    <asp:button runat="server" text="Generar Cuotas" ID="btnGenerar" OnClick="btnGenerar_Click" class="botonSeguro" />
                </td>                
                <td class="auto-style1">
                    <asp:button runat="server" text="Cancelar" ID="btnVolver" OnClick="btnVolver_Click" class="botonSeguro" />
                </td>
                <td>
                    <div id="divGuardarSeguro" style="display: none">
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" class="botonSeguro"/>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</asp:Content>
