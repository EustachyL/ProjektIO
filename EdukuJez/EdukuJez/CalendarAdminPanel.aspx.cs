using System;
using EdukuJez.Repositories;
using System.Linq;
using System.Data;
using System.Web.UI.WebControls;

namespace EdukuJez
{
    public partial class CalendarAdminPanel : System.Web.UI.Page
    {
        CalendarRepository Calend = new CalendarRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Inicjalizacja strony
                RefreshListBox();
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            DateTime date;
            if (DateTime.TryParse(TextBoxDate.Text, out date))
            {
                string desc = TextBoxDescription.Text;

                // Dodaj nowy wpis do kalendarza
                Calend.AddNewEntry(new Repositories.Calendar { Date = date, Desc = desc });

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

        protected void ButtonEdit_Click(object sender, EventArgs e)
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
        }



        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (ListBoxAllDates.SelectedIndex >= 0)
            {
                // Split the selected item to separate date and description
                string[] selectedItemParts = ListBoxAllDates.SelectedItem.Text.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (selectedItemParts.Length == 2)
                {
                    // Trim to remove any leading or trailing whitespaces
                    string selectedDateText = selectedItemParts[0].Trim();
                    string selectedDescText = selectedItemParts[1];
                    if (DateTime.TryParse(selectedDateText, out DateTime selectedDate))
                    {
                        // Pobierz zaznaczony wpis do usunięcia na podstawie daty
                        var selectedEntry = Calend.Table.FirstOrDefault(entry => entry.Date == selectedDate && entry.Desc == selectedDescText);

                        if (selectedEntry != null)
                        {
                            // Usuń zaznaczony wpis
                            Calend.RemoveEntry(selectedEntry);
                            // Odśwież dane i przekształć kalendarz
                            RefreshListBox();
                        }
                    }
                    else
                    {
                        // Obsługa błędu parsowania daty
                        LabelInfo.Text = "Nieprawidłowy format daty.";
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
        }


        private void RefreshListBox()
        {
            // Pobierz dane z kalendarza i przypisz do ListBox
            var calendarE = Calend.Table.OrderBy(a => a.Date).ToList();

            // Przygotuj listę niestandardowych ciągów do wyświetlenia w ListBoxie
            var listBoxItems = calendarE.Select(a => $"{a.Date.ToString("dd-MM-yyyy")} : {a.Desc}").ToList();

            ListBoxAllDates.DataSource = listBoxItems;
            ListBoxAllDates.DataBind();
        }




        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            //Czyszczenie
            TextBoxDescription.Text = "";

            string date = Calendar1.SelectedDate.ToString().Substring(0, 10);

            TextBoxDate.Text = date;


        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            var calendar = Calend.Table.ToList();

            foreach (var Day in calendar)
            {
                if (e.Day.Date.Month == Day.Date.Month)
                {
                    if (e.Day.Date == Day.Date)
                    {
                        e.Cell.BackColor = System.Drawing.Color.Green;
                    }
                }
            }
        }




        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
        }
    }
}
