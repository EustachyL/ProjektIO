<%@ Page Title="Obecności" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Attendances.aspx.cs" Inherits="EdukuJez.Attendances" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-bottom: 30px;" class="header Container-Title ">
        <h2>Obecności</h2>
        <hr />
    </div>

    <div style="overflow: auto;">
        <asp:DropDownList ID="SubjectDropDownList" runat="server" AutoPostBack ="true" OnSelectedIndexChanged="SubjectDropDownList_SelectedIndexChanged" Visible ="false"></asp:DropDownList>
        <asp:Panel ID="AdminDropDownListsPanel" runat="server" Visible ="false">
            <asp:DropDownList ID="SubjectAdminDropDownList" runat="server" OnSelectedIndexChanged="UpdateAdminGridView" AutoPostBack="true"></asp:DropDownList>
            <asp:DropDownList ID="GroupDropDownList" runat="server" OnSelectedIndexChanged="UpdateAdminGridView" AutoPostBack="true"></asp:DropDownList>
            <asp:DropDownList ID="StudentsDropDownList" runat="server" OnSelectedIndexChanged="UpdateAdminGridView" AutoPostBack="true"></asp:DropDownList>
        </asp:Panel>
        <br />
        <div style="float: left;"><asp:Calendar ID="Calendar1" runat="server" Height="226px" OnSelectionChanged="Calendar1_SelectionChanged" Width="565px"></asp:Calendar><asp:Button ID="CalendarButton" runat="server" Text="Odznacz datę" Enabled="false" Visible="false" OnClick="CalendarButton_Click"/></div>
        
        <div style="float: right;">
            <asp:Label ID="DateLabel" runat="server" Text="Data" Visible = "false"/>
            <br /><asp:Label ID="AdditionalLabel" runat="server" Text="Addidional Data" Visible = "false"/>
           <br /> <asp:GridView ID="StudentGridView" runat="server" Visible = "False"></asp:GridView>
        
        <asp:GridView ID="TeacherGridView" runat="server" Visible = "False" AutoGenerateColumns="false" OnRowDataBound ="TeacherGridView_RowDataBound" DataKeyNames = "Id">
            <Columns>
                <asp:TemplateField HeaderText="Imię i Nazwisko">
                    <ItemTemplate>
                        <%# Eval("UserName") %> <%# " " %> <%# Eval("UserSurname") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Obecność">
                    <ItemTemplate>
                        <asp:DropDownList ID="AttendanceDropDownList" runat="server" OnSelectedIndexChanged ="TeacherSetsAttendance" AutoPostBack ="true">
                            <asp:ListItem Text = "Obecny/a" Value = "Obecny"/>
                            <asp:ListItem Text = "Nieobecny/a" Value = "Nieobecny"/>
                            <asp:ListItem Text = "Spóźniony/a" Value = "Spóźniony"/>
                            <asp:ListItem Text = "brak" Value = ""/>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
            <asp:GridView ID="AdminGridView" runat="server" Visible = "False"></asp:GridView>
        </div>
       </div>
    </asp:Content>
