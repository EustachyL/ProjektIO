<%@ Page Title="Kalendarz" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Calendars.aspx.cs" Inherits="EdukuJez.Calendars" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <div style="margin-bottom: 30px;" class="header Container-Title ">
        <img src="Imgs/Calendar_Title.png" class="logo1" style="height: 82px; width: 485px" />
        <hr />
    </div>

        <div>
            <asp:Table ID="Calendar" runat="server" CssClass="Calendars"></asp:Table>
        </div>

        <div style="overflow: auto;">
        <div style="float: left;"><asp:Calendar ID="Calendar1" runat="server" Height="700px" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender" Width="565px" ></asp:Calendar></div>
  
                <div style="float: right; margin-left: 20px; background-color: white;">
            <asp:Label ID="TextHolder" runat="server" Text="Tutaj wyświetli się tekst po wybraniu daty z kalendarza" Height="700px" Width="565px" ></asp:Label>
     </div> 
        </div>


</asp:Content>
