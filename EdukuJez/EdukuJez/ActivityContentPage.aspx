<%@ Page Title="Activity Content" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ActivityContentPage.aspx.cs" Inherits="EdukuJez.ActivityContentPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div style="width:100%">
    <asp:Button ID="GoBackButton" runat="server" Text="Powrót" Style="margin-top: 12px; width: 170px; height: 60px; font-size: 20px; float: left; white-space: normal;" OnClick="GoBackButton_Click" CssClass="Main-Panel-Image" ForeColor="Black" EnableTheming="True"/>
   </div>
    <br>
    <br>
    <div style="width:100%">
    <asp:Label ID="MainActLabel" runat="server" Text=""  Font-Size="32px"></asp:Label>
    <br>
    <br>
    <br>
    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <asp:Label ID="Label1" runat="server" Text=""  Font-Size="24px"></asp:Label>
    <br>
    <br>
    <asp:Label ID="LabelSubDesc" runat="server" Text=""  Font-Size="24px"></asp:Label>
        <br>
    <br>
    <br>
    <br>
        <asp:Label ID="Label2" runat="server" Text="Przesyłanie pracy:"  Font-Size="24px"></asp:Label>
        <br>
    <br>
        <asp:FileUpload ID="FileUpload1" runat="server" /><br><br>
        <asp:Button ID="Button1" runat="server" Text="Zatwierdź" OnClick="Button1_Click" />

        </asp:Panel>

    <asp:Panel ID="Panel2" runat="server" Visible="False">
       
    </asp:Panel>
    </div>



</asp:Content>