using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Models;
using Common;

namespace DAL
{
    public class DishService
    {

        #region 获取下拉菜单信息
        /// <summary>
        /// 获取下拉菜单信息
        /// </summary>
        /// <returns></returns>
        public List<DishCategory> GetAll()
        {
            string sql = "select CategoryId,CategoryName from DishCategory";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            List<DishCategory> list = new List<DishCategory>();
            while (objReader.Read())
            {
                list.Add(new DishCategory()
                {
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    CategoryName = objReader["CategoryName"].ToString()
                });
            }
            objReader.Close();
            return list;
        }
        #endregion

        #region 发布菜品
        /// <summary>
        /// 发布菜品
        /// </summary>
        /// <param name="objDish"></param>
        /// <returns></returns>
        public int AddDish(Dishes objDish)
        {
            StringBuilder strsql = new StringBuilder("insert into Dishes");
            strsql.Append("(DishName,UnitPrice,CategoryId,DishImg) values");//追加
            strsql.Append("(@DishName,@UnitPrice,@CategoryId,@DishImg);select @@identity");

            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@DishName",objDish.DishName),
                new SqlParameter("@UnitPrice",objDish.UnitPrice),
                new SqlParameter("@CategoryId",objDish.CategoryId),
                new SqlParameter("@DishImg",objDish.DishImg),
            };

            return SQLHelper.QuerySingle(strsql.ToString(), parameter);
        }
        #endregion

        #region 分页查询菜品信息
        /// <summary>
        /// 分页查询菜品信息
        /// </summary>
        /// <param name="stuName"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentCount"></param>
        /// <param name="TotalCount"></param>
        /// <returns></returns>
        public List<Dishes> GetDishes(string CategoryId, int pageSize, int currentCount, out int TotalCount)
        {
            string order = string.Format("DishId DESC");//根据DishId查询
            string TableName = string.Format("Dishes");//查询Dishes表
            string Where = "1=1";
            if (!string.IsNullOrEmpty(CategoryId))
            {
                Where += string.Format("  And CategoryId='{0}'", CategoryId);//根据CategoryId查询
            }
            DataSet ds = SQLCommon.GetList(pageSize, order, currentCount, TableName, Where, out TotalCount);
            List<Dishes> result = new List<Dishes>();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    result.Add(new Dishes()//查询的信息进行封装
                    {
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        DishImg = dr["DishImg"].ToString(),
                        CategoryName = CategoryNameById(Convert.ToInt32(dr["CategoryId"])),
                        DishId = Convert.ToInt32(dr["DishId"]),
                        DishName = dr["DishName"].ToString(),
                        UnitPrice = Convert.ToInt32(dr["UnitPrice"])
                    });
                }
            }
            return result;
        }
        #endregion

        #region 根据ID查询菜品名称
        /// <summary>
        /// 根据ID查询菜品名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CategoryNameById(int id)
        {
            string sql = $"select CategoryName from DishCategory where CategoryId={id}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            string categoryName = null;
            if (objReader.Read())
            {
                categoryName = objReader["CategoryName"].ToString();
            }
            objReader.Close();
            return categoryName;
        }
        #endregion

        #region 根据ID查询菜品信息
        /// <summary>
        /// 根据ID查询菜品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dishes GetDishById(string id)
        {
            string sql = $"select [DishId], [DishName], [UnitPrice], [CategoryId], [DishImg] from Dishes where DishId={id}";
            //string sql = $"select [DishId], [DishName], [UnitPrice], Dishes.[CategoryId], [DishImg],CategoryName from Dishes left join DishCategory on Dishes.CategoryId=DishCategory.CategoryId where DishId={id}";
            SqlDataReader objReader = SQLHelper.GetReader(sql);
            Dishes dishes = null;
            if (objReader.Read())
            {
                dishes = new Dishes()
                {
                    DishId = Convert.ToInt32(objReader["DishId"]),
                    DishName = objReader["DishName"].ToString(),
                    UnitPrice = Convert.ToInt32(objReader["UnitPrice"]),
                    CategoryId = Convert.ToInt32(objReader["CategoryId"]),
                    //CategoryName = objReader["CategoryName"].ToString(),
                    DishImg = objReader["DishImg"].ToString()
                };
            }
            objReader.Close();
            return dishes;
        }
        #endregion

        #region 根据Id进行修改菜品信息
        /// <summary>
        /// 更新菜品
        /// </summary>
        /// <param name="objDish"></param>
        /// <returns></returns>
        public int UpdateDish(Dishes objDish)
        {
            //普通语句
            //string sql = $"update Dishes set [DishName]='{objDish.DishName}', [UnitPrice]='{objDish.UnitPrice}', [CategoryId]='{objDish.CategoryId}', [DishImg]='{objDish.DishImg}' where [DishId]='{objDish.DishId}'";
            //return SQLHelper.QuerySingle(sql);

            //存储过程
            SqlParameter[] parameter = new SqlParameter[] {
                new SqlParameter("@DishName",objDish.DishName),
                new SqlParameter("@UnitPrice",objDish.UnitPrice),
                new SqlParameter("@CategoryId",objDish.CategoryId),
                new SqlParameter("@DishImg",objDish.DishImg),
                new SqlParameter("@DishId",objDish.DishId),
            };
            return SQLHelper.UpdateByProcedure("usp_UpDish", parameter);//第一个是存储过程的名称
        }
        #endregion

        #region 删除菜品
        /// <summary>
        /// 删除菜品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelDishById(string id)
        {
            string sql = $"delete Dishes where DishId={id}";
            return SQLHelper.QuerySingle(sql);
        }
        #endregion

    }
}
