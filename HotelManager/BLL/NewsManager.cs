using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public class NewsManager
    {
        /// <summary>
        /// 查询指定的新闻条数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<News> GetNews(int count)
        {
            return new DAL.NewsService().GetNews(count);
        }

        /// <summary>
        /// 根据ID查询新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsById(string newsId)
        {
            return new DAL.NewsService().GetNewsById(newsId);
        }

        /// <summary>
        /// 发布新闻
        /// </summary>
        /// <param name="objNews"></param>
        /// <returns>受影响的行数</returns>
        public int PublishNews(News objNews)
        {
            return new DAL.NewsService().PublishNews(objNews);
        }

        /// <summary>
        /// 根据新闻编号修改新闻详情
        /// </summary>
        /// <param name="objNews"></param>
        /// <returns></returns>
        public int UpdateNewsById(News objNews)
        {
            return new DAL.NewsService().UpdateNewsById(objNews);
        }

        /// <summary>
        /// 根据新闻Id删除新闻内容
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public int DeleteNewsById(int newsId)
        {
            return new DAL.NewsService().DeleteNewsById(newsId);
        }

        /// <summary>
        /// 获取新闻所有分类
        /// </summary>
        /// <returns></returns>
        public List<NewsCategory> GetAllCategory()
        {
            return new DAL.NewsService().GetAllCategory();
        }

        /// <summary>
        /// 分页查询新闻信息
        /// </summary>
        /// <param name="stuName"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public List<News> GetNews(string CategoryId, int pageSize, int currentCount, out int TotalCount)
        {
            return new DAL.NewsService().GetNews(CategoryId, pageSize, currentCount, out TotalCount);
        }

        /// <summary>
        /// 根据新闻编号查询新闻名称
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string GetCategoryName(int categoryId)
        {
            return new DAL.NewsService().GetCategoryName(categoryId);
        }

        /// <summary>
        /// 新闻更新
        /// </summary>
        /// <param name="objmodel"></param>
        /// <returns></returns>
        public int ModifyNews(News objmodel)
        {
            return new DAL.NewsService().ModifyNews(objmodel);
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public int DelNews(string newsId)
        {
            return new DAL.NewsService().DelNews(newsId);
        }

        }
}