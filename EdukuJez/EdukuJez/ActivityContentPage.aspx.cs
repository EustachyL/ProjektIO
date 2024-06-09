using EdukuJez.Model.Main;
using EdukuJez.Repositories;
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

        protected void Page_Load(object sender, EventArgs e) 
        {
            MainActLabel.Text = SubjectManager.ShowedActivity?.Name;
        }



    }
}