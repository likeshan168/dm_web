<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="alterPassword.aspx.cs" Inherits="alterPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">
    <p>
        -=修改密码=-
    </p>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
    <asp:Label ID="lblPrimitivePWD" runat="server" Text="请输入原始密码："></asp:Label>
    <asp:TextBox ID="txtPrimitivePWD" runat="server" TextMode="Password"></asp:TextBox>
            <asp:Button ID="btnOK" runat="server" Height="22px" onclick="btnOK_Click" 
                Text="验证" Width="35px" />
            <asp:Label ID="lblText" runat="server" Text="Label"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvIsNull" runat="server" 
        ControlToValidate="txtPrimitivePWD" ErrorMessage="请输入原始密码。"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblNewPWD" runat="server" Text="请输入新的密码："></asp:Label>
            <asp:TextBox ID="txtNewPWD" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvIsNullNewPWD" runat="server" 
        ControlToValidate="txtNewPWD" ErrorMessage="请输入新密码。"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="lblTrueNewPWD" runat="server" Text="请确认新的密码："></asp:Label>
            <asp:TextBox ID="txtTrueNewPWD" runat="server" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="cvdCompare" runat="server" 
        ControlToCompare="txtTrueNewPWD" ControlToValidate="txtNewPWD" 
        ErrorMessage="两次输入不一致。"></asp:CompareValidator>
            <br />
            <br />
            <br />
               <asp:Button ID="btnSubmit" runat="server" Text="保 存" 
        onclick="btnSubmit_Click" />
    </ContentTemplate>
    </asp:UpdatePanel>
         
      
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>

