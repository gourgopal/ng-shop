using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class User
    {
        public int? user_key { get; set; }
        public string name { get; set; }
        public char? type { get; set; }
        public string email { get; set; }
        public DateTime dob { get; set; }
        public string password { get; set; }
    }
    public class UserLayer
    {
        public static bool CheckEmail(User data)
        {
            var query = $"SELECT TOP 1 user_key FROM shop.users WHERE email = '{data.email}'";
            var res = Convert.ToInt16(SQLConnect.GetScalar(query));
            return res > 0 ? true : false;
        }
        public static bool CheckUsername(User data)
        {
            var query = $"SELECT TOP 1 user_key FROM shop.users WHERE name = '{data.name}'";
            var res = Convert.ToInt16(SQLConnect.GetScalar(query));
            return res > 0 ? true : false;
        }
        public static int InsertUser(User data)
        {
            var UserParameters = new List<SqlParameter>();
            var objSqlParameter = new SqlParameter("@user_key", data.user_key)
            {
                Direction = ParameterDirection.Output,
                Size = 8
            };
            UserParameters.Add(objSqlParameter); //UserParameters.Add(new SqlParameter("@user_key", data.user_key));
            UserParameters.Add(new SqlParameter("@name", data.name));
            UserParameters.Add(new SqlParameter("@type", data.type));
            UserParameters.Add(new SqlParameter("@email", data.email));
            UserParameters.Add(new SqlParameter("@dob", data.dob));
            UserParameters.Add(new SqlParameter("@password", data.password));
            var res = SQLConnect.ExecuteNonQuery("shop.INSERT_USER", UserParameters, CommandType.StoredProcedure);
            return Convert.ToInt32(objSqlParameter.Value);
        }
        public static int SignInUser(User data)
        {
            var query = $"SELECT TOP 1 user_key FROM shop.users WHERE name = '{data.name}' AND password = '{data.password}'";
            return Convert.ToInt32(SQLConnect.GetScalar(query));
        }
        public static int GetUserType(int? UserKey)
        {
            var query = $"SELECT TOP 1 type FROM shop.users WHERE user_key = '{UserKey}'";
            return Convert.ToInt32(SQLConnect.GetScalar(query));
        }
    }
}