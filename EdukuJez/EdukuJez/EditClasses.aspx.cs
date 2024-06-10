
using EdukuJez.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.EntityFrameworkCore;
using EdukuJez.Model.ServerAccess.Repositories;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace EdukuJez
{
    public partial class EditClasses : System.Web.UI.Page
    {

        // Deklaracja zmiennych do przechowywania DropDownList jako zmiennych klasy
        private DropDownList DropDownListDay;
        private DropDownList DropDownListHour;
        private DropDownList DropDownListTeacher;
        private DropDownList DropDownListGroup;
        private DropDownList DropDownListSubject;
        private DropDownList DropDownListClass;


        List<string> Class = new List<string> { };
        List<string> Subject = new List<string> { };
        List<string> Group = new List<string> { };
        List<string> Name = new List<string> { };
        List<string> Surname = new List<string> { };
        List<string> Teacher = new List<string> { };

        readonly GroupUsersRepository groupUserRepo = new GroupUsersRepository();
        private ScheduleRepository scheduleRepo = new ScheduleRepository();
        private GroupsRepository groupRepo = new GroupsRepository();
        private SubjectsRepository subjRepo = new SubjectsRepository();
        private UsersRepository userRepo = new UsersRepository();
        private ClassUsersRepository CURepo = new ClassUsersRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserSession.CheckPermission(UserSession.ADMIN_GROUP) == false)
                UserSession.ChangeSiteNoPermission(this, "Main.aspx");
            if (!IsPostBack)
            {

                ReloadData();
            }
            else
            {
                LoadLessonPlan();
            }
        }


        protected void AddButton_Click(object sender, EventArgs e)
        {
            string dzien = DayDropDown.SelectedValue;
            string godzina = HourDropDown.SelectedValue;

            // rozdzielenie imienia i nazwiska na dwa osobne stringi do wysłania do DB
            string Teacher = TeacherDropDown.SelectedValue;
            string[] parts = Teacher.Split(' ');


            int group = int.Parse(GroupDropDown.SelectedValue);
            var subject = Convert.ToString(SubjectDropDown.SelectedValue);
            int classRoom = Convert.ToInt32(ClassDropDown.SelectedValue);

            var c = new ClassC() { Hour = godzina, Day = dzien, Class = classRoom };

            userRepo.Table.First(x => x.UserName == parts[0] && x.UserSurname == parts[1]).Teaches.Add(c);
            groupRepo.Table.First(x => x.Id == group).Classes.Add(c);
            subjRepo.Table.First(x => x.SubjectName == subject).Classes.Add(c);



            User u = userRepo.Table.First(x => x.UserName == parts[0] && x.UserSurname == parts[1]);
            var CU = new ClassUsers();
            c.Users = new List<ClassUsers>() { CU };
            u.Clasess = new List<ClassUsers>() { CU };


            if (scheduleRepo.Table.Any(x => x.Hour == godzina && x.Day == dzien && x.Warden.UserName == parts[0] && x.Warden.UserSurname == parts[1] && x.Class == classRoom && x.Group.Id == group && x.Subject.SubjectName == subject))
            {

            }
            else
            {
                userRepo.Update();
                groupRepo.Update();
                subjRepo.Update();

            }



            ReloadData();
            LoadLessonPlan();
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            string dzien = DropDownListDay.SelectedValue;
            string godzina = DropDownListHour.SelectedValue;

            // rozdzielenie imienia i nazwiska na dwa osobne stringi do wysłania do DB
            string Teacher = DropDownListTeacher.SelectedValue;
            string[] parts = Teacher.Split(' ');

            var group = Convert.ToString(DropDownListGroup.SelectedValue);
            var subject = Convert.ToString(DropDownListSubject.SelectedValue);
            int classRoom = Convert.ToInt32(DropDownListClass.SelectedValue);



            ClassC query = scheduleRepo.Table.Include(x => x.Users)
                .FirstOrDefault(x => x.Hour == godzina && x.Day == dzien && x.Warden.UserName == parts[0] && x.Warden.UserSurname == parts[1] && x.Class == classRoom && x.Group.Name == group && x.Subject.SubjectName == subject);

            scheduleRepo.Delete(query);
            if (query != null)
            {
                var CU = query.Users.ToList();

                foreach (var users in CU)
                {
                    CURepo.Delete(users);
                }
                ReloadData();
                LoadLessonPlan();
            }
            else { }
        }

        protected void DeleteButtonDynamic_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            string buttonId = clickedButton.ID;
            string[] buttonParts = buttonId.Split('_');
            int lessonId = int.Parse(buttonParts[1]);

            ClassC query = scheduleRepo.Table.Include(x => x.Users)
                .FirstOrDefault(x => x.Id == lessonId);

            scheduleRepo.Delete(query);
            if (query != null)
            {
                var CU = query.Users.ToList();

                foreach (var users in CU)
                {
                    CURepo.Delete(users);
                }
                ReloadData();
                LoadLessonPlan();
            }
            else { }
        }


        private void CreateDynamicControls(ICollection<ClassC> lessonPlan)
        {
     //       DropDownListTeacher?.Items.Clear();
     //       DropDownListGroup?.Items.Clear();
     //       DropDownListSubject?.Items.Clear();
     //       MainTable.Controls.Clear();

            List<string> days = new List<string> { "Poniedzialek", "Wtorek", "Sroda", "Czwartek", "Piatek" };
            DayDropDown.DataSource = days;
            DayDropDown.DataBind();

            List<string> hours = new List<string> { "8:00 – 8:45", "8:50 – 9:35", "9:45 – 10:30", "10:35 – 11:20", "11:40 – 12:25", "12:45 – 13:30", "13:35 – 14:20", "14:25 – 15:10" };
            HourDropDown.DataSource = hours;
            HourDropDown.DataBind();

            List<string> teachers = Teacher;
            TeacherDropDown.DataSource = teachers;
            TeacherDropDown.DataBind();

            List<Group> groups = groupRepo.Table.ToList();
            GroupDropDown.DataSource = groups;
            GroupDropDown.DataTextField = "Name";
            GroupDropDown.DataValueField = "Id";
            GroupDropDown.DataBind();

            List<string> subjects = Subject;
            SubjectDropDown.DataSource = subjects;
            SubjectDropDown.DataBind();

            List<string> classRoom = new List<string> { "1", "2", "3", "4", "5", "6" };
            ClassDropDown.DataSource = classRoom;
            ClassDropDown.DataBind();


            //Pętla wypełnia tabela 5x8
            ClearTable();

            /*
            //dodawanie wartości do tabeli
            foreach (ClassC lesson in lessonPlan)
            
            {
                TableRow row = new TableRow();

                // Tworzenie nowych komórek TableCell
                TableCell cellId = new TableCell { Text = lesson.Id.ToString() };
                TableCell cellClass = new TableCell { Text = lesson.Class.ToString() };
                TableCell cellHour = new TableCell { Text = lesson.Hour?.ToString() };
                TableCell cellDay = new TableCell { Text = lesson.Day?.ToString() };
                TableCell cellTeacherName = new TableCell { Text = lesson.Warden?.UserName.ToString() };
                TableCell cellTeacherSurname = new TableCell { Text = lesson.Warden?.UserSurname.ToString() };
                TableCell cellGroup = new TableCell { Text = lesson.Group.Name.ToString() };
                TableCell cellSubject = new TableCell { Text = lesson.Subject.SubjectName.ToString() };

                // Dodawanie komórek do wiersza
                row.Cells.Add(cellId);
                row.Cells.Add(cellDay);
                row.Cells.Add(cellHour);
                row.Cells.Add(cellClass);
                row.Cells.Add(cellGroup);
                row.Cells.Add(cellSubject);
                row.Cells.Add(cellTeacherName);
                row.Cells.Add(cellTeacherSurname);

                // Dodanie komórki z przyciskiem usuwania
                TableCell deleteButtonCell = new TableCell();
                Button deleteButton = new Button();
                deleteButton.ID = "DeleteButton_" + lesson.Id.ToString();  // Ustawienie unikalnego identyfikatora dla przycisku
                deleteButton.Text = "Usuń";
                deleteButton.Click += new EventHandler(DeleteButtonDynamic_Click);  // Podłączenie metody obsługującej zdarzenie kliknięcia
                deleteButtonCell.Controls.Add(deleteButton);
                row.Cells.Add(deleteButtonCell);

                // Dodawanie wiersza do tabeli MainTable
                MainTable.Rows.Add(row);
            }
            */

        }

        private void LoadLessonPlan()
        {
            int selectedGroupId;
            if (int.TryParse(GroupDropDown.SelectedValue, out selectedGroupId))
            {
                var lessonPlan = scheduleRepo.Table
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

                // Tworzenie przycisku
                Button deleteButton = new Button();
                deleteButton.ID = "DeleteButton_" + lesson.Id.ToString();  // Ustawienie unikalnego identyfikatora dla przycisku
                deleteButton.Text = "Usuń";
                deleteButton.Click += new EventHandler(DeleteButtonDynamic_Click);  // Podłączenie metody obsługującej zdarzenie kliknięcia


                // Dodanie tekstu i przycisku do komórki
                Label lbl = new Label();
                lbl.Text = lesson.Subject.SubjectName + "<br />" + lesson.Warden.UserName + "<br />  Sala: " + lesson.Class + "<br />";
                MainTable.Rows[rowIndex].Cells[colIndex].Controls.Add(lbl);

                // Dodanie przycisku do komórki
                MainTable.Rows[rowIndex].Cells[colIndex].Controls.Add(deleteButton);
            }
        }

           

        private void ReloadData()
        {


            var subList = subjRepo.Table.ToList();
            var groupList = groupRepo.Table.Where(y => y.ParentGroup.Name == UserSession.STUDENT_GROUP).ToList();
            List<GroupUser> groupUserList = groupUserRepo.Table.Include(u => u.User).Include(g => g.Group).ToList();
            var userList = groupUserList.Where(x => x.Group != null && x.Group.Name == UserSession.TEACHER_GROUP && x.User != null).Select(x => x.User).ToList(); ;

            var lessonPlan = scheduleRepo.Table.Include(u => u.Group).Include(u => u.Warden).Include(u => u.Subject).ToList();

            LoadToChose(groupList, subList, userList, lessonPlan);

            CreateDynamicControls(lessonPlan);
            
        }
        void LoadToChose(ICollection<Group> groupT, ICollection<Subject> SubjectT, ICollection<User> UserT, ICollection<ClassC> lessonPlan)
        {
            foreach (Group group in groupT)
            {

                Group.Add(group.Name);

            }
            foreach (Subject subject in SubjectT)
            {
                Subject.Add(subject.SubjectName.ToString());
            }
            foreach (User user in UserT)
            {

                var a = user.UserName.ToString() + " " + user.UserSurname.ToString();
                Teacher.Add(a);

            }
            foreach (ClassC lesson in lessonPlan)
            {
                Class.Add(lesson.Class.ToString());
            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
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