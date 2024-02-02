using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.ApiResult
{
    public class Result
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public dynamic Data { get; set; }
        public int Total { get; set; }
    }
}
