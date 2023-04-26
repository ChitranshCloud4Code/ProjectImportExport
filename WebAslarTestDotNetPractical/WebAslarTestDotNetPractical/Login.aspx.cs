using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAslarTestDotNetPractical
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        public void InitialiseCon()
        {
            string s = ConfigurationManager.AppSettings["employee_db"];
            cn = new SqlConnection(s);
            cn.Open();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text != "" && TextBox1.Text != "")
            {
                InitialiseCon();
                cmd = new SqlCommand("sp_loginCredentials", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", TextBox1.Text);
                cmd.Parameters.AddWithValue("@UserPassword", TextBox2.Text);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    bool isHuman = ExampleCaptcha.Validate(CaptchaCodeTextBox.Text);

                    CaptchaCodeTextBox.Text = null; // clear previous user input

                    if (isHuman)
                    {
                        Response.Redirect("CreateEmployee.aspx");
                    }
                    else
                    {
                        Response.Write("<script text='JavaScript'>alert('Invalid Captcha')</script>");
                    }
                }
                else
                {
                    Response.Write("<script text='JavaScript'>alert('Invalid Credentials')</script>");
                }
            }
            else
            {
                Response.Write("<script text='JavaScript'>alert('Fill the fields')</script>");
            }
        }
    }
}