using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public class SuggestionManager
    {
        /// <summary>
        /// 提交投诉
        /// </summary>
        /// <param name="objSuggestion"></param>
        /// <returns></returns>
        public int SubmitSuggestion(Suggestion objSuggestion)
        {
            return new DAL.SuggestionService().SubmitSuggestion(objSuggestion);
        }

        /// <summary>
        /// 获取最新的建议
        /// </summary>
        /// <returns></returns>
        public List<Suggestion> GetSuggestion()
        {
            return new DAL.SuggestionService().GetSuggestion();
        }

        /// <summary>
        /// 受理投诉
        /// </summary>
        /// <param name="suggestionId"></param>
        /// <returns></returns>
        public int HandlSuggestion(string suggestionId)
        {
            return new DAL.SuggestionService().HandlSuggestion(suggestionId);
        }

        }
}
