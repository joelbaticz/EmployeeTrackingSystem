using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ETS.domain;
using ETS.data;

namespace ETS.logic
{
    public class EmployeeHourManager
    {
        //fields
        private readonly EmpHourDAO dao;

        //cons
        public EmployeeHourManager()
        {
            dao = EmpHourDAO.Instance;
        }

        //methods
        public ResultEnum InsertEmployeeHour(EmpHour empHour)
        {
            ResultEnum result = ResultEnum.Success;

            try
            {
                dao.InsertEmpHour(empHour);
            }
            catch (Exception e)
            {
                result = ResultEnum.Fail;
                Console.WriteLine("Exception occured " + e.Message);
            }
            return result;
        }

        public Result<List<EmpHour>> SelectAllEmpHour()
        {
            Result<List<EmpHour>> result = new Result<List<EmpHour>>();

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

        public Result<List<EmpHour>> SelectEmpHourByEmpId(int empId)
        {
            Result<List<EmpHour>> result = new Result<List<EmpHour>>();

            try
            {
                result.ResultData = dao.SelectHoursByEmpId(empId);
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
