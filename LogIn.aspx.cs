using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Text.RegularExpressions;


public partial class LogIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    



    protected void login_bttn_Click(object sender, EventArgs e)
    {
        /*
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from [Table] where Username =@username and Password=@password", con);
        cmd.Parameters.AddWithValue("@username", uname.Text);
        cmd.Parameters.AddWithValue("@password", pwd.Text);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)

        {

            Response.Redirect("AuthenticatedSites/Success.aspx");

        }

        else

        {

            loginfail_lbl.Visible = true;
            loginfail_lbl.Text = "Kullanıcı Adı veya Şifre Hatalı!";
        }  */
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool authenticated = this.ValidateCredentials(Login1.UserName, Login1.Password);

        if (authenticated)
        {
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, Login1.RememberMeSet);
        }
    }

    public bool IsAlphaNumeric(string text)
    {
        return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
    }

    private bool ValidateCredentials(string userName, string password)
    {
        bool returnValue = false;

        if (this.IsAlphaNumeric(userName) && userName.Length <= 50 && password.Length <= 50)
        {
            SqlConnection conn = null;

            try
            {
                string sql = "select count(*) from [Table] where Username=@username and Password=@password";

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["AuthenticatedSitesConStr"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter user = new SqlParameter();
                user.ParameterName = "@username";
                user.Value = userName.Trim();
                cmd.Parameters.Add(user);

                SqlParameter pass = new SqlParameter();
                pass.ParameterName = "@password";
                pass.Value = password.Trim();
                cmd.Parameters.Add(pass);

                conn.Open();

                int count = (int)cmd.ExecuteScalar();

                if (count > 0) returnValue = true;
            }
            catch (Exception ex)
            {
                // Log your error
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        else
        {
            // Log error - user name not alpha-numeric or 
            // username or password exceed the length limit!
        }

        return returnValue;
    }


}