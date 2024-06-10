<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditClassRooms.aspx.cs" Inherits="EdukuJez.EditClassRooms" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderNav" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">



    
        <div style="margin-bottom: 30px;" class="header Container-Title ">
 <asp:Button ID="GoBackAdminButton" runat="server" Text="Panel Administratora" 
     Style="margin-top: 12px; width: 170px; height: 60px; font-size: 20px;
float: left; white-space: normal;" OnClick="GoBackAdminButton_Click"
     CssClass="Main-Panel-Image" ForeColor="Black" EnableTheming="True"/>
        <img src="Imgs/Timetable_Management_Page_Title.png" class="logo1" style="height: 82px; width: 661px; margin-left: 100px;"  />

<asp:Button ID="ClassRoomButton" runat="server" Text="Powrót do planu zajęć" 
    Style="margin-top: 12px; width: 170px; height: 60px; font-size: 20px; float: right; white-space: normal;" 
    OnClick="GoBackClassesButton_Click" CssClass="Main-Panel-Image" ForeColor="Black" EnableTheming="True"/>
    <hr />
    </div>


<div class="Center-Form" style="margin-top: 20px; width: 100%; text-align: center; display: flex; flex-direction: column; align-items: center;">

    <div style="margin-bottom: 20px;">
        <asp:TextBox ID="TextBoxNumber" runat="server" placeholder="Wprowadź numer" style="width: 300px; height: 30px; font-size: 16px; margin-bottom: 10px;"></asp:TextBox>
        <asp:TextBox ID="TextBoxDesc" runat="server" placeholder="Wprowadź opis" style="width: 300px; height: 30px; font-size: 16px;"></asp:TextBox>
    </div>

    <div style="margin-bottom: 20px;">
        <asp:Button ID="ButtonAdd" runat="server" Text="Dodaj nową klasę" OnClick="ButtonAdd_Click" style="width: 220px; height: 40px; font-size: 20px;" />
        <br />
        <asp:Button ID="ButtonDelete" runat="server" Text="Usuń klasę" OnClick="ButtonDelete_Click" style="width: 220px; height: 40px; font-size: 20px; margin-top: 10px;" />
        <asp:Button ID="ButtonEdit" runat="server" Text="Edytuj klasę" OnClick="ButtonEdit_Click" style="width: 220px; height: 40px; font-size: 20px; margin-top: 10px;" />
        <asp:Label ID="LabelInfo" runat="server" Text="Label" Visible="False" Font-Size="24px" ForeColor="#CC0000" style="display: block; margin-top: 10px;"></asp:Label>
    </div>

    <div style="width: 100%; overflow-x: auto;">
        <asp:Repeater ID="myRepeater" runat="server">
            <HeaderTemplate>
                <table border="1" style="margin: 0 auto;">
                    <tr>
                        <th>Numer</th>
                        <th>Opis</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style='<%# "background-color: #a0b891;" %>'>
                    <td><%# Eval("Number") %></td>
                    <td><%# Eval("Desc") %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
</asp:Content>
