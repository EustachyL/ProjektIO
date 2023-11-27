using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Windows;


namespace EdukuJez.Model.ServerAccess.Repositories
{
    public class ClassesDataBaseDelete
    {

        public void DeleteData(string dzien, string godzina, int nauczyciel, int przedmiot, int grupa)
        {
            string query = "DELETE FROM Classes WHERE Day = '" + dzien + "'AND Hour ='" + godzina + "'AND ID_Teacher ='" + nauczyciel + "'AND ID_Group ='" + przedmiot + "'AND ID_Subject ='" + grupa + "'";
            var SC = ServerClient.StartConnection().SendRequestToSqlServer(query);
            SC.Wait();

        }
    }
}
