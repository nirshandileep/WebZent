<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="UserLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="js/Validations.js">
function ClientValidate(source, arguments) 
{ 
alert(document.getElementById("txtUserName").value);
alert(document.getElementById("txtPassword").value);


    var txtUserName=document.getElementById("txtUserName");
    var txtPassword=document.getElementById("txtPassword");
    if ((txtUserName.value !="" ) || (txtPassword.value !="")) 
    { 
        arguments.IsValid = false; 
    }	
    alert("ValidateDualFields fired."); 
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td >
                    &nbsp;</td>
                <td >
                 
                    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Cannot leave User Name or Password fields empty" ClientValidationFunction="ClientValidate" ValidationGroup="vgOne" ControlToValidate="txtPassword" Display="Dynamic" ValidateEmptyText="True"></asp:CustomValidator>&nbsp;</td>
                <td >
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td width="572">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td >
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            </td>
                                        <td>
                                            <div class="login_content_bg">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td width="100">
                                                            User Name:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="text"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100">
                                                            &nbsp;</td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Passsword:</td>
                                                        <td>
                                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="text" TextMode="Password"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;</td>
                                                        <td>
                                                            <label>
                                                                </label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 24px">
                                                            &nbsp;</td>
                                                        <td align="right" style="height: 24px">
                                                            &nbsp;<asp:Button ID="btnUserLogin" runat="server" OnClick="btnLogin_Click" ValidationGroup="vgOne" Text="Login" /></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            </td>
                                        <td>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="Logincopyright">

                                <script language="JavaScript" src="Js/Copyright.js" type="text/javascript"></script>

                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
