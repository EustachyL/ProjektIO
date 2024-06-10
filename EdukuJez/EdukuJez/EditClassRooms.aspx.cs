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

/*        protected void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (ListBoxAllDates.SelectedIndex >= 0)
            {
                // Split the selected item to separate date and description
                string[] selectedItemParts = ListBoxAllDates.SelectedItem.Text.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (selectedItemParts.Length == 2)
                {
                    // Trim to remove any leading or trailing whitespaces
                    string selectedDateText = selectedItemParts[0].Trim();
                    string selectedDescText = selectedItemParts[1].Trim();

                    if (DateTime.TryParse(selectedDateText, out DateTime selectedDate))
                    {
                        // Pobierz zaznaczony wpis do edycji na podstawie daty
                        var selectedEntry = Calend.Table.FirstOrDefault(entry => entry.Date == selectedDate && entry.Desc == selectedDescText);

                        if (selectedEntry != null)
                        {
                            // Zaktualizuj dane z formularza
                            DateTime date;
                            if (DateTime.TryParse(TextBoxDate.Text, out date))
                            {
                                string desc = TextBoxDescription.Text;

                                // Ustaw nowe wartości
                                selectedEntry.Date = date;
                                selectedEntry.Desc = desc;

                                // Zapisz zmiany
                                Calend.EditEntry(selectedEntry);

                                // Odśwież dane i przekształć kalendarz
                                RefreshListBox();
                            }
                            else
                            {
                                // Informacja o błędnej dacie
                                LabelInfo.Text = "Wprowadź poprawną datę.";
                                LabelInfo.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        // Obsługa błędu parsowania daty
                        LabelInfo.Text = "Nieprawidłowy format daty w ListBoxie.";
                        LabelInfo.Visible = true;
                    }
                }
                else
                {
                    // Handle invalid format
                    LabelInfo.Text = "Nieprawidłowy format daty w ListBoxie.";
                    LabelInfo.Visible = true;
                }
            }
        }*/



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