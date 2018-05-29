using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace HotelManager.Controllers
{
    public class CompanyNewsController : Controller
    {
        // GET: CompanyNews
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewsDetail(string newsId)
        {
            //根据Id查询详情
            News objModel = new NewsManager().GetNewsById(newsId);
            //返回视图
            return View("NewsDetail", objModel);
        }

        [HttpGet]
        public ActionResult NewsList()
        {
            return View();
        }
    }
}