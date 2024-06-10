using System;
using System.Linq;
using System.Web.UI.WebControls;
using EdukuJez.Repositories;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens;
using System.Configuration;

namespace EdukuJez
{
    public partial class AccountsManagement : System.Web.UI.Page
    {
        private User newUser = new User();
        private User userToEdit = new User();
        private UsersRepository usersRepository = new UsersRepository();
        private GroupUsersRepository groupsUsersRepository = new GroupUsersRepository();
        private GroupsRepository groupsRepo = new GroupsRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (UserSession.CheckPermission(UserSession.ADMIN_GROUP) == false)
                UserSession.ChangeSiteNoPermission(this, "Main.aspx");
            if (!IsPostBack)
            {
                List<User> users = usersRepository.Table.ToList();
                List<GroupUser> groupUserList = groupsUsersRepository.Table.Include(u => u.User).Include(g => g.Group).ToList();

                var mergedData = users.Select(user => new
                {
                    UserId = user.Id,
                    UserLogin = user.UserLogin,
                    UserName = user.UserName,
                    UserSurname = user.UserSurname,
                    ParentGroup = returnGroup(user.Id),
                    Deactivated = user.Deactivated
                });

                myRepeater.DataSource = mergedData;
                myRepeater.DataBind();

                List<Group> groups = groupsRepo.Table.ToList();
                GroupBox.DataSource = groups.Where(x => x.ParentGroup == null).Select(x => x.Name);
                GroupBox.DataBind();
            }
        }

        protected string returnGroup(int UserId)
        {
            string groupName = null;
            List<GroupUser> groupUserList = groupsUsersRepository.Table.Include(u => u.User).Include(g => g.Group).ToList();
            List<Group> allGroups = groupUserList.Where(x => x.User.Id == UserId).Select(x => x.Group).ToList();
            List<string> parentGroups = allGroups.Where(x => x.ParentGroup == null).Select(x => x.Name).ToList();
            groupName = parentGroups.FirstOrDefault();
            return groupName;
        }

        private void SetControlVisibility(bool loginBox, bool passwordBox, bool groupBox, bool confirmAddButton, bool confirmEditButton,
            bool confirmDeactivateButton, bool confirmDeleteButton, bool restartButton, bool nameLabel, bool nameBox,
            bool surnameLabel, bool surnameBox, bool passwordLabel, bool groupLabel, bool loginLabel, bool loginBoxVisible = true)
        {
            LoginBox.Enabled = loginBox;
            PasswordBox.Visible = passwordBox;
            GroupBox.Visible = groupBox;
            ConfirmAddButton.Visible = confirmAddButton;
            ConfirmEditButton.Visible = confirmEditButton;
            ConfirmDeactivateButton.Visible = confirmDeactivateButton;
            ConfirmDeleteButton.Visible = confirmDeleteButton;
            RestartButton.Visible = restartButton;
            NameLabel.Visible = nameLabel;
            NameBox.Visible = nameBox;
            SurnameLabel.Visible = surnameLabel;
            SurnameBox.Visible = surnameBox;
            PasswordLabel.Visible = passwordLabel;
            GroupLabel.Visible = groupLabel;
            LoginLabel.Visible = loginLabel;
            LoginBox.Visible = loginBoxVisible;
        }

        protected void AddClick(object sender, EventArgs e)
        {
            MainInfoLabel.Text = "Wypełnij dane nowego użytkownika:";
            SetControlVisibility(false, true, true, true, false, false, false, false, true, true, true, true, true, true, false);
            DeleteUserButton.Visible = false;
            DeactivateUserButton.Visible = false;
            AddUserButton.Visible = false;
            EditUserButton.Visible = false;
        }

        protected void DeactivateClick(object sender, EventArgs e)
        {
            MainInfoLabel.Text = "Czy na pewno chcesz dezaktywować tego użytkownika? Konto o tej nazwie zostanie usunięte, ale aktywności do niego przypisane nadal będą widoczne.";
            SetControlVisibility(false, false, false, false, false, true, false, false, false, false, false, false, false, false, false);
        }

