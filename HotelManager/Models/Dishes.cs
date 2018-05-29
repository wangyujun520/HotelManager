using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 菜品表
    /// </summary>
    [Serializable]
    public class Dishes
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public int UnitPrice { get; set; }
        public int CategoryId { get; set; }
        public string DishImg { get; set; }
        public string CategoryName { get; set; }
    }
}
