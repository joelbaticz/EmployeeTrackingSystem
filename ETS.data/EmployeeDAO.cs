using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ETS.domain;
using System.Data.SqlClient;

namespace ETS.data
{
    public class EmployeeDAO
    {
        //fields
        private SqlConnection connection;
        private static EmployeeDAO instance;

        //props
        public static EmployeeDAO Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new EmployeeDAO();
                }
                return instance;
            }
        }

        //constructors
        private EmployeeDAO()
        {
        }

        public void InsertEmployee(Employee emp)
        {
            connection = ConnectionHelper.GetConnection();

            using (connection)
            {
                SqlCommand cmd = new SqlCommand("sp_Employees_InsertEmployee", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@firstName", emp.FirstName));
                cmd.Parameters.Add(new SqlParameter("@lastName", emp.LastName));
                cmd.Parameters.Add(new SqlParameter("@email", emp.Email));
                cmd.Parameters.Add(new SqlParameter("@dob", emp.DOB));
                cmd.Parameters.Add(new SqlParameter("@phone", emp.Phone));

                cmd.ExecuteNonQuery();
            }
        }

        public List<Employee> SelectAll()
        {
            connection = ConnectionHelper.GetConnection();

            using (connection)
            {
                SqlCommand cmd = new SqlCommand("sp_Employees_SelectAllEmployees", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                List<Employee> list = new List<Employee>();

                while (reader.Read())
                {
                    Employee emp = new Employee();
                    emp.EmpID = Convert.ToInt32(reader["EmpID"]);
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.LastName = reader["LastName"].ToString();
                    emp.Email = reader["Email"].ToString();
                    emp.DOB = Convert.ToDateTime(reader["DOB"]);
                    emp.Phone = reader["Phone"].ToString();

                    list.Add(emp);
                }
                return list;
            }
        }

    }
}
