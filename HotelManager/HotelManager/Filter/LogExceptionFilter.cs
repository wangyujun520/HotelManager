using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace HotelManager.Filter
{
    public class LogExceptionFilter : FilterAttribute, IExceptionFilter//继承FilterAttribute类，实现IExceptionFilter接口方法
    {
        /// <summary>
        /// 自定义日志文件
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            //定义日志文件路径
            string filePath = filterContext.HttpContext.Server.MapPath(@"~/log.txt");
            //写入日志信息
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine("时间{0}", DateTime.Now.ToString());//获取当前时间
                sw.WriteLine("控制器{0}", filterContext.RouteData.Values["Controller"]);//获取控制器
                sw.WriteLine("动作方法{0}", filterContext.RouteData.Values["Action"]);//获取动作方法
                sw.WriteLine("异常信息{0}", filterContext.Exception.Message);//获取异常信息
            }
        }
    }
}