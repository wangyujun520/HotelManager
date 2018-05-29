using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SysAdminManager
    {
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="objAdmin"></param>
        /// <returns></returns>
        public SysAdmins AdminLogin(SysAdmins objAdmin)
        {
            return new DAL.SysAdminService().AdminLogin(objAdmin);
        }

    }
}