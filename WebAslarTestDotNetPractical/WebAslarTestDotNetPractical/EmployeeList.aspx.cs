using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SqlServer.Server;
using System.Runtime.InteropServices.ComTypes;
using System.Xml.Linq;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace WebAslarTestDotNetPractical
{
    public partial class EmployeeList : System.Web.UI.Page
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getAllEmployees();
            }
        }

        public void InitialiseCon()
        {
            string s = ConfigurationManager.AppSettings["employee_db"];
            cn = new SqlConnection(s);
            cn.Open();
        }

        public void getAllEmployees()
        {
            InitialiseCon();
            cmd = new SqlCommand("sp_DisplayAllEmployees", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();
            GridView1.DataSource = dr;
            GridView1.DataBind();
        }

        protected void edit_Employee(object sender, EventArgs e)
        {
            GridViewRow gr1 = (((LinkButton)sender).NamingContainer as GridViewRow);
            LinkButton l1 = (LinkButton)gr1.FindControl("edit_link");
            TextBox t1 = (TextBox)gr1.FindControl("IdText");
            TextBox t2 = (TextBox)gr1.FindControl("NameText");
            TextBox t3 = (TextBox)gr1.FindControl("JdateText");
            TextBox t4 = (TextBox)gr1.FindControl("DOBText");
            TextBox t5 = (TextBox)gr1.FindControl("SkillsText");
            TextBox t6 = (TextBox)gr1.FindControl("SalaryText");
            DropDownList t7 = (DropDownList)gr1.FindControl("DesignationText");
            TextBox t8 = (TextBox)gr1.FindControl("EmailText");
            t2.ReadOnly = false;
            t3.ReadOnly = false;
            t4.ReadOnly = false;
            t5.ReadOnly = false;
            t6.ReadOnly = false;
            t7.Enabled= true;
            t8.ReadOnly = false;
            t2.Focus();
        }

        protected void update_Employee(object sender, EventArgs e)
        {
            InitialiseCon();
            GridViewRow gr1 = (((LinkButton)sender).NamingContainer as GridViewRow);
            LinkButton l1 = (LinkButton)gr1.FindControl("update_link");
            TextBox t1 = (TextBox)gr1.FindControl("IdText");
            TextBox t2 = (TextBox)gr1.FindControl("NameText");
            TextBox t3 = (TextBox)gr1.FindControl("JdateText");
            TextBox t4 = (TextBox)gr1.FindControl("DOBText");
            TextBox t5 = (TextBox)gr1.FindControl("SkillsText");
            TextBox t6 = (TextBox)gr1.FindControl("SalaryText");
            DropDownList t7 = (DropDownList)gr1.FindControl("DesignationText");
            TextBox t8 = (TextBox)gr1.FindControl("EmailText");
            cmd = new SqlCommand("sp_UpdateEmployee", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", t1.Text);
            cmd.Parameters.AddWithValue("@Name", t2.Text);
            cmd.Parameters.AddWithValue("@JDate", t3.Text);
            cmd.Parameters.AddWithValue("@DOB", t4.Text);
            cmd.Parameters.AddWithValue("@Skills", t5.Text);
            cmd.Parameters.AddWithValue("@Salary", t6.Text);
            cmd.Parameters.AddWithValue("@Designation", t7.Text);
            cmd.Parameters.AddWithValue("@Email", t8.Text);
            cmd.ExecuteNonQuery();
            Response.Write("<script text='JavaScript'>alert('Data Updated')</script>");
            getAllEmployees();
        }

        protected void del_Employee(object sender, EventArgs e)
        {
            InitialiseCon();
            GridViewRow gr1 = (((LinkButton)sender).NamingContainer as GridViewRow);
            LinkButton l2 = (LinkButton)gr1.FindControl("del_link");
            TextBox t1 = (TextBox)gr1.FindControl("IdText");
            cmd = new SqlCommand("sp_DeleteEmployee", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", t1.Text);
            cmd.ExecuteNonQuery();
            Response.Write("<script text='JavaScript'>alert('Data Deleted')</script>");
            getAllEmployees();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<EmployeeData> dataList = new List<EmployeeData>();
            InitialiseCon();
            cmd = new SqlCommand("sp_DisplayAllEmployees", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            { 
                int Id = Convert.ToInt32(dr["Id"]);
                string Name = dr["Name"].ToString();
                DateTime JDate = (DateTime)dr["JDate"];
                DateTime DOB = (DateTime)dr["DOB"];
                string Skills = dr["Skills"].ToString();
                decimal Salary = (decimal)dr["Salary"];
                string Designation = dr["Designation"].ToString();
                string Email = dr["Email"].ToString();
                EmployeeData addEmployee = new EmployeeData ( Id, Name, JDate, DOB, Skills, Salary, Designation, Email );
                dataList.Add(addEmployee);
            }
            dr.Close();
            cn.Close();
            var EmployeeTableData = dataList.AsEnumerable().OrderBy(x => x.JDate).ThenBy(x=>x.Skills);
            ExportEmployeeData(EmployeeTableData);
        }

        public void ExportEmployeeData(IEnumerable<EmployeeData> EmployeeTableData)
        {
            var propertiesToInclude = new List<string> { "Id", "Name", "JDate", "DOB", "Skills", "Salary", "Designation", "Email" };
            var bytes = ExportHelper.GenerateWorksheet(EmployeeTableData, propertiesToInclude);
            var stream = new MemoryStream(bytes);
            stream.Position = 0;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "Employee.xlsx";
            Response.Clear();
            Response.ContentType = contentType;
            Response.Headers.Add("content-disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.End();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string filePath = Server.MapPath("~/Uploads/" + FileUpload1.FileName);

                FileUpload1.SaveAs(filePath);

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    List<string> uniqueIdentifiers = new List<string>();
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int startRow = 2;
                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;
                    InitialiseCon();
                    for (int row = startRow; row <= rowCount; row++)
                    {
                        string Name = worksheet.Cells[row, 2].Value.ToString();
                        string JDateString = worksheet.Cells[row, 3].Value.ToString();
                        DateTime JDate = DateTime.Parse(JDateString);
                        string DOBString = worksheet.Cells[row, 4].Value.ToString();
                        DateTime DOB = DateTime.Parse(DOBString);
                        string Skills = worksheet.Cells[row, 5].Value.ToString();
                        string Salary = worksheet.Cells[row, 6].Value.ToString();
                        string Designation = worksheet.Cells[row, 7].Value.ToString();
                        string Email = worksheet.Cells[row, 8].Value.ToString();
                        if (uniqueIdentifiers.Contains(Email) && uniqueIdentifiers.Contains(Name))
                        {
                            //Response.Write("<script text='JavaScript'>alert('Skipped')</script>");
                            continue;
                        }
                        uniqueIdentifiers.Add(Name);
                        uniqueIdentifiers.Add(Email);
                        cmd = new SqlCommand("sp_InsertEmployee", cn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", Name);
                        cmd.Parameters.AddWithValue("@JDate", JDate);
                        cmd.Parameters.AddWithValue("@DOB", DOB);
                        cmd.Parameters.AddWithValue("@Skills", Skills);
                        cmd.Parameters.AddWithValue("@Salary", Salary);
                        cmd.Parameters.AddWithValue("@Designation", Designation);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.ExecuteNonQuery();
                    }
                }
                getAllEmployees();
            }
            else
            {
                Response.Write("<script text='JavaScript'>alert('Upload file to import')</script>");
            }
        }
    }
}