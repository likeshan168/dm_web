<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Main.aspx.cs" Inherits="Main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#span1").ajaxStart(function () {
                $(this).html("应用程序初始化中, 请稍候...").css("color", "red");
            });
            $.get("httpHandler/Main_Response.ashx", null, function (data) {
                if (data === "Finished") {
                    $("#span1").html("初始化完成.欢迎使用").css("color", "Gray");
                }
                else {
                    $("#span1").html(xmlHttp.responseText);
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <div style="position: fixed; z-index: 10; font-size: large; font-family: 微软雅黑; font-weight: bold;">
        <span id="span1" style="color: red">应用程序初始化中,请稍候</span></div>
    <div style="background-image: url('images/welcome.jpg'); background-repeat: no-repeat;
        width: 656px; height: 404px; position: fixed">
    </div>
</asp:Content>
