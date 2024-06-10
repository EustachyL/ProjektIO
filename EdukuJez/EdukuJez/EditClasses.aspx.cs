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
using EdukuJez.Migrations;


//po zmainie bazy z 09.06 -roomsAndClasses zakomentowany kod z salami
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
        List<string> Rooms = new List<string> { };
        List<string> Subject = new List<string> { };
        List<string> Group = new List<string> { };
        List<string> Teacher = new List<string> { };

        readonly GroupUsersRepository groupUserRepo = new GroupUsersRepository();
        private ScheduleRepository scheduleRepo = new ScheduleRepository();
        private GroupsRepository groupRepo = new GroupsRepository();
        private SubjectsRepository subjRepo = new SubjectsRepository();
        private UsersRepository userRepo = new UsersRepository();
        private ClassUsersRepository CURepo = new ClassUsersRepository();
        private ClassRoomsRepository classRoomsRepo = new ClassRoomsRepository();

        DateTime currentTime = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserSession.CheckPermission(UserSession.ADMIN_GROUP) == false)
                UserSession.ChangeSiteNoPermission(this, "Main.aspx");
            if (!IsPostBack)
            {
                DateBox.Visible = false;
                Label.Visible = false;
                ListBoxDates.Visible = false;
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
            var classRoom = Convert.ToString(ClassDropDown.SelectedValue);

            var c = new ClassC();

            if (!string.IsNullOrWhiteSpace(DateBox.Text))
            {
                DateTime date;

                if (DateTime.TryParse(DateBox.Text, out date))
                {

                    c.Hour = godzina; c.Cyclicality = date;

                }
                else
                {
                    // Informacja o błędnej dacie
                    Label.Text = "Wprowadź poprawną datę.";
                    return;
                }
            }
            else
            {
                c.Hour = godzina; c.Day = dzien;
            }

            userRepo.Table.First(x => x.UserName == parts[0] && x.UserSurname == parts[1]).Teaches.Add(c);
            groupRepo.Table.First(x => x.Id == group).Classes.Add(c);
            subjRepo.Table.First(x => x.SubjectName == subject).Classes.Add(c);
            classRoomsRepo.Table.First(x => x.Number == classRoom).Classes.Add(c);



            User u = userRepo.Table.First(x => x.UserName == parts[0] && x.UserSurname == parts[1]);
            var CU = new ClassUsers();
            c.Users = new List<ClassUsers>() { CU };
            u.Clasess = new List<ClassUsers>() { CU };


            if (scheduleRepo.Table.Any(x => x.Hour == godzina && x.Day == dzien && x.Warden.UserName == parts[0] && x.Warden.UserSurname == parts[1] && x.Class.Number == classRoom &&  x.Group.Id == group && x.Subject.SubjectName == subject))
            {

            }
            else
            {
                userRepo.Update();
                groupRepo.Update();
                subjRepo.Update();
                classRoomsRepo.Update();    

            }



            ReloadData();
            LoadLessonPlan();
            RefreshListBox();
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
            var classRoom = Convert.ToString(DropDownListClass.SelectedValue);



            ClassC query = scheduleRepo.Table.Include(x => x.Users)
                .FirstOrDefault(x => x.Hour == godzina && x.Day == dzien && x.Warden.UserName == parts[0] && x.Warden.UserSurname == parts[1] && x.Class.Number == classRoom &&  x.Group.Name == group && x.Subject.SubjectName == subject);

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
                RefreshListBox();
            }
            else { }
        }


        private void CreateDynamicControls(ICollection<ClassC> lessonPlan)
        {


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

            List<string> classRoom = Rooms;
            ClassDropDown.DataSource = classRoom;
            ClassDropDown.DataBind();


            ClearTable();



        }

        private void LoadLessonPlan()
        {
            int selectedGroupId;
            if (int.TryParse(GroupDropDown.SelectedValue, out selectedGroupId))
            {
  
                var lessonPlan = scheduleRepo.Table
                    .Where(a => a.Group.Id == selectedGroupId && a.Cyclicality==null)
                    .Include(a => a.Warden)
                    .Include(u => u.Group)
                    .Include(w => w.Subject)
                    .ToList();

                ClearTable();
                //Czyszczenie tabeli

                AssignToCell(lessonPlan);
                //zajecia cykliczne

                 lessonPlan = scheduleRepo.Table
                .Where(a => a.Group.Id == selectedGroupId && a.Cyclicality != null)
                .Include(a => a.Warden)
                .Include(u => u.Group)
                .Include(w => w.Subject)
                .ToList();

                AssignToCell(lessonPlan);
                //nie cykliczne
            }
        }

        private void AssignToCell(ICollection<ClassC> lessonPlan)
        {
            // Znalezienie zeszłej soboty
            DateTime lastSaturday = currentTime.AddDays(-(int)currentTime.DayOfWeek - 1);
            // Znalezienie nadchodzącej soboty
            DateTime nextSaturday = lastSaturday.AddDays(7);

            Dictionary<string, int> dayIndex = new Dictionary<string, int>
        {
    { "Poniedzialek", 1 },
    { "Monday", 1 },
    { "Wtorek", 2 },
    { "Tuesday", 2 },
    { "Sroda", 3 },
    { "Wednesday", 3 },
    { "Czwartek", 4 },
    { "Thursday", 4 },
    { "Piatek", 5 },
    { "Friday", 5 }

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
                int colIndex;

                if (lesson.Cyclicality == null)
                {
                 colIndex = dayIndex[lesson.Day];
                }
                else
                {
                    DateTime cyclicalityDate = lesson.Cyclicality.Value;

                    // przedział od soboty do soboty 
                    if (cyclicalityDate >= lastSaturday && cyclicalityDate <= nextSaturday)
                    { 
                        var day = lesson.Cyclicality.Value.DayOfWeek;
                        colIndex = dayIndex[day.ToString()];
                    }
                    else break;
                }

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

            var roomList = classRoomsRepo.Table.ToList();
            var subList = subjRepo.Table.ToList();
            var groupList = groupRepo.Table.Where(y => y.ParentGroup.Name == UserSession.STUDENT_GROUP).ToList();
            List<GroupUser> groupUserList = groupUserRepo.Table.Include(u => u.User).Include(g => g.Group).ToList();
            var userList = groupUserList.Where(x => x.Group != null && x.Group.Name == UserSession.TEACHER_GROUP && x.User != null).Select(x => x.User).ToList(); ;

            var lessonPlan = scheduleRepo.Table.Include(u => u.Group).Include(u => u.Warden).Include(u => u.Subject).Include(u => u.Class).ToList();

            LoadToChose(groupList, subList, userList, lessonPlan, roomList);

            CreateDynamicControls(lessonPlan);
            
        }
        void LoadToChose(ICollection<Group> groupT, ICollection<Subject> SubjectT, ICollection<User> UserT, ICollection<ClassC> lessonPlan, ICollection<ClassRoom> RoomsT)
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
            foreach (ClassRoom rooms in RoomsT)
            {
                Rooms.Add(rooms.Number.ToString());
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
            RefreshListBox();
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
    

    protected void ChangeButton_Click(object sender, EventArgs e)
    {
            if (DateBox.Visible == false)
            {
                ChangeButton.Text = "nie cykliczne";

                DateBox.Visible = true;
                Label.Visible = true;
                DayDropDown.Visible = false;
                MainTable.Visible = false;
                ListBoxDates.Visible = true;
                RefreshListBox();
            }
            else
            {
                ChangeButton.Text = "cykliczne";

                DateBox.Visible = false;
                Label.Visible = false;
                DayDropDown.Visible = true;
                MainTable.Visible = true;
                ListBoxDates.Visible = false;
            }
     }


        private void RefreshListBox()
        {
            int selectedGroupId;
            if (int.TryParse(GroupDropDown.SelectedValue, out selectedGroupId))
            {
                // Pobierz dane z kalendarza i przypisz do ListBo
                var calendarE = scheduleRepo.Table.Where(a => a.Cyclicality != null && a.Group.Id == selectedGroupId).ToList();

                // Przygotuj listę niestandardowych ciągów do wyświetlenia w ListBoxie
                var listBoxItems = calendarE.Select(a => a.Cyclicality).ToList();

                ListBoxDates.DataSource = listBoxItems;
                ListBoxDates.DataBind();
            }
        }

    }

    }