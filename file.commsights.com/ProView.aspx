<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ProView.aspx.cs" Inherits="ProView" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin: 0px; margin: auto; text-align: center;">
        <img width="50%" height="50%" src="Images/logo01.png" />
    </div>
    <hr />
    <div style="margin: 0px; margin: auto; text-align: center;">
        <%=this.HTML %>
    </div>
</asp:Content>
