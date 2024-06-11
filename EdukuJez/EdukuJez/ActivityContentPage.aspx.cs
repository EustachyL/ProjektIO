using EdukuJez.Model.Main;
using EdukuJez.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace EdukuJez
{
    public partial class ActivityContentPage : System.Web.UI.Page
    {
        public GradesRepository repoGrades = new GradesRepository();
        public ActivitiesRepository repoAct = new ActivitiesRepository();
        public UsersRepository repoUser = new UsersRepository();

        protected void Page_Load(object sender, EventArgs e) 
        {
            if (SubjectManager.ShowedActivity == null)
            {
                Response.Redirect("SubjectContentPage.aspx");
            }
            MainActLabel.Text = SubjectManager.ShowedActivity.Name;
            if (UserSession.CheckPermission(UserSession.STUDENT_GROUP) == true)
            {
                StudentView();
            }
            else if (UserSession.CheckPermission(UserSession.TEACHER_GROUP) == true)
            {

            }
        }
        void TeacherView()
        {
            Panel1.Visible = false;
        }

        void StudentView()
        {
            Panel1.Visible = true;
            var grade = repoGrades.Table.Include(x => x.Activity).Include(x => x.Users)
                .FirstOrDefault(x => x.Activity.Id == SubjectManager.ShowedActivity.Id && x.Users.Id == UserSession.GetSession().user.Id);
            if (grade == null)
                LabelSubDesc.Text = "brak oceny ";
            else
            {
                LabelSubDesc.Text = "Otrzymano ocenę: ";
                LabelSubDesc.Text += grade.GradeValue.ToString();
            }

        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SubjectContentPage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Submission a = new Submission();
            var activity = SubjectManager.ShowedActivity;
            a.Activity = activity;
            a.Name = activity.Name;

    
            if (FileUpload1.HasFile)
            {
                try
                {
                    // Pobranie pliku z kontrolki FileUpload
                    HttpPostedFile postedFile = FileUpload1.PostedFile;
                    a.ContentType = postedFile.ContentType;
                    // Konwersja pliku do tablicy bajtów
                    byte[] fileData = new byte[postedFile.ContentLength];
                    postedFile.InputStream.Read(fileData, 0, postedFile.ContentLength);
                    a.Content = fileData;
                    a.Text = postedFile.FileName;
                    repoAct.Table.FirstOrDefault(x => x.Id == activity.Id).Submissions.Add(a);
                    repoAct.Update();
                    repoUser.Table.FirstOrDefault(x => x.Id == UserSession.GetSession().user.Id).Submissions.Add(a);
                    repoUser.Update();
                }
                catch (Exception ex)
                {
                }

            }
            

        }
    }
}