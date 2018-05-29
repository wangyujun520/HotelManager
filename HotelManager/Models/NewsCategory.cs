using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// 新闻分类表
    /// </summary>
    [Serializable]
    public class NewsCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
