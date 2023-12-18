using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using EdukuJez.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EdukuJez
{
    public partial class LessonPlan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            {
                // Pobierz plan lekcji z bazy danych i wyświetl na stronie
                ScheduleRepository Lessons = new ScheduleRepository();
                var lessonPlan = Lessons.Table.Include(u => u.Group).Include(s => s.Subject).ToList();
                PopulateLessonTable(lessonPlan);
            }
        }


        private void PopulateLessonTable(ICollection<ClassC> lessonPlan)
        {
            foreach (ClassC lesson in lessonPlan)
            {
                TableRow row = new TableRow();
                TableCell cellSubject = new TableCell { Text = lesson.Subject?.SubjectName.ToString() ?? string.Empty };
                TableCell cellName = new TableCell { Text = lesson.Name ?? string.Empty };
                TableCell cellSurname = new TableCell { Text = lesson.Surname ?? string.Empty };
                TableCell cellClass = new TableCell { Text = lesson.Class.ToString() };
                TableCell cellHour = new TableCell { Text = lesson.Hour ?? string.Empty };
                TableCell cellDay = new TableCell { Text = lesson.Day ?? string.Empty };


                row.Cells.Add(cellDay);
                row.Cells.Add(cellSubject);
                row.Cells.Add(cellName);
                row.Cells.Add(cellSurname);
                row.Cells.Add(cellClass);
                row.Cells.Add(cellHour);
                

                LessonTable.Rows.Add(row);
            }
        }
    }
}