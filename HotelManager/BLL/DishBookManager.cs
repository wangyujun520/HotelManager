using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class DishBookManager
    {
        /// <summary>
        /// 客户预定
        /// </summary>
        /// <param name="objDishBook"></param>
        /// <returns></returns>
        public int Book(DishBook objDishBook)
        {
            return new DAL.DishBookService().Book(objDishBook);
        }

        /// <summary>
        /// 获取未关闭的订单或未审核的订单
        /// </summary>
        /// <returns></returns>
        public List<DishBook> GetAllDishBook()
        {
            return new DAL.DishBookService().GetAllDishBook();
        }

        /// <summary>
        /// 根据预定id修改订单状态
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        public int ModifyBook(string bookId, string orderStatus)
        {
            return new DAL.DishBookService().ModifyBook(bookId, orderStatus);
        }


        }
}
