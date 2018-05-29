using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace HotelManager.Areas.HotelAdmin.Controllers
{
    [Authorize]
    public class RecruitmentController : Controller
    {
        // GET: HotelAdmin/Recruitment
        //跳转到发布招聘页面
        [HttpGet]
        public ActionResult RecruitmentPublish()
        {
            return View();
        }
        /// <summary>
        /// 发布招聘和修改招聘的实现
        /// </summary>
        /// <param name="objRec"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoRecruitment(Recruitment objRec)//映射数据模型
        {
            //验证非空！！自己补充

            //调用BLL
            objRec.PublishTime = DateTime.Now;//设置当前时间
            int result = 0;
            //判断用户
            if (objRec.PostId!=0)//修改操作
            {
                result = new BLL.RecruitmentManager().ModifyRecruiment(objRec);
                if (result > 0)//修改成功
                {
                    return Content("<script>alert('修改招聘成功！');location.href='" + Url.Action("RecruitmentManagers") + "';</script>");
                }
                else//修改失败
                {
                    return Content("<script>alert('修改招聘失败！');location.href='" + Url.Action("RecruitmentManagers") + "';</script>");
                }
            }
            else//新增操作
            {
                result = new BLL.RecruitmentManager().AddRecruitment(objRec);
                if (result > 0)//发布成功
                {
                    return Content("<script>alert('发布招聘成功！');location.href='" + Url.Action("RecruitmentPublish") + "';</script>");
                }
                else//发布失败
                {
                    return Content("<script>alert('发布招聘失败！');location.href='" + Url.Action("RecruitmentPublish") + "';</script>");
                }
            }
            
        }
        /// <summary>
        /// 跳转到招聘管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RecruitmentManagers()
        {
            //查询数据方法
            List<Recruitment> objList = new RecruitmentManager().GetAllRecList();
            //保存数据
            ViewBag.list = objList;
            //返回视图
            return View();
        }

        /// <summary>
        /// 跳转到修改视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModifyRecruit(string postId)
        {
            //根据ID查询出当条数据
            Recruitment objModel = new RecruitmentManager().GetPostById(postId);

            //返回视图，将强类型模型传递到视图
            return View("ModifyRecruit", objModel);
        }

        /// <summary>
        /// 根据ID进行删除
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DelPost(string postId)
        {
            //调用BLL层业务逻辑的删除方法
            int resutl = new BLL.RecruitmentManager().DeleteRecruiment(postId);
            if (resutl>0)//删除成功
            {
                return Content("<script>alert('删除成功！');location.href='"+Url.Action("RecruitmentManagers") +"';</script>");
            }
            else//删除失败
            {
                return Content("<script>alert('删除失败！');location.href='" + Url.Action("RecruitmentManagers") + "';</script>");
            }
        }


    }
}