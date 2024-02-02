using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.ApiResult
{
    public static class ResultHelper
    {
        public static Result Success(dynamic data, string message, int total)
        {
            return new Result
            {
                Code = 200,
                Data = data,
                Msg = message,
                Total = total
            };
        }

        public static Result Success()
        {
            return Success(null, "Success",0);
        }
        public static Result Success(string msg)
        {
            return Success(null, msg,0);
        }

        public static Result Success(dynamic data,int total)
        {
            return Success(data, "Success",total);
        }

        public static Result Success(dynamic data)
        {
            return Success(data, "Success", 0);
        }

        public static Result Error(dynamic data,string msg, int total )
        {
            return new Result
            {
                Code = 500,
                Data = data,
                Msg = msg,
                Total = total
            };
        }

        public static Result Error(string msg)
        {
            return Error(null, msg, 0);
        }

        public static Result Error()
        {
            return Error(null, "Error", 0);
        }

        public static Result NotFound()
        {
            return new Result
            {
                Code = 404,
                Data = null,
                Msg = "Not Found",
                Total = 0,
            };
        }

        public static Result Unauthorized(string message = "Login failed")
        {
            return new Result
            {
                Code = 403,
                Data = null,
                Msg = message,
                Total = 0,
            };
        }

    }
}
