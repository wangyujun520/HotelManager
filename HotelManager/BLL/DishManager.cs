using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class DishManager
    {
        /// <summary>
        /// 发布菜品
        /// </summary>
        /// <param name="objDish"></param>
        /// <returns></returns>
        public int AddDish(Dishes objDish)
        {
            return new DAL.DishService().AddDish(objDish);
        }

        /// <summary>
        /// 获取下拉菜单信息
        /// </summary>
        /// <returns></returns>
        public List<DishCategory> GetAll()
        {
            return new DAL.DishService().GetAll();
        }
        
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
            return new DAL.DishService().GetDishes(CategoryId, pageSize, currentCount, out TotalCount);
        }

        /// <summary>
        /// 根据ID查询菜品信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dishes GetDishById(string id)
        {
            return new DAL.DishService().GetDishById(id);
        }

        /// <summary>
        /// 更新菜品
        /// </summary>
        /// <param name="objDish"></param>
        /// <returns></returns>
        public int UpdateDish(Dishes objDish)
        {
            return new DAL.DishService().UpdateDish(objDish);
        }

        /// <summary>
        /// 删除菜品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelDishById(string id)
        {
            return new DAL.DishService().DelDishById(id);
        }

        }
}

