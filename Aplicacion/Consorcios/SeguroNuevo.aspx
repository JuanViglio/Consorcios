﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SeguroNuevo.aspx.cs" Inherits="WebSistemmas.Consorcios.SeguroNuevo" MasterPageFile="~/Consorcios/MenuConsorcios.Master"%>
<%@ Register TagPrefix="uc1" TagName="tituloPagina" Src="~/Consorcios/UserControls/Titulo.ascx" %>



<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script src="../js/Proveedores.js"></script>

    <form id="form1" runat="server">
        <style>
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
        </style>

        <uc1:tituloPagina ID="tituloPaginaID" runat="server" />
        
        <table>
            <tr>
                <td>
                    <table>
            <tr class="trSeguroNuevo">
                <td class="tdSeguroNuevo">Compañia
                </td>
                <td>
                    <asp:textbox id="TextBox1" runat="server" class="textBox"></asp:textbox>
                </td>
            </tr>
            <tr class="trSeguroNuevo">
                <td class="tdSeguroNuevo">Poliza
                </td>
                <td>
                    <asp:textbox id="TextBox2" runat="server" width="208px" class="textBox"></asp:textbox>
                </td>
            </tr>
            <tr>
                <td class="tdSeguroNuevo">Fecha Inicio
                </td>
                <td>
                    <asp:calendar id="Calendar1" runat="server" backcolor="White" bordercolor="#3366CC" borderwidth="1px" cellpadding="1" daynameformat="Shortest" font-names="Verdana" font-size="8pt" forecolor="#003399" height="180px" width="205px">
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

                <td style="width: 89px; text-align: right">Fecha Fin
                </td>
                <td style="width: 250px">
                    <asp:calendar id="Calendar2" runat="server" style="margin-left: 23px" backcolor="White" bordercolor="#3366CC" borderwidth="1px" cellpadding="1" daynameformat="Shortest" font-names="Verdana" font-size="8pt" forecolor="#003399" height="180px" width="205px">
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
                    <asp:dropdownlist id="DropDownList1" runat="server" class="textBox">
                </asp:dropdownlist>
                </td>
            </tr>
            <tr class="trSeguroNuevo">
                <td class="tdSeguroNuevo">Cantidad Cuotas
                </td>
                <td>
                    <asp:textbox id="TextBox3" runat="server" class="textBox"></asp:textbox>
                </td>
            </tr>
            <tr class="trSeguroNuevo">
                <td class="tdSeguroNuevo">Cuotas de gracia
                </td>
                <td>
                    <asp:textbox id="TextBox4" runat="server" class="textBox"></asp:textbox>
                </td>
            </tr>
            <tr class="trSeguroNuevo">
                <td class="tdSeguroNuevo">Importe de Cuota
                </td>
                <td>
                    <asp:textbox id="TextBox5" runat="server" class="textBox"></asp:textbox>
                </td>
            </tr>
        </table>
                </td>
                <td>
                    tabla de cuotas
                </td>
            </tr>
        </table>

        <table>
            <tr>
                <td class="auto-style1">
                    <asp:button runat="server" text="Generar Cuotas" ID="btnGenerar" Height="38px" OnClick="btnGenerar_Click" style="margin-left: 0px" Width="120px" />
                </td>                
                <td class="auto-style1">
                    <asp:button runat="server" text="Cancelar" ID="btnVolver" Height="38px" OnClick="btnVolver_Click" style="margin-left: 0px" Width="120px" />
                </td>
            </tr>
        </table>
    </form>
</asp:Content>
