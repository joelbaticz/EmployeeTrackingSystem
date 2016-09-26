using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using ETS.domain;

namespace ETS.data
{
    public class EmpHourDAO
    {
        //fields
        private SqlConnection connection;
        private static EmpHourDAO instance;

        //props
        public static EmpHourDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmpHourDAO();
                }
                return instance;
            }
        }

        //constructors
        private EmpHourDAO()
        {
        }

        //methods
        public void InsertEmpHour(EmpHour empHour)
        {
            connection = ConnectionHelper.GetConnection();

            using (connection)
            {
                SqlCommand cmd = new SqlCommand("sp_EmpHours_InsertHours", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empId", empHour.EmpID));
                cmd.Parameters.Add(new SqlParameter("@workdate", empHour.WorkDate));
                cmd.Parameters.Add(new SqlParameter("@hours", empHour.Hours));

                cmd.ExecuteNonQuery();
            }
        }

        public List<EmpHour> SelectAll()
        {
            connection = ConnectionHelper.GetConnection();

            using (connection)
            {
                SqlCommand cmd = new SqlCommand("sp_EmpHours_SelectAllHours", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                List<EmpHour> list = new List<EmpHour>();

                while (reader.Read())
                {
                    EmpHour empHour = new EmpHour();

                    empHour.EmpHourID = Convert.ToInt32(reader["EmpHourID"]);
                    empHour.EmpID = Convert.ToInt32(reader["EmpID"]);
                    empHour.WorkDate = Convert.ToDateTime(reader["WorkDate"]);
                    empHour.Hours= Convert.ToInt32(reader["Hours"]);

                    list.Add(empHour);
                }
                return list;
            }
        }

        public List<EmpHour> SelectHoursByEmpId(int empId)
        {
            connection = ConnectionHelper.GetConnection();

            using (connection)
            {
                SqlCommand cmd = new SqlCommand("sp_EmpHours_SelectHoursByEmpId", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@empId", empId));

                SqlDataReader reader = cmd.ExecuteReader();

                List<EmpHour> list = new List<EmpHour>();

                while (reader.Read())
                {
                    EmpHour empHour = new EmpHour();

                    empHour.EmpHourID = Convert.ToInt32(reader["EmpHourID"]);
                    empHour.EmpID = Convert.ToInt32(reader["EmpID"]);
                    empHour.WorkDate = Convert.ToDateTime(reader["WorkDate"]);
                    empHour.Hours = Convert.ToInt32(reader["Hours"]);

                    list.Add(empHour);
                }
                return list;
            }
        }

    }
}
