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
        ScheduleRepository Lessons = new ScheduleRepository();
        GroupsRepository GroupsRepo = new GroupsRepository(); // Dodane repozytorium do obsługi grup

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGroups(); // Metoda do załadowania grup do DropDownList przy pierwszym załadowaniu strony
            }

            LoadLessonPlan(); // Metoda do załadowania planu lekcji
        }

        private void LoadGroups()
        {
            List<Group> groups = GroupsRepo.Table.ToList();
            GroupDropDown.DataSource = groups;
            GroupDropDown.DataTextField = "Name";
            GroupDropDown.DataValueField = "Id";
            GroupDropDown.DataBind();
        }

        private void LoadLessonPlan()
        {
            int selectedGroupId;
            if (int.TryParse(GroupDropDown.SelectedValue, out selectedGroupId))
            {
                var lessonPlan = Lessons.Table
                    .Where(a => a.Group.Id == selectedGroupId)
                    .Include(a => a.Warden)
                    .Include(u => u.Group)
                    .Include(w => w.Subject)
                    .ToList();

                AssignToCell(lessonPlan);
            }
        }



        private void AssignToCell(ICollection<ClassC> lessonPlan)
        {

            ClearTable();

            Dictionary<string, int> dayIndex = new Dictionary<string, int>
        {
            { "Poniedzialek",1  },
            { "Wtorek", 2 },
            { "Sroda", 3 },
            { "Czwartek", 4 },
            { "Piatek", 5 }

        };

            Dictionary<string, int> hourIndex = new Dictionary<string, int>
        {
            { "8:00 – 8:45", 1 },
            { "8:50 – 9:35", 2 },
            { "9:45 – 10:30", 3 },
            { "10:35 – 11:20", 4 },
            { "11:40 – 12:25", 5 },
            { "12:45 – 13:30", 6},
            { "13:35 – 14:20", 7 },
            { "14:25 – 15:10", 8 }
        };



            foreach (var lesson in lessonPlan)
            {
                int rowIndex = hourIndex[lesson.Hour];
                int colIndex = dayIndex[lesson.Day];


                // Czyszczenie komórki przed dodaniem nowej zawartości
                MainTable.Rows[rowIndex].Cells[colIndex].Controls.Clear();

                //kolor :)
                MainTable.Rows[rowIndex].Cells[colIndex].BackColor = System.Drawing.Color.LightGreen;

                // Dodanie tekstu i przycisku do komórki
                Label lbl = new Label();
                lbl.Text = lesson.Subject.SubjectName + "<br />" + lesson.Warden.UserName + "<br />  Sala: " + lesson.Class + "<br />";
                MainTable.Rows[rowIndex].Cells[colIndex].Controls.Add(lbl);


            }
        }

        protected void GroupSelectionChanged(object sender, EventArgs e)
        {
            // Zdarzenie wywoływane po zmianie wybranej grupy w DropDownList
            LoadLessonPlan(); // Załaduj plan lekcji dla wybranej grupy
        }

        private void ClearTable()
        {
            for (int i = 1; i < 9; i++)
            {

                for (int j = 1; j < 6; j++)
                {

                    MainTable.Rows[i].Cells[j].BackColor = System.Drawing.Color.Empty;
                    MainTable.Rows[i].Cells[j].Text = "    ";
                }

            }
        }
    }
}
