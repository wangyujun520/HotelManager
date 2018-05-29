using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DAL
{
    public class NewsService
    {
        #region 查询指定的新闻条数
        /// <summary>
        /// 查询指定的新闻条数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<News> GetNews(int count)
        {
            //使用带参的sql语句
            string sql = "select top (@count) NewsId, NewsTitle, NewsContents, PublishTime, NewsCategory.CategoryId,CategoryName from News inner join NewsCategory on News.CategoryId=NewsCategory.CategoryId order by PublishTime desc";
            //sql排序有升序和降序order by desc是降序asc是升序
            SqlParameter[] parameter = new SqlParameter[] 
            {
                new SqlParameter("@count",count)
            };
            List<News> list = new List<News>();
            SqlDataReader objReader = SQLHelper.GetReader(sql,parameter);
            //查询多条数List集合据用while
            while (objReader.Read())
            {
                list.Add(new News()
                {
                    NewsId =Convert.ToInt32( objReader["NewsId"]),
                    NewsTitle = objReader["NewsTitle"].ToString(),
                    NewsContents = objReader["NewsContents"].ToString(),
                    PublishTime = Convert.ToDateTime(objReader["PublishTime"]),
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    CategoryName = objReader["CategoryName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        #endregion

        #region 通过ID查询新闻详情
        /// <summary>
        /// 根据ID查询新闻详情
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public News GetNewsById(string newsId)
        {
            string sql = "select [NewsId], [NewsTitle], [NewsContents], [PublishTime], [CategoryId] from News where NewsId=@NewsId";
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@NewsId",newsId)
            };
            
            SqlDataReader objReader = SQLHelper.GetReader(sql, parameter);
            News news = null;
            //查询一条实体数据用if
            if(objReader.Read())
            {
                news = new News()
                {
                    NewsId = Convert.ToInt32(objReader["NewsId"]),
                    NewsTitle = objReader["NewsTitle"].ToString(),
                    NewsContents = objReader["NewsContents"].ToString(),
                    PublishTime = Convert.ToDateTime(objReader["PublishTime"]),
                    CategoryId = Convert.ToInt32(objReader["CategoryId"])
                };
            }
            objReader.Close();
            return news;
        }
        #endregion

        #region 发布新闻
        /// <summary>
        /// 发布新闻
        /// </summary>
        /// <param name="objNews"></param>
        /// <returns>受影响的行数</returns>
        public int PublishNews(News objNews)
        {
            string sql = "insert into News(NewsTitle,NewsContents,CategoryId) values(@NewsTitle,@NewsContents,@CategoryId)";
            SqlParameter[] parameter = new SqlParameter[]
            {
                new SqlParameter("@NewsTitle",objNews.NewsTitle),
                new SqlParameter("@NewsContents",objNews.NewsContents),
                new SqlParameter("@CategoryId",objNews.CategoryId)
            };
            return SQLHelper.QuerySingle(sql, parameter);
        }
        #endregion

        #region 根据新闻Id修改新闻内容
        /// <summary>
        /// 根据新闻编号修改新闻详情
        /// </summary>
        /// <param name="objNews"></param>
        /// <returns></returns>
        public int UpdateNewsById(News objNews)
        {
            string sql = "update News set [NewsTitle]=@NewsTitle, [NewsContents]=@NewsContents, [PublishTime]=@PublishTime, [CategoryId]=@CategoryId where NewsId=@NewsId";
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@NewsTitle",objNews.NewsTitle),
                new SqlParameter("@NewsContents",objNews.NewsContents),
                new SqlParameter("@PublishTime",objNews.PublishTime),
                new SqlParameter("@CategoryId",objNews.CategoryId),
                new SqlParameter("@NewsId",objNews.NewsId),
            };
            return SQLHelper.QuerySingle(sql, parameter);
        }
        #endregion

        #region 根据新闻Id删除新闻内容
        /// <summary>
        /// 根据新闻Id删除新闻内容
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public int DeleteNewsById(int newsId)
        {
            string sql = "delete News where NewsId=@NewsId";
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@NewsId",newsId)
            };
            return SQLHelper.QuerySingle(sql, parameter);
        }
        #endregion

        #region 获取新闻所有分类
        /// <summary>
        /// 获取新闻所有分类
        /// </summary>
        /// <returns></returns>
        public List<NewsCategory> GetAllCategory()
        {
            string sql = "SELECT CategoryId,CategoryName FROM NewsCategory";
            List<NewsCategory> list = new List<NewsCategory>();
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            while (objReader.Read())
            {
                list.Add(new NewsCategory()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    CategoryName = objReader["CategoryName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        #endregion

        #region 分页查询新闻信息
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
            string order = string.Format("PublishTime DESC");
            string TableName = string.Format("News");
            string Where = "1=1";
            if (!string.IsNullOrEmpty(CategoryId))
            {
                Where += string.Format("  And CategoryId='{0}'", CategoryId);
            }
            DataSet ds = SQLCommon.GetList(pageSize, order, currentCount, TableName, Where, out TotalCount);
            List<News> result = new List<News>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new News()
                    {
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        NewsContents = dr["NewsContents"].ToString(),
                        NewsId = Convert.ToInt32(dr["NewsId"]),
                        NewsTitle = dr["NewsTitle"].ToString(),
                        PublishTime = Convert.ToDateTime(dr["PublishTime"].ToString())
                    });
                }
            }
            return result;
        }
        #endregion

        #region 根据新闻编号查询新闻名称
        /// <summary>
        /// 根据新闻编号查询新闻名称
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public string GetCategoryName(int categoryId)
        {
            string sql = string.Format("select CategoryName from NewsCategory where CategoryId={0}", categoryId);
            string categoryName = null;
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            if (objReader.Read())
            {
                categoryName = objReader["CategoryName"].ToString();
            }
            objReader.Close();
            return categoryName;
        }
        #endregion

        #region 修改新闻
        /// <summary>
        /// 新闻更新
        /// </summary>
        /// <param name="objmodel"></param>
        /// <returns></returns>
        public int ModifyNews(News objmodel)
        {
            try
            {
                string sql = string.Format("UPDATE News SET NewsTitle='{0}',NewsContents='{1}',PublishTime='{2}',CategoryId='{3}' WHERE NewsId={4}", objmodel.NewsTitle, objmodel.NewsContents, objmodel.PublishTime, objmodel.CategoryId, objmodel.NewsId);
                return SQLHelper.QuerySingle(sql);
            }
            catch (SqlException e)
            {
                throw new Exception("数据库操作出现异常！具体信息:\r\n" + e.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region 删除新闻
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        public int DelNews(string newsId)
        {
            try
            {
                string sql = "DELETE FROM News WHERE NewsId=@NewsId";
                SqlParameter[] param = new SqlParameter[] { new SqlParameter("@NewsId", newsId) };
                return SQLHelper.QuerySingle(sql, param);
            }
            catch (SqlException e)
            {
                throw new Exception("数据库操作出现异常！具体信息:\r\n" + e.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}