using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CrudWEF2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CrudWEF2.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            List<Teacher> teacherList = new List<Teacher>();
            string conn = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
            using (SqlConnection connection = new SqlConnection(conn))
            {
                //SQL Data Reader
                connection.Open();
                string sql = "Select * From Teacher";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Teacher teacher = new Teacher();
                        teacher.Id = Convert.ToInt32(dataReader["Id"]);
                        teacher.Name = Convert.ToString(dataReader["Name"]);
                        teacher.Skills = Convert.ToString(dataReader["Skills"]);
                        teacher.TotalStudents = Convert.ToInt32(dataReader["TotalStudents"]);
                        teacher.Salary = Convert.ToDecimal(dataReader["Salary"]);
                        teacher.AddeOn = Convert.ToDateTime(dataReader["AddeOn"]);
                        teacherList.Add(teacher);
                    }
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

            }
            return View(teacherList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                //string connectionString = Configuration["ConnectionString:DefaultConnection"];
                string conn = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");

                using (SqlConnection connection = new SqlConnection(conn))
                {
                    string sql = $"Insert Into Teacher(Name, Skills, TotalStudents, Salary) Values ('{teacher.Name}', '{teacher.Skills}', '{teacher.TotalStudents}', '{teacher.Salary}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }

            }
            else
                return View();
        }

        public IActionResult Update(int id)
        {
            string conn = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
            Teacher teacher = new Teacher();
            using (SqlConnection connection = new SqlConnection(conn)) 
            {
                string sql = $"Select * From Teacher Where Id = '{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        teacher.Id = Convert.ToInt32(dataReader["Id"]);
                        teacher.Name = Convert.ToString(dataReader["Name"]);
                        teacher.Skills = Convert.ToString(dataReader["Skills"]);
                        teacher.TotalStudents = Convert.ToInt32(dataReader["TotalStudents"]);
                        teacher.Salary = Convert.ToDecimal(dataReader["Salary"]);
                        teacher.AddeOn = Convert.ToDateTime(dataReader["AddeOn"]);
                    }
                }
                if(connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }

            }
            return View(teacher);
        }
        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update(Teacher teacher)
        {
            string conn = Microsoft.Extensions.Configuration.ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
            using(SqlConnection connection = new SqlConnection(conn))
            {
                string sql = $"Update Teacher SET Name='{teacher.Name}', Skills='{teacher.Skills}', TotalStudents='{teacher.TotalStudents}', Salary='{teacher.Salary}' WHERE Id='{teacher.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
