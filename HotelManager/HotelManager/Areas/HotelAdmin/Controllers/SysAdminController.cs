using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using System.Web.Security;//安全命名空间

namespace HotelManager.Areas.HotelAdmin.Controllers
{
    public class SysAdminController : Controller
    {
        // GET: HotelAdmin/SysAdmin
        [HttpGet]
        public ActionResult Index()
        {
            return View("AdminLogin");
        }

        [Authorize]//身份认证
        public ActionResult AdminMain()
        {
            string name = User.Identity.Name;
            return View();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult ExtSys()
        {
            Session["currentAdmin"] = null;
            Session.Abandon();
            FormsAuthentication.SignOut();
            return View("AdminLogin");
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="objSys"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoginUser(SysAdmins objAdmin)
        {
            //获取参数数据验证
            if (ModelState.IsValid)
            {
                //调用BLL业务逻辑层登录方法
                objAdmin = new SysAdminManager().AdminLogin(objAdmin);
                //if(登录成功)  1.存值到session 给2.当前登录用户发放票据 3.返回主页（）
                if (objAdmin != null)
                {
                    Session["currentAdmin"] = objAdmin.LoginName;
                    //发票据
                    FormsAuthentication.SetAuthCookie(objAdmin.LoginName, true);
                    return View("AdminMain");
                }
                else
                {
                    return Content("<script>alert('用户名或密码错误!');location.href='" + Url.Action("Index") + "'</script>");
                }
            }
            else
            {
                return View("AdminLogin");
            }
        }
        
        /// <summary>
        /// 跳转到投诉管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SuggestionList()
        {
            List<Suggestion> objlist = new SuggestionManager().GetSuggestion();
            //ViewBag.list = objlist;
            return View("SuggestionList", objlist);
        }

        /// <summary>
        /// 受理投诉
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DoSugg(string suggId)
        {
            //调用模型BLL层进行数据处理
            int result = new SuggestionManager().HandlSuggestion(suggId);
            if (result>0)
            {
                return Content("<script>alert('投诉受理成功！');location.href='"+Url.Action("SuggestionList") +"';</script>");
            }
            else
            {
                return Content("<script>alert('投诉受理失败！');location.href='" + Url.Action("SuggestionList") + "';</script>");
            }
        }


    }
}