<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>VIP Manger BS System Login</title>
    <link rel="stylesheet" href="cssFiles/LoginPage.css" type="text/css" />
    
</head>
<body>
    <form runat="server" id="form1">
    <div class="login">
        <div>
            <asp:TextBox runat="server" ID="txtName" class="iputuser"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                ErrorMessage="*" SetFocusOnError="True" Font-Size="Small">*</asp:RequiredFieldValidator>
            <asp:TextBox runat="server" ID="txtPWD" TextMode="Password" class="inputpass"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPWD"
                ErrorMessage="*" Font-Size="Small">*</asp:RequiredFieldValidator>
            <asp:ImageButton runat="server" ID="btnLogin" ImageUrl="images/c.gif" Style="cursor: pointer"
                class="subpic" OnClick="btnLogin_Click" Height="20px" />

        </div>
    </div>
    </form> 
</body>
</html>
