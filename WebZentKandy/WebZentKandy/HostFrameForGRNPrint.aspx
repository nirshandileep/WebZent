<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HostFrameForGRNPrint.aspx.cs" Inherits="HostFrameForGRNPrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<HEAD>
  <title>GRN Print</title>
  <META NAME="Print GRN" Content="Microsoft Visual Studio 7.0" />
  
 </HEAD>
 <frameset rows="0,*" >

  <frame name="header" src="" scrolling="no" noresize>
  <frame name="main" src='<%=src%>'>
 </frameset>
</html>
