using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ETS.domain;
using ETS.data;

namespace ETS.logic
{
    public class EmployeeManager
    {
        //fields
        private readonly EmployeeDAO dao;

        //cons
        public EmployeeManager()
        {
            dao = EmployeeDAO.Instance;
        }

        //methods
        public ResultEnum InsertEmployee(Employee emp)
        {
            ResultEnum result = ResultEnum.Success;

            try
            {
                dao.InsertEmployee(emp);
            }
            catch(Exception e)
            {
                result = ResultEnum.Fail;
                Console.WriteLine("Exception occured " + e.Message);
            }
            return result;
        }

        public Result<List<Employee>> SelectAllEmployee()
        {
            Result<List<Employee>> result = new Result<List<Employee>>();

            try
            {
                result.ResultData = dao.SelectAll();
                result.ResultStatus = ResultEnum.Success;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured" + e.Message);
                result.ResultStatus = ResultEnum.Fail;
            }
            return result;
        }

    }
}
