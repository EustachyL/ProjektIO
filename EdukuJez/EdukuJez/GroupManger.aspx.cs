﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EdukuJez.Repositories;

namespace EdukuJez
{
    public partial class GroupManger : System.Web.UI.Page
    {
        readonly GroupsRepository repo = new GroupsRepository();
        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshTables();
            
        }

        protected void NewGroupButton_Click(object sender, EventArgs e)
        {
            var ng = new Group();
            ng.Name = NewGroupTextBox.Text;
            var parentName = PGroupDropdown.SelectedValue;
            ng.ParentGroup = repo.Table.First(x => x.Name==parentName );
            repo.Insert(ng);
        }

        void RefreshTables()
        {
            myRepeater.DataSource = repo.Table;
            myRepeater.DataBind();
            PGroupDropdown.DataSource = repo.Table.Select(x => x.Name);
            PGroupDropdown.DataBind();
        }
    }
}
