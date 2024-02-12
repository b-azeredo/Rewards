using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using Microsoft.Ajax.Utilities;
using Rewards.Manager;
using Rewards.DBModel;
using System.Configuration;
using System;

namespace Rewards
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        private static string EscapeLdapSearchFilter(string searchFilter)
        {
            StringBuilder escape = new StringBuilder();
            for (int i = 0; i < searchFilter.Length; ++i)
            {
                char current = searchFilter[i];
                switch (current)
                {
                    case '\\':
                        escape.Append(@"\5c");
                        break;
                    case '*':
                        escape.Append(@"\2a");
                        break;
                    case '(':
                        escape.Append(@"\28");
                        break;
                    case ')':
                        escape.Append(@"\29");
                        break;
                    case '\u0000':
                        escape.Append(@"\00");
                        break;
                    case '/':
                        escape.Append(@"\2f");
                        break;
                    default:
                        escape.Append(current);
                        break;
                }
            }
            return escape.ToString();
        }

        public static bool IsAuthenticated(string usr, string pwd)
        {
            bool authenticated = false;

            try
            {
                DirectoryEntry entry = new DirectoryEntry(ConfigurationManager.AppSettings["LDAP_HOST"], usr, pwd);
                DirectorySearcher search = new DirectorySearcher(entry)
                {
                    PageSize = int.MaxValue,
                    Filter = "(&(objectCategory=person)(objectClass=user)(sAMAccountName=" + EscapeLdapSearchFilter(usr) + "))"
                };
                search.PropertiesToLoad.Add("c");
                SearchResult result = search.FindOne();

                if (result == null || (result != null && result.Properties["c"][0] != null && result.Properties["c"][0].ToString() != "PT"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return authenticated;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(username.Text) && !String.IsNullOrEmpty(password.Text) && !String.IsNullOrEmpty(email.Text) && !String.IsNullOrEmpty(bossEmail.Text))
                {
                    string Username = username.Text;
                    string Password = password.Text;
                    if (IsAuthenticated(Username, Password))
                    {
                        using (var entities = new Entities2())
                        {
                            var user = entities.USER.FirstOrDefault(u => u.NAME == Username && u.ACTIVATED == true);
                            if (user != null)
                            {
                                string script = "messageAlert('There is an already exist account with this user info.');";
                                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                            }
                            else
                            {
                                USER NewUser = new USER
                                {
                                    NAME = Username,                                    
                                    EMAIL = email.Text,
                                    MANAGER_EMAIL = bossEmail.Text,
                                    ROLE = "EMPLOYEE",
                                    ACTIVATED = true 
                                };
                                entities.USER.Add(NewUser);
                                entities.SaveChanges();
                                string script = "messageAlert('Account registred with sucess!.');";
                                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                                ClientScript.RegisterStartupScript(this.GetType(), "RedirectToLoginPage", "setTimeout(function() { window.location.href = 'LoginPage.aspx'; }, 2000);", true);

                            }
                        }
                    }
                    else
                    {
                        string script = "messageAlert('The user doesn\\'t belong to the company!.');";
                        ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                    }
                }
                else
                {
                    string script = "messageAlert('Please fill all the fields.');";
                    ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                }
            }
            catch
            {

            }
        }

    }
}