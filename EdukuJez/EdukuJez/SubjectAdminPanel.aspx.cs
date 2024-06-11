using EdukuJez.Model.Main;
using EdukuJez.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EdukuJez
{
    public partial class SubjectAdminPanel : Page
    {
        private Subject subjectToDeactivate = new Subject();
        private SubjectsRepository repoS = new SubjectsRepository();

        protected void Page_load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    foreach (var s in repoS.Table)
                    {
                    if(s.Deactivated == false)
                        ListBoxAllSubjects.Items.Add(s.SubjectName);

                    }
                    ButtonAdd.Visible = true;
                    ButtonEdit.Visible = true;
                    ButtonDelete.Visible = true;
                    ButtonDeactivate.Visible = true;
                }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("SubjectAddAdminPanel.aspx");
        }

        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (ListBoxAllSubjects.SelectedItem == null)
            {
                LabelInfo.Text = "Należy wybrać przedmiot";
                LabelInfo.Visible = true;
            }
            else
            {
                var subject = new SubjectsRepository();
                Session["Subject"] = subject.Table.FirstOrDefault(s => s.SubjectName == ListBoxAllSubjects.SelectedItem.Text);
                Response.Redirect("SubjectAddAdminPanel.aspx");
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (ListBoxAllSubjects.SelectedItem == null)
            {
                LabelInfo.Text = "Należy wybrać przedmiot";
                LabelInfo.Visible = true;
            }
            else
            {
                //nazwa przedmiotu musi być unikatowa
                repoS.Delete(repoS.Table.FirstOrDefault(x => x.SubjectName == ListBoxAllSubjects.SelectedItem.Text));
                ListBoxAllSubjects.Items.Remove(ListBoxAllSubjects.SelectedItem);
            }
        }

        protected void ButtonDeactivate_Click(object sender, EventArgs e)
        {
            if (ListBoxAllSubjects.SelectedItem == null)
            {
                LabelInfo.Text = "Należy wybrać przedmiot";
                LabelInfo.Visible = true;
            }
            else
            {
                subjectToDeactivate = repoS.Table.FirstOrDefault(x => x.SubjectName == ListBoxAllSubjects.SelectedItem.Text);
                subjectToDeactivate.Deactivated = true;
                repoS.UpdateRow(subjectToDeactivate);
                ListBoxAllSubjects.Items.Remove(ListBoxAllSubjects.SelectedItem);
                LabelInfo.Text = "Dezaktywacja przedmiotu " + subjectToDeactivate.SubjectName + " przebiegła pomyślnie.";
            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
        }
    }
}