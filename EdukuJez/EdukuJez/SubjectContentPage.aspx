<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SubjectContentPage.aspx.cs" Inherits="EdukuJez.SubjectContentPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header Container-Title ">
        <img src="Imgs/Subjects_Title.png" class="logo"/>
        <hr/>
    </div>
    <div class="Center-Form" style="flex-direction: column;">
        <asp:Label ID="SubjectNameLabel" runat="server" Text="SubjectName" CssClass="Subject-Label"></asp:Label>
        <asp:Panel ID="Panel1" runat="server" CssClass="Subject-Panel">
        <asp:Button ID="AttendancesButton" runat="server" Text="Obecności" Width="98px" Height="30px" OnClick="AttendancesButton_Click" Visible="True" />
        <asp:Button ID="GradesButton" runat="server" Text="Oceny" Width="98px" Height="30px" OnClick="GradesButton_Click" Visible="True" />
        </asp:Panel>
        <asp:Panel ID="AttachmentPanel" runat="server" CssClass="Subject-Panel">
            <asp:Label ID="AttachmentLabel" runat="server" Text="Materiały" CssClass="Subject-Label"></asp:Label>
            <asp:Table ID="AttachmentTable" runat="server"></asp:Table>
            <asp:Button ID="NewAttachmentButton" runat="server" Text="Dodaj materiał" OnClick="NewAttachmentButton_Click" Width="118px" Height="30px" Visible="False" />
            <asp:Button ID="DeactivateAttachmentButton" runat="server" Text="Deaktywuj materiał" Width="134px" Height="30px" OnClick="DeactivateAttachmentButton_Click" Visible="False" />
            <asp:Button ID="DelAttachmentButton" runat="server" Text="Usuń materiał" Width="105px" Height="30px" OnClick="DelAttachmentButton_Click" Visible="False" />
            <asp:DropDownList ID="AttachmentDropDownList" runat="server" Width="121px" Height="30px" Visible="False">
            </asp:DropDownList>
        </asp:Panel>
        <hr/>
        <asp:Panel ID="ActivitesPanel" runat="server" CssClass="Subject-Panel">
            <asp:Label ID="ActivitesLabel" runat="server" Text="Aktywności" CssClass="Subject-Label"></asp:Label>
            <asp:Table ID="ActivitesTable" runat="server"></asp:Table>
            <asp:Button ID="NewActivityButton" runat="server" Text="Dodaj aktywność" OnClick="NewActivityButton_Click" Width="115px" Height="30px" Visible="False" />
            <asp:Button ID="DeactivateActivityButton" runat="server" Text="Deaktywuj aktywność" Width="149px" Height="30px" OnClick="DeactivateActivityButton_Click" Visible="False" />
            <asp:Button ID="DelActivityButton" runat="server" Text="Usuń aktywność" Width="120px" Height="30px" OnClick="DelActivityButton_Click" Visible="False" />
            <asp:DropDownList ID="ActivityDropDownList" runat="server" Width="121px" Visible="False">
            </asp:DropDownList>
        </asp:Panel>
        <hr/>
        <asp:Button ID="GoBackButton" runat="server" Text="Powrót"  Style="width: 200px; height: 40px; font-size: 20px;" OnClick="GoBackButton_Click" />
    </div>
</asp:Content>
