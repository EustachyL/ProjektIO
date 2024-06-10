<%@ Page Title="Timetable Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditClasses.aspx.cs" Inherits="EdukuJez.EditClasses" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
            <asp:Table ID="LessonTable" runat="server" CssClass="LessonPlan"></asp:Table>
        </div>

        <div style="margin-bottom: 30px;" class="header Container-Title ">
                <asp:Button ID="GoBackButton" runat="server" Text="Panel Administratora" Style="margin-top: 12px; width: 170px; height: 60px; font-size: 20px; float: left; white-space: normal;" OnClick="GoBackButton_Click" CssClass="Main-Panel-Image" ForeColor="Black" EnableTheming="True"/>
        <img src="Imgs/Timetable_Management_Page_Title.png" class="logo1" style="height: 82px; width: 661px" />
            <asp:Button ID="Button1" runat="server" Text=" " Style="width: 250px; height: 40px; font-size: 20px; float: right;" BackColor="#FEFAE0" BorderColor="#FEFAE0" BorderStyle="None" />
         <hr />
    </div>

    <asp:DropDownList ID="GroupDropDown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="GroupSelectionChanged">
    <asp:ListItem Text="-- Wybierz grupę --" Value="" />
    </asp:DropDownList>
                <asp:TextBox ID="DateBox" runat="server" ></asp:TextBox>
        <asp:DropDownList ID="DayDropDown" runat="server">
    <asp:ListItem Text="-- Wybierz grupę --" Value="" />
    </asp:DropDownList>

        <asp:DropDownList ID="HourDropDown" runat="server">
    <asp:ListItem Text="-- Wybierz grupę --" Value="" />
    </asp:DropDownList>

        <asp:DropDownList ID="SubjectDropDown" runat="server">
    <asp:ListItem Text="-- Wybierz grupę --" Value="" />
    </asp:DropDownList>

        <asp:DropDownList ID="ClassDropDown" runat="server">
    <asp:ListItem Text="-- Wybierz grupę --" Value="" />
    </asp:DropDownList>

        <asp:DropDownList ID="TeacherDropDown" runat="server">
    <asp:ListItem Text="-- Wybierz grupę --" Value="" />
    </asp:DropDownList>

    <asp:Button ID="AddButton" runat="server" Text="Dodaj" OnClick="AddButton_Click" CssClass="btn btn-primary" />
    <br/>
     <asp:Button ID="ChangeButton" runat="server" Text="Zajęcia cykliczne" OnClick="ChangeButton_Click" CssClass="btn btn-primary" />
    <br/>
                <asp:Label ID="Label" runat="server" Text="Dla zajęć niecyklicznych wprowadź datę: DD.MM.RRRR "></asp:Label>
    <br/>

 <div style="text-align: center;">

<asp:ListBox ID="ListBoxDates" runat="server" Width="1200px" Height="400px" Font-Size="20px" style="margin-right: 150px;"></asp:ListBox>


 </div>


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
