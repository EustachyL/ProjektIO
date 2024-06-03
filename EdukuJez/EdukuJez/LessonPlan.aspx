<%@ Page Title="Plan Lekcji" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LessonPlan.aspx.cs" Inherits="EdukuJez.LessonPlan" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
            <div style="margin-bottom: 30px;" class="header Container-Title ">
        <img src="Imgs/Lesson_Plan_Title.png" class="logo1" style="height: 82px; width: 480px" />
        <hr />
    </div>
        <div>
            <asp:Table ID="LessonTable" runat="server" CssClass="LessonPlan"></asp:Table>
        </div>
    <asp:DropDownList ID="GroupDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="GroupSelectionChanged">
    <asp:ListItem Text="-- Wybierz grupę --" Value="" />
    </asp:DropDownList>

<asp:Table ID="MainTable" runat="server" CellSpacing="20" CssClass="Center-Form Main-Table" >
    <asp:TableRow>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell>Poniedziałek</asp:TableCell>
        <asp:TableCell>Wtorek</asp:TableCell>
        <asp:TableCell>Środa</asp:TableCell>
        <asp:TableCell>Czwartek</asp:TableCell>
        <asp:TableCell>Piątek</asp:TableCell>

    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>8:00 – 8:45</asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>

    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>8:50 – 9:35</asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>

        <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>9:45 – 10:30</asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>

        <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>10:35 – 11:20</asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>

        <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>11:40 – 12:25</asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>

        <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>12:45 – 13:30</asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>

        <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>13:35 – 14:20</asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>

        <asp:TableCell></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>14:25 – 15:10</asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>

        <asp:TableCell></asp:TableCell>
        <asp:TableCell></asp:TableCell>
    </asp:TableRow>
</asp:Table>
    

</asp:Content>
