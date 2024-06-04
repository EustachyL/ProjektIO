using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using EdukuJez.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EdukuJez
{
    public partial class Calendars : System.Web.UI.Page
    {
        CalendarRepository Calend = new CalendarRepository();
        DataTable dataTable = new DataTable();







        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        private void PopulateCalendar(List<Repositories.Calendar> calendarE)
        {

          //  var filteredCalendar = calendarE.Where(a => a.Date >= currentDate).ToList();

   
        }




        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //Czyszczenie
            TextHolder.Text = "";
            dataTable.Clear();

            string date = Calendar1.SelectedDate.ToString().Substring(0, 10); //wybrana data bez godziny

            var calendar = Calend.Table.ToList();

            int i = 1;
            foreach(var Day in calendar)
            {
                var ax = Day.Date.ToString().Substring(0, 10);
                if(date == ax)
                {
                    TextHolder.Text += "Wydarzenie "+ i + "\n" + Day.Desc + "\n" ;
                }
            }

          
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            var calendar = Calend.Table.ToList();

            foreach (var Day in calendar)
            {
                 if (e.Day.Date.Month==Day.Date.Month)
                 {   
                    if (e.Day.Date == Day.Date)
                    {
                        e.Cell.BackColor = System.Drawing.Color.Green;
                    }
                 }
            }
        }

    }
}
