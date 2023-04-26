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
    public partial class CreateEmployee : System.Web.UI.Page
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
            if (TextBox1.Text != "" && TextBox2.Text != "" && TextBox4.Text != "")
            {
                InitialiseCon();
                cmd = new SqlCommand("sp_InsertEmployee", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", TextBox1.Text);
                cmd.Parameters.AddWithValue("@JDate", txtDatePicker1.Text);
                cmd.Parameters.AddWithValue("@DOB", txtDatePicker2.Text);
                cmd.Parameters.AddWithValue("@Skills", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Salary", salaryrange.Text);
                cmd.Parameters.AddWithValue("@Designation", DropDownList1.Text);
                cmd.Parameters.AddWithValue("@Email", TextBox4.Text);
                cmd.ExecuteNonQuery();
            }
            else
            {
                Response.Write("<script text='JavaScript'>alert('Fill the required fields')</script>");                
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmployeeList.aspx");
        }
    }
}