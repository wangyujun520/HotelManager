using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace HotelManager.Controllers
{
    public class CompanyInfoController : Controller
    {
        // GET: CompanyInfo
        [HttpGet]
        public ActionResult Index()
        {
            List<News> newsList = new NewsManager().GetNews(5);//让页面显示五条数据
            ViewBag.list = newsList;//将数据保存到ViewBag中
            return View();
        }

        [HttpGet]
        public ActionResult AboutUs()
        {
            return View();
        }

        /// <summary>
        /// 跳转到招聘列表视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZhaoPinList()
        {
            return View();
        }


        /// <summary>
        /// 跳转到招聘详情视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ZhaoPinDetail(string PostId)
        {
            //调用BLL业务逻辑方法，根据ID查看详情
            Recruitment objRec = new BLL.RecruitmentManager().GetPostById(PostId);
            //返回视图，将模型数据传递到视图中
            return View("ZhaoPinDetail", objRec);
        }
        /// <summary>
        /// 跳转到投诉建议视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Suggestions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoSugg(Suggestion objSug,string ValidateCode)
        {
            if (string.Compare(Session["ValidateCode"].ToString(),ValidateCode,true)!=0)
            {
                ModelState.AddModelError("yzm", "验证码不正确，请重新输入");
                return View("Suggestions");
            }
            else
            {
                //调用BLL数据处理
                int result = new SuggestionManager().SubmitSuggestion(objSug);
                if (result > 0)
                {
                    return Content("添加成功！");
                }
                else
                {
                    return Content("添加失败！");
                }
            }
        }

        /// <summary>
        /// 跳转到公司介绍页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ComDesc()
        {
            return View();
        }

        /// <summary>
        /// 跳转到加盟连锁页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult JoinUs()
        {
            return View();
        }
        

    }
}