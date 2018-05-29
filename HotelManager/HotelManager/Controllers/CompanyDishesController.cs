using HotelManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
using Webdiyer.WebControls.Mvc;

namespace HotelManager.Controllers
{
    public class CompanyDishesController : Controller
    {
        /// <summary>
        /// 跳转到在线预订页面
        /// </summary>
        /// <returns></returns>
        // GET: CompanyDishes
        [HttpGet]
        public ActionResult DishesBooks()
        {
            return View();
        }

        [HttpGet]
        public ActionResult VerificationCode()
        {
            //【1】生成4位验证码随机字符串
            VerificationCode vCode = new VerificationCode();
            string code = vCode.CreateValidateCode(4);//获取4位数的随机数

            //【2】保存到TempDate里面（因为TempDate默认的机制是Session），TempDate只能取一次值
            //TempData["ValidateCode"] = code;
            Session["ValidateCode"] = code;
            //byte[] bytes = vCode.CreateValidateGraphic(code);
            //return File(bytes, @"image/jpeg");
            //【3】返回验证码图片
            return File(vCode.CreateValidateGraphic(code), "image/jpeg");
        }

        /// <summary>
        ///验证码判断 
        /// </summary>
        /// <returns></returns>
        public ActionResult ExsitsValidate()
        {
            string txtValidateCode = Request["value"];
            if (String.Compare(Session["ValidateCode"].ToString(), txtValidateCode, true) != 0)
            {
                return Content("0");  //0代表验证码不正确
            }
            else
            {
                return Content("1"); //1代表正确
            }
        }
        /// <summary>
        /// 在线预订，向数据库插入一条数据
        /// </summary>
        /// <param name="objDish"></param>
        /// <returns></returns>
        public ActionResult DoAdd(DishBook objDish)
        {
            //调用BLL层新增数据
            objDish.BookTime = DateTime.Now;//设置当前时间
            int result = new BLL.DishBookManager().Book(objDish);
            if (result>0)//成功
            {
                //return Content("<script>alert('新增成功！');location.href='"+Url.Action("Index", "CompanyInfo") +"';</script>");
                return Content("success");
            }
            else//失败
            {
                //return Content("<script>alert('新增失败！');location.href='" + Url.Action("DishesBooks", "CompanyDishes") + "';</script>");
                return Content("error");
            }
        }        

        /// <summary>
        /// 美食展示分页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DishesShow(int? id = 1)
        {
            int totalCount = 0;
            int pageIndex = id ?? 1;
            int pageSize = 6;
            PagedList<Dishes> objlist = new DishManager().GetDishes("", pageSize, (pageIndex - 1) * 6, out totalCount).AsQueryable().ToPagedList(pageIndex, pageSize);
            objlist.TotalItemCount = totalCount;
            objlist.CurrentPageIndex = (int)(id ?? 1);
            Common.Common info = new Common.Common();
            info.objDish = objlist;
            return View("DishesShow", info);
        }

        /// <summary>
        /// 跳转到餐厅环境页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Environment()
        {
            return View();
        }

    }
}