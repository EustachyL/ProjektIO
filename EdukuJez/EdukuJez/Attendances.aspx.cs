using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.UI.WebControls;
using EdukuJez.Model.ServerAccess.Repositories;
using EdukuJez.Repositories;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EdukuJez
{
    public partial class Attendances : System.Web.UI.Page
    {
        User currentuser = UserSession.GetSession()?.user;
        #region Repozytoria
        AttendancesRepository attendancesRepo = new AttendancesRepository();
        ScheduleRepository scheduleRepository = new ScheduleRepository();
        ClassUsersRepository classUsersRepository = new ClassUsersRepository();
        UsersRepository usersRepo = new UsersRepository();
        SubjectsRepository subjectsRepo = new SubjectsRepository();
        GroupsRepository groupsRepo = new GroupsRepository();
        #endregion
        string dayOfWeek;
        string subject;

        protected void Page_Load(object sender, EventArgs e)
        {
            subject = (string)Session["AttendancesSubject"];
            //subject = "przedmiot1"; //tymczasowo
            if (!IsPostBack)
            {
                if (UserSession.CheckPermission(UserSession.TEACHER_GROUP) == true)
                {
                    List<string> subjects = new List<string>(); //przedmioty ktorych uczy zalogowany

                    subjects = subjectsRepo.Table //przedmioty ktore uczy zalogowany
                        .Where(x => x.Classes.Any(c => c.Warden == currentuser))
                        .Select(x => x.SubjectName).ToList();

                    if (subjects.Any())
                    {
                        SubjectDropDownList.DataSource = subjects;
                        SubjectDropDownList.DataBind();
                        if (subject != null)
                        {
                            ListItem item = SubjectDropDownList.Items.FindByText(subject);
                            SubjectDropDownList.SelectedIndex = SubjectDropDownList.Items.IndexOf(item);
                        }
                        else
                        {
                            SubjectDropDownList.SelectedValue = SubjectDropDownList.Items[0].Value;
                            subject = SubjectDropDownList.SelectedItem.Text;
                        }
                    }
                    else
                    {
                        SubjectDropDownList.Items.Add("Brak przedmiotów");
                    }
                    SubjectDropDownList.DataBind();
                    SubjectDropDownList.Visible = true;
                }
                if (UserSession.CheckPermission(UserSession.ADMIN_GROUP) == true) //jesli zalogowany jest adminem
                {
                    //usupelnianie dropdownlist:
                    //przedmioty:
                    List<Tuple<string, int>> subjects = subjectsRepo.Table
                        .Select(x => new Tuple<string, int>(x.SubjectName, x.Id)).ToList();
                    SubjectAdminDropDownList.Items.Add(new ListItem("Wybierz przedmiot", "-1"));

                    if (!subjects.Any()) //jesli nie ma zadnych przedmiotow w bazie
                        SubjectAdminDropDownList.Items.Add(new ListItem("Brak przedmiotów", "-1"));
                    else
                    {
                        foreach (var s in subjects)
                        {
                            SubjectAdminDropDownList.Items.Add(new ListItem(s.Item1, s.Item2.ToString())); //tekst listy to nazwa przedmiotu, wartosc to id przedmiotu
                            
                        }
                        SubjectAdminDropDownList.DataBind();
                        if (subject != null)
                        {
                            ListItem item = SubjectAdminDropDownList.Items.FindByText(subject);
                            SubjectAdminDropDownList.SelectedIndex = SubjectAdminDropDownList.Items.IndexOf(item);
                        }
                        else
                            SubjectAdminDropDownList.SelectedIndex = 0;
                        SubjectAdminDropDownList.DataBind();
                        
                    }
                    //grupy:
                    List<Tuple<string, int>> groups = groupsRepo.Table
                        .Select(x => new Tuple<string, int>(x.Name, x.Id)).ToList();
                    GroupDropDownList.Items.Add(new ListItem("Wybierz grupę", "-1"));
                    if (!groups.Any())
                        GroupDropDownList.Items.Add(new ListItem("Brak grup", "-1"));
                    else
                        foreach (var g in groups)
                            GroupDropDownList.Items.Add(new ListItem(g.Item1, g.Item2.ToString())); //tekst listy to nazwa grupy, wartosc to id grupy

                    //uczniowie:
                    List<Tuple<string, int>> students = usersRepo.Table
                        .Where(x => x.Groups.Any(g => g.Group.ParentGroup.Name == UserSession.STUDENT_GROUP))
                        .Select(x => new Tuple<string, int>(x.UserName + " " + x.UserSurname, x.Id)).ToList();
                    StudentsDropDownList.Items.Add(new ListItem("Wybierz ucznia", "-1"));
                    if (!students.Any())
                        StudentsDropDownList.Items.Add(new ListItem("Brak uczniów", "-1"));
                    else
                        foreach (var s in students)
                            StudentsDropDownList.Items.Add(new ListItem(s.Item1, s.Item2.ToString())); //tekst listy to imie i nazwisko ucznia, wartosc to jego id

                    AdminDropDownListsPanel.Visible = true;


                    //widocznosc przycisku do odklikania daty:
                    CalendarButton.Visible = true;
                    if(subject!= null)
                        UpdateAdminGridView(null, null);
                }
            }
            if (IsPostBack)
            {
                dayOfWeek = (string)Session["dayOfWeek"];
            }
        }
        //zaznaczenie dnia w kalendarzu
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            switch (Calendar1.SelectedDate.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    dayOfWeek = "Poniedzialek";
                    break;
                case DayOfWeek.Tuesday:
                    dayOfWeek = "Wtorek";
                    break;
                case DayOfWeek.Wednesday:
                    dayOfWeek = "Sroda";
                    break;
                case DayOfWeek.Thursday:
                    dayOfWeek = "Czwartek";
                    break;
                case DayOfWeek.Friday:
                    dayOfWeek = "Piatek";
                    break;
                case DayOfWeek.Saturday:
                    dayOfWeek = "Sobota";
                    break;
                case DayOfWeek.Sunday:
                    dayOfWeek = "Niedziela";
                    break;
                default:
                    dayOfWeek = "Wystąpił problem z dniem tyg.";
                    break;
            }
            Session["dayOfWeek"] = dayOfWeek;
            DateLabel.Text = Calendar1.SelectedDate.ToString().Substring(0, 10) + " " + dayOfWeek; //wybrana data bez godziny i dzień tygodnia
            DateLabel.Visible = true;

            AdditionalLabel.Text = subject;
            AdditionalLabel.Visible = true;

            if (UserSession.CheckPermission(UserSession.ADMIN_GROUP) == true) //jesli zalogowany jest adminem
            {
                UpdateAdminGridView(null, null);
            }
            else if (UserSession.CheckPermission(UserSession.STUDENT_GROUP) == true) //jesli zalogowany jest uczniem
            {
                SelectedDateStudent(currentuser);
            }
            else if (UserSession.CheckPermission(UserSession.PARENT_GROUP) == true) //jesli zalogowany jest rodzicem
            {
                SelectedDateStudent(UserSession.GetSession().checkedChild);
            }
            else if (UserSession.CheckPermission(UserSession.TEACHER_GROUP) == true)
            {
                if (subject != null)
                    SelectedDateTeacher(subject);
                else
                {

                    SelectedDateTeacher(SubjectDropDownList.SelectedItem.Text);
                }
            }
            CalendarButton.Enabled = true;
        }

        #region Metody Ucznia
        //wybor daty w kalendarzu przez ucznia
        void SelectedDateStudent(User uczen)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Obecności");

            List<ClassC> classes = classUsersRepository.Table
                .Include(x => x.Class.Attendances)
                .Include(x => x.Class.Subject)
                .Include(x => x.Class.Group)

                .Where(x => (x.Class.Day == dayOfWeek || x.Class.Cyclicality == Calendar1.SelectedDate) && x.Class.Group.Users.Any(y => y.User == currentuser))
                .Select(x => x.Class).ToList();  //zajecia w ktorych bierze udzial zalogowany uzytkownik, ktore odbywaja sie dnia zaznaczonego w kalendarzu

            //wiersze z zajeciami:
            if (classes.Count == 0) //jesli nie ma zajec wybranego dnia
            {
                AdditionalLabel.Text = "Brak zajęć do wyświetlenia";
                AdditionalLabel.Visible = true;
                StudentGridView.Visible = false;
            }
            else //jesli sa jakies zajecia wybranego dnia
            {
                int numClass = 0;
                foreach (var c in classes) //uzupelnianie tabeli
                {
                    var attendance = c.Attendances.Select(x => x.Presence).ToString(); //obecnosc na zajeciach c
                    if (attendance == "" || c.Attendances.Count == 0)
                    {
                        attendance = "brak wpisu";
                    }
                    DataRow row = dataTable.NewRow();
                    row[0] = c.Subject.SubjectName + "\n" + attendance; //wiersz z nazwą przedmiotu i obecnoscia
                    dataTable.Rows.Add(row);
                    numClass++;
                }
                StudentGridView.DataSource = dataTable;
                StudentGridView.DataBind();
                StudentGridView.Visible = true;
                AdditionalLabel.Visible = false;
            }

        }
        #endregion

        #region Metody Nauczyciela
        //wybor daty w kalendarzu przez nauczyciela
        void SelectedDateTeacher(string subjectName)
        {
            var students = classUsersRepository.Table //studenci uczeszczajacy na dany przedmiot
                .Where(x => x.Class.Warden == currentuser && x.Class.Subject.SubjectName == subjectName && x.Class.Day == dayOfWeek) //zajecia gdzie opiekunem jest zalogowany i maja okreslona nazwe
                .SelectMany(x => x.Class.Group.Users.Select(y => y.User)).ToList();

            if (students.Count == 0)//jesli  nie ma uczniow
            {
                DateLabel.Text += ": Brak przypisanych uczniów";
                TeacherGridView.Visible = false;
                return;
            }
            TeacherGridView.DataSource = students;
            TeacherGridView.DataBind();
            TeacherGridView.Visible = true;
        }

        //odpowiednie uzupełnienie dropDown list z obecnościami
        protected void TeacherGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            User dataItem = (User)e.Row.DataItem;
            DropDownList ddl = (DropDownList)e.Row.FindControl("AttendanceDropDownList");

            if (ddl != null)
            {
                string attendance = dataItem.Attendance //obecnosc ucznia
                    .Where(x => x.Class.Subject.SubjectName == subject && x.Date == Calendar1.SelectedDate)
                    .Select(x => x.Presence).ToString();

                switch (attendance)
                {
                    case "Obecny":
                        ddl.SelectedValue = "Obecny";
                        break;
                    case "Nieobecny":
                        ddl.SelectedValue = "Nieobecny/a";
                        break;
                    case "Spóźniony":
                        ddl.SelectedValue = "Spóźniony/a";
                        break;
                    default:
                        ddl.SelectedValue = "";
                        break;
                }
                ddl.DataBind();
            }
        }

        //nauczyciel zmienia / wpisuje obecność
        protected void TeacherSetsAttendance(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer; //wiersz w ktorym jest dana DrpDown Lista
            string prevValue = ViewState[ddl.UniqueID] as string;

            if (prevValue != ddl.SelectedValue)
            {
                int id = (int)TeacherGridView.DataKeys[row.RowIndex].Value; //pobranie id obiektu ucznia, który uzupełnił ten wiersz tabeli
                if (prevValue == null) //jesli nie bylo wpisanej obenosci -> dodanie nowej
                {
                    Attendance attendance = new Attendance();


                    attendance.Student = (User)usersRepo.Table
                        .Where(x => x.Id == id)
                        .First();

                    attendance.Date = Calendar1.SelectedDate;

                    if (subject == null)
                        subject = SubjectDropDownList.SelectedItem.Text;
                    attendance.Class = (ClassC)classUsersRepository.Table
                        .Include(x => x.Class.Subject)
                        .Where(x => x.Class.Subject.SubjectName == subject && x.Class.Day == dayOfWeek)
                        .Select(x => x.Class)
                        .First();

                    attendance.Presence = ddl.SelectedValue;

                    attendancesRepo.Table.Add(attendance); //dodanie obecnosci
                    attendancesRepo.Update(); //<--------------------------------------------------------- sprawdzic !
                }
                else //zmiana wczesniej wpisanej obecnosci
                {
                    Attendance attendance = (Attendance)attendancesRepo.Table
                        .Where(x => x.Student.Id == id && x.Date == Calendar1.SelectedDate && x.Class.Subject.SubjectName == subject)
                        .Select(x => x);
                    attendance.Presence = ddl.SelectedValue;

                    attendancesRepo.Table.Update(attendance); //zmiana obecnosci
                }

            }
        }

        //zmiana wybranego przedmiotu przez nauczyciela
        protected void SubjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            subject = ddl.SelectedValue;
            Session["AttendancesSubject"] = subject;
        }
        #endregion

        #region Metody Admina
        //zmiana parametrow przez admina
        protected void UpdateAdminGridView(object sender, EventArgs e)
        {//data, przedmiot, grupa, uczen, obecnosc
            AdditionalLabel.Visible = false;
            var query = attendancesRepo.Table.Include(x => x.Class.Subject).Include(x => x.Class.Group).Include(x => x.Student).AsQueryable();
            if (Calendar1.SelectedDate != DateTime.MinValue)
            {
                query = query.Where(x => x.Date == Calendar1.SelectedDate);
            }
            if (SubjectAdminDropDownList.SelectedValue != "-1")
            {
                query = query.Where(x => x.Class.Subject.Id == int.Parse(SubjectAdminDropDownList.SelectedValue));
            }
            if (GroupDropDownList.SelectedValue != "-1")
            {
                query = query.Where(x => x.Class.Group.Id == int.Parse(GroupDropDownList.SelectedValue));
            }
            if (StudentsDropDownList.SelectedValue != "-1")
            {
                query = query.Where(x => x.Student.Id == int.Parse(StudentsDropDownList.SelectedValue));
            }
            
            List<Attendance> attendances = query.Select(a => a).ToList();

            if (!attendances.Any())
            {
                AdditionalLabel.Text = "Brak obecności związanych z podanymi warunkami";
                AdditionalLabel.Visible = true;
                AdminGridView.Visible = false;
            }
            else
            {
                DataTable adminDataTable = new DataTable();
                adminDataTable.Columns.Add("Data");
                adminDataTable.Columns.Add("Przedmiot");
                adminDataTable.Columns.Add("Grupa");
                adminDataTable.Columns.Add("Uczeń");
                adminDataTable.Columns.Add("Obecność");
                foreach (var a in attendances)
                {
                    DataRow row = adminDataTable.NewRow();
                    row["Data"] = a.Date.ToString().Substring(0, 10);
                    row["Przedmiot"] = a.Class.Subject.SubjectName;
                    row["Grupa"] = a.Class.Group.Name;
                    row["Uczeń"] = a.Student.UserName + " " + a.Student.UserSurname;
                    row["Obecność"] = a.Presence;
                    adminDataTable.Rows.Add(row);
                }


                AdminGridView.DataSource = adminDataTable;
                AdminGridView.DataBind();
                AdminGridView.Visible = true;
                AdditionalLabel.Visible = false;
            }
        }
        protected void CalendarButton_Click(object sender, EventArgs e)
        {
            Calendar1.SelectedDates.Clear();
            CalendarButton.Enabled = false;
            DateLabel.Visible = false;
            UpdateAdminGridView(null, null);
        }
        #endregion

    }
}
