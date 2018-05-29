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
    public class DishsController : Controller
    {
        // GET: HotelAdmin/Dishs
        /// <summary>
        /// 跳转到添加页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DishesPublish()
        {
            return View();
        }
        #region 发布菜品
        /// <summary>
        /// 发布菜品
        /// </summary>
        /// <param name="objModel"></param>
        /// <param name="DishImg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoAdd(Dishes objModel,HttpPostedFileBase DishImg)//传递参数的时候映射跟视图中要上传的图片ID一致
        {
            //判断文件是否上传成功（文件大小、文件名重命名）
            try
            {
                //判断是否有文件
                if (DishImg != null && DishImg.FileName!="")
                {
                    //判断文件大小是否符合要求
                    double fileLength = DishImg.ContentLength / (1024.0 * 1024.0);//是否大于2M
                    if (fileLength>2.0)
                    {
                        return Content("<script>alert('图片最大不能超过2MB');location.href='"+Url.Action("DishesPublish") +"';</script>");
                    }
                    //获取文件名
                    string fileName = DishImg.FileName;
                    //重命名
                    fileName = DateTime.Now.ToString("yyyyMMddmmhhss")+".png";
                    objModel.DishImg = fileName;
                    int res = 0;

                    //判断是修改还是新增
                    if (objModel.DishId!=0)//代表用户要做修改菜品
                    {
                        //调用BLL进行内容修改到数据库
                        res = new BLL.DishManager().UpdateDish(objModel);
                        if (res > 0)
                        {
                            string filePath = Server.MapPath("~/UploadFile/" + fileName);
                            DishImg.SaveAs(filePath);//保存
                            //修改成功跳转到管理页面
                            return Content("<script>alert('菜品修改成功！');location.href='" + Url.Action("DishesManager") + "';</script>");
                        }
                        else
                        {
                            //修改失败还是返回当前页面
                            return Content("<script>alert('菜品修改失败！');location.href='" + Url.Action("DishesPublish") + "';</script>");
                        }
                    }
                    else//代表新增菜品
                    {
                        //调用BLL进行内容插入到数据库
                        res = new BLL.DishManager().AddDish(objModel);
                        if (res > 0)
                        {
                            //添加成功,成功的时候上传图片到项目文件底下
                            //图片上传路径
                            string filePath = Server.MapPath("~/UploadFile/" + fileName);
                            DishImg.SaveAs(filePath);//保存
                            return Content("<script>alert('菜品上传成功！');location.href='" + Url.Action("DishesPublish") + "';</script>");
                        }
                        else
                        {
                            //添加失败
                            return Content("<script>alert('菜品上传失败！');location.href='" + Url.Action("DishesPublish") + "';</script>");
                        }
                    }
                    
                }
                else
                {
                    //文件不存在
                    return Content("<script>alert('请选择上传的图片！');location.href='" + Url.Action("DishesPublish") + "';</script>");
                }
            }
            catch (Exception ex)
            {
                return Content("<script>alert('程序出错！'"+ex.Message+");location.href='" + Url.Action("DishesPublish") + "';</script>");
            }
        }
        #endregion

        #region 菜品分页查询
        /// <summary>
        /// 菜品分页查询
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DishesManager(string CategoryId,int? id=1)
        {
            //List<Dishes>
            int totalCount = 0;
            int pageIndex = id ?? 1;
            int pageSize = 6;
            PagedList<Dishes> objlist = new DishManager().GetDishes(CategoryId, pageSize, (pageIndex - 1) * 5, out totalCount).AsQueryable().ToPagedList(pageIndex, pageSize);
            objlist.TotalItemCount = totalCount;
            objlist.CurrentPageIndex = (int)(id ?? 1);
            Common.Common info = new Common.Common();
            info.objDish = objlist;
            return View("DishesManager", info);
        }
        #endregion

        #region 根据ID删除菜品信息，并删除图片
        /// <summary>
        /// 根据ID删除菜品对象，并删除图片
        /// </summary>
        /// <param name="disId"></param>
        /// <returns></returns>
        public ActionResult DelDish(string disId)
        {
            //先根据ID查询对象模型
            Dishes objModel = new DishManager().GetDishById(disId);
            //找一下有没有图片信息，
            string filePath = Server.MapPath("~/UploadFile/" + objModel.DishImg);
            //获取业务逻辑
            int result = new BLL.DishManager().DelDishById(disId);
            if (result>0)
            {
                //判断文件是否存在
                if (System.IO.File.Exists(filePath))
                {
                    //存在就删除
                    System.IO.File.Delete(filePath);
                }
                return Content("删除成功！");
            }
            else
            {
                return Content("删除失败！");
            }
        }
        #endregion

    }
}