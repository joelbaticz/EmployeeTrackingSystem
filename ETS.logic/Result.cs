using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETS.logic
{
    public class Result<T> where T:class
    {
        public ResultEnum ResultStatus { get; set; }

        public T ResultData { get; set; }

    }
}
