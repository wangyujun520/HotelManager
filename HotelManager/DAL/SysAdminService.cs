using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models;
using Common;

namespace DAL
{
    public class SysAdminService
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="objAdmin"></param>
        /// <returns></returns>
        public SysAdmins AdminLogin(SysAdmins objAdmin)
        {
            string sql = "select LoginName from SysAdmins where LoginId=@LoginId and LoginPwd=@LoginPwd";

            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@LoginId",objAdmin.LoginId),
                new SqlParameter("@LoginPwd",objAdmin.LoginPwd)
            };
            try
            {
                SqlDataReader objReader = SQLHelper.GetReader(sql, parameters);
                if (objReader.Read())
                {
                    objAdmin = new SysAdmins()
                    {
                        LoginName = objReader["LoginName"].ToString()
                    };
                }
                else
                {
                    objAdmin = null;
                }
                objReader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objAdmin;
        }
    }
}