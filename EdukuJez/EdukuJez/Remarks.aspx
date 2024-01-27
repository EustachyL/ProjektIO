﻿<%@ Page Title="Uwagi" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Remarks.aspx.cs" Inherits="EdukuJez.Remark" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   
    <div style="margin-bottom: 30px;" class="header Container-Title ">
               <asp:Button ID="GoBackButton" runat="server" Text="Strona Główna" Style="width: 250px; height: 40px; font-size: 20px; float: left;" OnClick="GoBackButton_Click" />
        <img src="Imgs/Remarks_Title.png" class="logo1" style="height: 82px; width: 350px; float: none;" />
             <asp:Button ID="x" runat="server" Text=" " Style="width: 250px; height: 40px; font-size: 20px; float: right;" BackColor="#FEFAE0" BorderColor="#FEFAE0" BorderStyle="None" />
        <hr />
    </div>
    
        <div style="margin-top: 20px; width: 2000px;">
             <asp:Panel ID="TeachersPanel" runat="server">
            <asp:Panel ID="LabelsPanel" runat="server" Style="display: inline-block;">
                <asp:Label ID="StudentGroupLabel" runat="server" Text="Wybierz klasę:" Font-Size="24px" Style="display: block; margin-bottom: 10px;"></asp:Label>
                <asp:Label ID="StudentLabel" runat="server" Text="Wybierz ucznia:" Font-Size="24px" Style="display: block; margin-bottom: 10px;"></asp:Label>
                </asp:Panel>

            <asp:Panel ID="ListsPanel" runat="server" Style="display: inline-block;">
               <asp:DropDownList ID="StudentsGroupsList" runat="server" Style="display: block; margin-bottom: 10px; width: 255px; height: 30px; font-size: 16px;" AutoPostBack="true" OnSelectedIndexChanged="StudentsGroupsListSelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="StudentsList" runat="server" Style="display: block; margin-bottom: 10px; width: 255px; height: 30px; font-size: 16px;"></asp:DropDownList>
                </asp:Panel>
            <br/>
            <asp:Label ID="InfoLabel" runat="server" Text="Wpisz uwagę:" Font-Size="24px" Style="display: block; margin-bottom: 10px;"></asp:Label>
              <br/>
             <asp:TextBox ID="NewRemarkTextBox" runat="server" Style="display: block; margin-bottom: 10px; width: 250px; height: 25px; font-size: 16px;" AutoPostBack="true"></asp:TextBox>
            <br/>
            <asp:Button  ID="AddNewRemarkButton" runat="server" Text="Wstaw uwagę" Style="width: 150px; height: 40px; font-size: 20px;" Enabled="False" OnClick="AddNewRemarkButton_Click"/>
          
    </asp:Panel>

                         <asp:Panel ID="StudentsPanel" runat="server">
<asp:Repeater ID="myRepeater" runat="server">
                <HeaderTemplate>
                    <table border="1">
                        <tr>
                            <th>Nauczyciel wystawiający</th>
                            <th>Uwaga</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Submitter") != null ? string.Format("{0} {1}", Eval("Submitter.UserName") ?? "brak", Eval("Submitter.UserSurname") ?? "brak") : "brak" %></td>
                        <td><%# Eval("Contents") %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
    </asp:Panel>

            </div>


    </asp:Content>
