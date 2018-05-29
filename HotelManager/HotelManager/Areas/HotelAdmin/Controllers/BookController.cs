using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;

namespace HotelManager.Areas.HotelAdmin.Controllers
{
    [Authorize]/*身份认证，验证票据*/
    public class BookController : Controller
    {
        [HttpGet]
        // GET: HotelAdmin/Book
        public ActionResult BookManagers()
        {
            List<DishBook> objlist = new DishBookManager().GetAllDishBook();

            ViewBag.list = objlist;
            return View();
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="statId">订单状态参数</param>
        /// <param name="BookId">预订状态ID</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DoUpdate(string statId,string BookId)
        {
            try
            {
                //调用BLL层进行数据处理 修改订单状态
                int result = new DishBookManager().ModifyBook(BookId, statId);
                if (result>0)
                {
                    //成功
                    return Content("<script>alert('订单状态修改成功！');location.href='"+Url.Action("BookManagers","Book") +"';</script>");
                }
                else
                {
                    //失败
                    return Content("<script>alert('订单状态修改失败！');location.href='" + Url.Action("BookManagers", "Book") + "';</script>");
                }
            }
            catch (Exception ex)
            {
                return Content("<script>alert('"+ex.Message+"');location.href='" + Url.Action("BookManagers", "Book") + "';</script>");
                
            }
        }
    }
}