﻿<%@ Page Title="Account Management" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountsManagement.aspx.cs" Inherits="EdukuJez.AccountsManagement" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-bottom: 30px;" class="header Container-Title ">
                       <asp:Button ID="GoBackButton" runat="server" Text="Powrót do Panelu Administratora" Style="margin-left: 90px; width: 260px; height: 85px; font-size: 20px; float: left; white-space: normal;" OnClick="GoBackButton_Click" CssClass="Main-Panel-Image" ForeColor="Black" EnableTheming="True"/>
        <img src="Imgs/Accounts_Management_Page_Title.png" class="logo1" style="height: 82px; width: 661px" />
             <asp:Button ID="x" runat="server" Text=" " Style="width: 250px; height: 40px; font-size: 20px; float: right;" BackColor="#FEFAE0" BorderColor="#FEFAE0" BorderStyle="None" />
        <hr />
    </div>
    <div>
        <asp:Label ID="MainInfoLabel" runat="server" Text="Wpisz login użytkownika, którego chcesz dodać, edytować lub usunąć:" Font-Size="28px"></asp:Label>
        <div style="margin-top: 20px; width: 2000px;">
            <asp:Panel ID="LabelsPanel" runat="server" Style="display: inline-block;">
                <asp:Label ID="LoginLabel" runat="server" Text="Login:" Font-Size="24px" Style="display: block; margin-bottom: 10px;"></asp:Label>
                <asp:Label ID="NameLabel" runat="server" Text="Imię:" Font-Size="24px" Visible="False" Style="display: block; margin-bottom: 10px;"></asp:Label>
                <asp:Label ID="SurnameLabel" runat="server" Text="Nazwisko:" Font-Size="24px" Visible="False" Style="display: block; margin-bottom: 10px;"></asp:Label>
                <asp:Label ID="PasswordLabel" runat="server" Text="Hasło:" Font-Size="24px" Visible="False" Style="display: block; margin-bottom: 10px;"></asp:Label>
                <asp:Label ID="GroupLabel" runat="server" Text="Grupa podstawowa:" Font-Size="24px" Visible="False" Style="display: block; margin-bottom: 10px;"></asp:Label>
            </asp:Panel>

            <asp:Panel ID="TextBoxesPanel" runat="server" Style="display: inline-block;">
                <asp:TextBox ID="LoginBox" runat="server" Style="display: block; margin-bottom: 10px; width: 250px; height: 25px; font-size: 16px;" AutoPostBack="True" OnTextChanged="LoginBoxChanged"></asp:TextBox>
                <asp:TextBox ID="NameBox" runat="server" Style="display: block; margin-bottom: 10px; width: 250px; height: 25px; font-size: 16px;" Visible="false"></asp:TextBox>
                <asp:TextBox ID="SurnameBox" runat="server" Style="display: block; margin-bottom: 10px; width: 250px; height: 25px; font-size: 16px;" Visible="false"></asp:TextBox>
                <asp:TextBox ID="PasswordBox" runat="server" Style="display: block; margin-bottom: 10px; width: 250px; height: 25px; font-size: 16px;" Visible="false"></asp:TextBox>
                <asp:DropDownList ID="GroupBox" runat="server" Style="display: block; margin-bottom: 10px; width: 255px; height: 30px; font-size: 16px;" Visible="false"></asp:DropDownList>

            </asp:Panel>

            <asp:Button ID="RestartButton" runat="server" Text="Zatwierdź" Style="width: 150px; height: 40px; font-size: 20px;" OnClick="ConfirmRestartClick" Visible="false" />

        </div>

        <div style="margin-top: 20px; width: 2020px;">
            <asp:Button ID="ConfirmDeleteButton" runat="server" Text="Potwierdź" Style="width: 150px; height: 40px; font-size: 20px;" OnClick="ConfirmDeleteClick" Visible="false" />
            <asp:Button ID="AddUserButton" runat="server" Text="Dodaj" OnClick="AddClick" Style="width: 150px; height: 40px; font-size: 20px;" Enabled="False" />
             <asp:Button ID="EditUserButton" runat="server" Text="Edytuj" OnClick="EditClick" Style="width: 150px; height: 40px; font-size: 20px;" Enabled="False" />
            <asp:Button ID="DeleteUserButton" runat="server" Text="Usuń" OnClick="DeleteClick" Style="width: 150px; height: 40px; font-size: 20px;" Enabled="False" />
        </div>
        <div style="margin-top: 20px; width: 2020px">
            <asp:Button ID="ConfirmAddButton" runat="server" Text="Zatwierdź" Style="width: 150px; height: 40px; font-size: 20px;" OnClick="ConfirmAddClick" Visible="false" />
         <asp:Button ID="ConfirmEditButton" runat="server" Text="ZatwierdźEdit" Style="width: 150px; height: 40px; font-size: 20px;" OnClick="ConfirmEditClick" Visible="false" />
        
        </div>
        <div style="margin-top: 20px; width: 2020px">
            <asp:Label ID="InfoLabel" runat="server" Font-Size="24px" ForeColor="#CC0000"></asp:Label>
        </div>
    </div>

    <div class="Center-Form" style="margin-top: 20px; width: 2000px; text-align: center;">
            <asp:Repeater ID="myRepeater" runat="server">
                <HeaderTemplate>
                    <table border="1">
                        <tr>
                            <th>Id</th>
                            <th>Login</th>
                            <th>Imię</th>
                            <th>Nazwisko</th>
                            <th>Grupa podstawowa</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("UserId") %></td>
                        <td><%# Eval("UserLogin") %></td>
                        <td><%# Eval("UserName")%></td>
                        <td><%# Eval("UserSurname") %></td>
                        <td><%# Eval("ParentGroup") %></td>

                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>


</asp:Content>