        protected void DeleteClick(object sender, EventArgs e)
        {
            MainInfoLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#CC0000");
            MainInfoLabel.Text = "Czy na pewno chcesz usunąć tego użytkownika?  TA OPERACJA JEST NIEODWRACALNA! <br> Spowoduje to również usunięcie wszystkich powiązanych z nim aktywności. Jeśli chcesz usunąć użytkownika bez usuwania jego aktywności, użyj opcji dezaktywacji.";
            SetControlVisibility(false, false, false, false, false, false, true, false, false, false, false, false, false, false, false);
        }

        protected void EditClick(object sender, EventArgs e)
        {
            MainInfoLabel.Text = "Wpisz nowe dane wybranego użytkownika:";
            SetControlVisibility(false, true, false, false, true, false, false, false, true, true, true, true, true, false, true);

            userToEdit = usersRepository.Table.FirstOrDefault(x => x.UserLogin == LoginBox.Text);
            NameBox.Text = userToEdit.UserName;
            SurnameBox.Text = userToEdit.UserSurname;
        }

        protected void ConfirmDeactivateClick(object sender, EventArgs e)
        {
            userToEdit = usersRepository.Table.FirstOrDefault(x => x.UserLogin == LoginBox.Text);
            userToEdit.UserName = usersRepository.Table.Where(x => x.UserLogin == LoginBox.Text).Select(x => x.UserName).FirstOrDefault();
            userToEdit.UserSurname = usersRepository.Table.Where(x => x.UserLogin == LoginBox.Text).Select(x => x.UserSurname).FirstOrDefault();
            userToEdit.UserPassword = usersRepository.Table.Where(x => x.UserLogin == LoginBox.Text).Select(x => x.UserPassword).FirstOrDefault();

            userToEdit.Deactivated = true;

            usersRepository.UpdateRow(userToEdit);

            MainInfoLabel.Text = "Dezaktywowałeś konto użytkownika o loginie " + LoginBox.Text + ". <br> Kliknij poniższy przycisk, aby dodać, edytować, dezaktywować lub usunąć kolejnego użytkownika.";

            SetControlVisibility(false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);
            myRepeater.DataBind();
        }

        protected void ConfirmDeleteClick(object sender, EventArgs e)
        {
            MainInfoLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            usersRepository.Delete(usersRepository.Table.First(x => x.UserLogin == LoginBox.Text));
            MainInfoLabel.Text = "Usunąłeś z bazy danych użytkownika o loginie " + LoginBox.Text + ". <br> Kliknij poniższy przycisk, aby dodać, edytować, dezaktywować lub usunąć kolejnego użytkownika.";
            SetControlVisibility(false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);
            myRepeater.DataBind();
        }

        protected void ConfirmAddClick(object sender, EventArgs e)
        {
            if (!newUser.IsNameValid(NameBox.Text))
            {
                InfoLabel.Text = "Niepoprawne imię.";
            }
            else if (!newUser.IsSurnameValid(SurnameBox.Text))
            {
                InfoLabel.Text = "Niepoprawne nazwisko.";
            }
            else if (!newUser.IsPasswordValid(PasswordBox.Text))
            {
                InfoLabel.Text = "Hasło musi się składać od 8 do 50 znaków, <br> co najmniej: jednej cyfry, jednej małej i jednej wielkiej litery <br> oraz co najmniej jednego ze znaków: . ! @ # $ % & ? <br> nie może także zawierać znaków polskich.";
            }
            else
            {
                InfoLabel.Visible = false;
                newUser.UserLogin = LoginBox.Text;
                newUser.UserName = NameBox.Text;
                newUser.UserSurname = SurnameBox.Text;
                newUser.UserPassword = PasswordBox.Text;

                Group g = groupsRepo.Table.FirstOrDefault(x => x.Name == GroupBox.SelectedValue.ToString());
                var gu = new GroupUser();
                g.Users = new List<GroupUser>() { gu };
                newUser.Groups = new List<GroupUser>() { gu };
                usersRepository.Insert(newUser);
                groupsRepo.Update();

                MainInfoLabel.Text = "Dodałeś do bazy danych użytkownika o loginie " + newUser.UserLogin +
                                     ". <br> Kliknij poniższy przycisk, aby dodać, edytować, dezaktywować lub usunąć kolejnego użytkownika.";

                SetControlVisibility(false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);
                myRepeater.DataBind();
            }
        }

