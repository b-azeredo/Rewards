using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using Microsoft.Ajax.Utilities;
using Rewards.Manager;
using Rewards.DBModel;

namespace Rewards
{
    public partial class LoginPage : System.Web.UI.Page
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtUsername.Text) && !String.IsNullOrEmpty(txtPassword.Text))
                {
                    string username = txtUsername.Text;
                    string password = txtPassword.Text;
                    if (IsAuthenticated(username, password))
                    {
                        using (var entities = new Entities2())
                        {
                            var user = entities.USER.FirstOrDefault(u => u.NAME == username && u.ACTIVATED == true);
                            if (user != null)
                            {
                                Session["User"] = user;
                                int USER_ID = user.ID;
                                string role = UserManager.Get_Role(USER_ID);
                                if (role == "EMPLOYEE")
                                {
                                    Response.Redirect("~/Default.aspx");

                                }
                                else if (role == "MANAGER")
                                {
                                    Response.Redirect("~/DefaultManager.aspx");

                                }
                                else
                                {
                                    Response.Redirect("~/DefaultAdmin.aspx");
                                }
                            }
                            else
                            {
                                string script = "messageAlert('Try to register before logging in.');";
                                ClientScript.RegisterStartupScript(this.GetType(), "ValidationAlert", script, true);
                            }
                        }
                    }
                    else
                    {
                        string script = "messageAlert('Username or password incorrect.');";
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