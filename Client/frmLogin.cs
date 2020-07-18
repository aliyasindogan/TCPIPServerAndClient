using Client.Models;
using DataAccess;
using Entities.Abstract.Enum;
using Entities.Concrete.Entity;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Client
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (ChatDbContext db = new ChatDbContext())
            {
                var result = db.UserTypes.FirstOrDefault(x => x.Id == (int)UserTypes.Client);
                if (result == null)
                {
                    db.UserTypes.Add(new UserType()
                    {
                        UserTypeName = "Server"
                    });
                    db.SaveChanges();

                    db.UserTypes.Add(new UserType()
                    {
                        UserTypeName = "Client"
                    });
                    db.SaveChanges();
                }

                var resultUser = db.Users.FirstOrDefault(x => x.UserName == txtUserName.Text.ToLower().Trim() || x.Password == txtPassword.Text);
                if (resultUser == null)
                {
                    var addUser = db.Users.Add(new User()
                    {
                        UserName = txtUserName.Text.ToLower().Trim(),
                        Password = txtPassword.Text,
                        UserTypeID = (int)UserTypes.Client,
                    });
                    db.SaveChanges();
                }
                else
                {
                    LoginViewModel.UserName = resultUser.UserName;
                    LoginViewModel.UserID = resultUser.Id;
                    this.Hide();
                    frmClient frmClient = new frmClient();
                    frmClient.Show();
                }
            }
        }
    }
}