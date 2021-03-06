﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Error.ascx.cs" Inherits="WebSistemmas.Consorcios.UserControls.Error" %>

<style type="text/css">
    .divError {
        margin-bottom: 20px;
        width: 580px;
    }
    .cruzRoja {
         height: 24px;
    }

    .auto-style1 {
        margin-bottom: 20px;
        width: 880px;
    }
    .auto-style2 {
        width: 749px;
    }

</style>

<div id="divError" runat="server" style="color: #003399; font-size: large; " align="left" class="auto-style1" >
    <div style="float:left;padding-right: 10px;">
        <img src="../../css/img/Cruz-roja.jpg" class="cruzRoja" />
    </div>
    <div class="auto-style2">
        <asp:label id="lblError" runat="server" forecolor="Red" style="text-align: left; font-size: large; width: 500px"></asp:label>
    </div>
</div>

<script>
    $(document).ready(function () {
        $('#ctl00_ContentPlaceHolder1_UserControl2ID_divError').on( "click", function() {
            $("#ctl00_ContentPlaceHolder1_UserControl2ID_divError").slideUp();
        });
    });
</script>