        protected void ConfirmEditClick(object sender, EventArgs e)
        {
            userToEdit = usersRepository.Table.FirstOrDefault(x => x.UserLogin == LoginBox.Text);
            userToEdit.UserName = usersRepository.Table.Where(x => x.UserLogin == LoginBox.Text).Select(x => x.UserName).FirstOrDefault();
            userToEdit.UserSurname = usersRepository.Table.Where(x => x.UserLogin == LoginBox.Text).Select(x => x.UserSurname).FirstOrDefault();
            userToEdit.UserPassword = usersRepository.Table.Where(x => x.UserLogin == LoginBox.Text).Select(x => x.UserPassword).FirstOrDefault();
            if (!userToEdit.IsNameValid(NameBox.Text) && !string.IsNullOrEmpty(NameBox.Text))
            {
                InfoLabel.Text = "Niepoprawne imię.";
            }
            else if (!userToEdit.IsSurnameValid(SurnameBox.Text) && !string.IsNullOrEmpty(SurnameBox.Text))
            {
                InfoLabel.Text = "Niepoprawne nazwisko.";
            }
            else if (!userToEdit.IsPasswordValid(PasswordBox.Text) && !string.IsNullOrEmpty(PasswordBox.Text))
            {
                InfoLabel.Text = "Hasło musi się składać od 8 do 50 znaków, <br> co najmniej: jednej cyfry, jednej małej i jednej wielkiej litery <br> oraz co najmniej jednego ze znaków: . ! @ # $ % & ? <br> nie może także zawierać znaków polskich.";
            }
            else
            {
                InfoLabel.Visible = false;

                if (!string.IsNullOrEmpty(NameBox.Text))
                {
                    userToEdit.UserName = NameBox.Text;
                }
                if (!string.IsNullOrEmpty(SurnameBox.Text))
                {
                    userToEdit.UserSurname = SurnameBox.Text;
                }
                if (!string.IsNullOrEmpty(PasswordBox.Text))
                {
                    userToEdit.UserPassword = PasswordBox.Text;
                }

                usersRepository.UpdateRow(userToEdit);

                MainInfoLabel.Text = "Edytowałeś dane użytkownika o loginie " + userToEdit.UserLogin +
                                     ". <br> Kliknij poniższy przycisk, aby dodać, edytować, dezaktywować lub usunąć kolejnego użytkownika.";

                SetControlVisibility(false, false, false, false, false, false, false, true, false, false, false, false, false, false, false);
                myRepeater.DataBind();
            }
        }

        protected void LoginBoxChanged(object sender, EventArgs e)
        {
            if (usersRepository.IsLoginInDatabase(LoginBox.Text))
            {
                DeleteUserButton.Enabled = true;
                DeactivateUserButton.Enabled = true;
                AddUserButton.Enabled = false;
                EditUserButton.Enabled = true;
            }
            else if (newUser.IsLoginValid(LoginBox.Text, LoginBox.Text.Length) && !usersRepository.IsLoginInDatabase(LoginBox.Text))
            {
                AddUserButton.Enabled = true;
                EditUserButton.Enabled = false;
                DeleteUserButton.Enabled = false;
                DeactivateUserButton.Enabled = false;
            }
            else
            {
                AddUserButton.Enabled = false;
                EditUserButton.Enabled = false;
                DeleteUserButton.Enabled = false;
                DeactivateUserButton.Enabled = false;
                InfoLabel.Text = "Login może się składać z 3-30 liter (nie polskich) oraz cyfr. Nie zaczynaj loginu od cyfry.";
            }
        }

        protected void ConfirmRestartClick(object sender, EventArgs e)
        {
            Response.Redirect("AccountsManagement.aspx");

        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminPanel.aspx");
        }
    }
}
