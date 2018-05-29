using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Serializable]
    public class SysAdmins
    {
        [DisplayName("登录名")]
        [Required(ErrorMessage ="{0}不能为空")]
        public int LoginId { get; set; }

        [DisplayName("密码")]
        [Required(ErrorMessage ="{0}不能为空")]
        public string LoginPwd { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string LoginName { get; set; }

    }
}
