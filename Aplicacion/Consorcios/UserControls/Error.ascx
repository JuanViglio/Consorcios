<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Error.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Error" %>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

<style type="text/css">
    .auto-style1 {
        height: 63px;
        margin-top: 20px;
    }
    .auto-style2 {
        width: 30px;
        height: 23px;
        margin-right: 10px;
    }
    </style>

<div id="divError" runat="server" style="color: #003399; font-size: large; " align="left" class="auto-style1">
    <div style ="float:left">
        <img src="../../css/img/Cruz-roja.jpg" class="auto-style2" />

    </div>
    <div style ="float:left">
        <asp:label id="lblError" runat="server" forecolor="Red" style="text-align: left; font-size: large;"></asp:label>
    </div>
</div>

<script>
    $(document).ready(function () {
        $('#ctl00_ContentPlaceHolder1_UserControl2ID_divError').live('click', function (e) {
            $("#ctl00_ContentPlaceHolder1_UserControl2ID_divError").slideUp();
        });
    });
</script>