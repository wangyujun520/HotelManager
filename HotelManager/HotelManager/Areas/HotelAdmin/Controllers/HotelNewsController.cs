using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using Webdiyer.WebControls.Mvc;

namespace HotelManager.Areas.HotelAdmin.Controllers
{
    [Authorize]
    public class HotelNewsController : Controller
    {
        // GET: HotelAdmin/HotelNews
        [HttpGet]
        public ActionResult NewsPublish()
        {
            return View();
        }

        /// <summary>
        /// 新闻管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NewsManager(int? id=1)
        {
            int totalCount = 0;
            int pageIndex = id ?? 1;
            int pageSize = 6;
            PagedList<News> objlist = new NewsManager().GetNews("", pageSize, (pageIndex - 1) * 5, out totalCount).AsQueryable().ToPagedList(pageIndex, pageSize);
            objlist.TotalItemCount = totalCount;
            objlist.CurrentPageIndex = (int)(id ?? 1);
            Common.Common info = new Common.Common();
            info.objNewsModel = objlist;
            return View("NewsManager", info);
        }

        [HttpGet]
        public ActionResult NewsModify(string newsId)//映射模型
        {
            News objmodel = new NewsManager().GetNewsById(newsId);
            //将newsId保存到模型中
            objmodel.NewsId = Convert.ToInt32(newsId);
            return View("NewsModify", objmodel);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DoNews(News objModel)//映射模型数据
        {
            //数据验证（客户端验证） MVC视图模型验证
            //调用BLL层进行数据处理
            objModel.PublishTime = DateTime.Now;//设置当前时间
            int result = 0;
            if (objModel.NewsId!=0)//修改操作
            {
                result = new BLL.NewsManager().ModifyNews(objModel);

                if (result > 0)
                {
                    return Content("<script>alert('新闻修改成功！');location.href='" + Url.Action("NewsManager") + "';</script>");
                }
                else
                {
                    return Content("<script>alert('新闻修改失败！');location.href='" + Url.Action("NewsModify") + "';</script>");
                }
            }
            else//新增操作
            {
                result = new BLL.NewsManager().PublishNews(objModel);
                if (result > 0)
                {
                    return Content("<script>alert('新闻发布成功！');location.href='" + Url.Action("NewsPublish") + "';</script>");
                }
                else
                {
                    return Content("<script>alert('新闻发布失败！');location.href='" + Url.Action("NewsPublish") + "';</script>");
                }

            }
        }

        [HttpGet]
        public ActionResult DoDelNew(string newsId)//映射数据模型
        {
            //调用数据模型
            int result = new BLL.NewsManager().DelNews(newsId);
            if (result>0)//删除成功
            {
                return Content("<script>alert('删除成功！');location.herf='"+Url.Action("NewsManager") +"';</script>");
            }
            else//删除失败
            {
                return Content("<script>alert('删除失败！');location.herf='" + Url.Action("NewsManager") + "';</script>");
            }
        }


    }
}