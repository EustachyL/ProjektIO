using EdukuJez.Model.ServerAccess.Repositories;
using EdukuJez.Repositories;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EdukuJez
{
    public partial class EditClassRooms : System.Web.UI.Page
    {
         ClassRoomsRepository ClassRoomsRepo = new ClassRoomsRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserSession.CheckPermission(UserSession.ADMIN_GROUP) == false)
                UserSession.ChangeSiteNoPermission(this, "Main.aspx");

            if (!IsPostBack)
            {
             ReloadData();
            }
        }

        private void ReloadData()
        {
            List<ClassRoom> classes = ClassRoomsRepo.Table.ToList();
            var Data = classes.Select(x => new
            {
                Number = x.Number,
                Desc = x.Desc
            });

            myRepeater.DataSource = Data;
            myRepeater.DataBind();
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
                string number = TextBoxNumber.Text;
                string desc = TextBoxDesc.Text;

            if (string.IsNullOrWhiteSpace(number))
            {
                return;
            }

            var query = ClassRoomsRepo.Table.FirstOrDefault(x => x.Number == TextBoxNumber.Text);

            if (query == null)
            {
                ClassRoomsRepo.AddNewEntry(new Repositories.ClassRoom { Number = number, Desc = desc });
                ReloadData();
            }
            else
            {
                LabelInfo.Text = "Sala już istnieje";
                LabelInfo.Visible = true;
            }



        }

       protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            string selectedDateText = TextBoxNumber.Text;
            string selectedDescText = TextBoxDesc.Text; ;

            var query = ClassRoomsRepo.Table.FirstOrDefault(x => x.Number == TextBoxNumber.Text);


            if (query != null)
            {
                query.Desc = selectedDescText;
                ClassRoomsRepo.EditEntry(query);
                ReloadData();
            }
            else
            {

                LabelInfo.Text = "Nieprawidłow dane";
                LabelInfo.Visible = true;
            }
        }
        



        protected void ButtonDelete_Click(object sender, EventArgs e)
        {


            var query = ClassRoomsRepo.Table.FirstOrDefault(x => x.Number == TextBoxNumber.Text);

            if (query != null)
              {

                  ClassRoomsRepo.RemoveEntry(query);
                  ReloadData();
           } 
            else
           {

                LabelInfo.Text = "Nieprawidłow dane";
                LabelInfo.Visible = true;
            }

            
        }
        protected void GoBackClassesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditClasses.aspx");
        }

        protected void GoBackAdminButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
        }
    }
}