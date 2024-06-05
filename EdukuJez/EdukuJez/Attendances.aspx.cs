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

namespace EdukuJez
{
    public partial class Attendances : System.Web.UI.Page
    {
        User currentuser = UserSession.GetSession()?.user;
        AttendancesRepository attendancesRepo = new AttendancesRepository();
        ScheduleRepository scheduleRepository = new ScheduleRepository();
        ClassUsersRepository classUsersRepository = new ClassUsersRepository();
        UsersRepository usersRepo = new UsersRepository();
        SubjectsRepository subjectsRepo = new SubjectsRepository(); 
       
        string dayOfWeek;
        string subject;
        protected void Page_Load(object sender, EventArgs e)
        {
            subject = (string)Session["AttendancesSubject"];
            subject = "przedmiot1"; //tymczasowo
            List<string> subjects = new List<string>(); //przedmioty ktorych uczy zalogowany

            subjects = subjectsRepo.Table
                .Where(x => x.Classes.Any(c => c.Warden == currentuser))
                .Select(x => x.SubjectName).ToList();

            SubjectDropDownList.DataSource = subjects;
            SubjectDropDownList.SelectedValue = subject;
            SubjectDropDownList.DataBind();
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
                //SelectedDateAdmin();
                //TO DO
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
                if(subject != null)
                    SelectedDateTeacher(subject);
                else
                {
                    
                    SelectedDateTeacher(subject); 
                }
            }
        }

        //wybor daty w kalendarzu przez ucznia
        void SelectedDateStudent(User uczen) 
        {
            DataTable dataTable = new DataTable();

            List<ClassC> classes  = classUsersRepository.Table
                .Include(x=>x.Class.Attendances)
                .Include(x=>x.Class.Subject)
                .Include(x => x.Class.Group)
                .Where(x => x.Class.Day == dayOfWeek && x.Class.Group.Users.Any(y=>y.User == currentuser))
                .Select(x => x.Class).ToList();  //zajecia w ktorych bierze udzial zalogowany uzytkownik, ktore odbywaja sie dnia zaznaczonego w kalendarzu

            //wiersze z zajeciami:
            if (classes.Count == 0) //jesli nie ma zajec wybranego dnia
            {
                DataRow row = dataTable.NewRow();
                row[0] = "Brak zajęć do wyświetlenia";
                dataTable.Rows.Add(row);
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
                    row[0] =c.Subject.SubjectName + "\n" + attendance; //wiersz z nazwą przedmiotu i obecnoscia
                    dataTable.Rows.Add(row);
                    numClass++;
                }
            }
            StudentGridView.DataSource = dataTable;
        }
        
        //wybor daty w kalendarzu przez nauczyciela
        void SelectedDateTeacher(string subjectName)
        {
            var students = classUsersRepository.Table //studenci uczeszczajacy na dany przedmiot
                .Where(x => x.Class.Warden == currentuser && x.Class.Subject.SubjectName == subjectName && x.Class.Day == dayOfWeek) //zajecia gdzie opiekunem jest zalogowany i maja okreslona nazwe
                .SelectMany(x => x.Class.Group.Users.Select(y=>y.User)).ToList();

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
                    .Where(x=>x.Class.Subject.SubjectName == subject && x.Date == Calendar1.SelectedDate)
                    .Select(x=>x.Presence).ToString();

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
                if(prevValue == null) //jesli nie bylo wpisanej obenosci -> dodanie nowej
                {
                    Attendance attendance = new Attendance();
                    
                    
                    attendance.Student = (User)usersRepo.Table
                        .Where(x=>x.Id == id)
                        .First();

                    attendance.Date = Calendar1.SelectedDate;

                    attendance.Class = (ClassC)classUsersRepository.Table
                        .Include(x=>x.Class.Subject)
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
        //wybor daty w kalendarzu przez admina
       /* void SelectedDateAdmin()
        {
           

        }
       */
        protected void SubjectDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            subject = ddl.SelectedValue;
            Session["AttendancesSubject"] = subject;
        }

        protected void AdminGridView_DataBound(object sender, EventArgs e)
        {

        }

        protected void AdminGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}
