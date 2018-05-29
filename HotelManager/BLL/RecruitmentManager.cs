using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using BLL;


namespace BLL
{
    public class RecruitmentManager
    {
        /// <summary>
        /// 发布招聘信息
        /// </summary>
        /// <param name="objRecru"></param>
        /// <returns></returns>
        public int AddRecruitment(Recruitment objRecru)
        {
            return new DAL.RecruitmentService().AddRecruitment(objRecru);
        }

        /// <summary>
        /// 查询所有职位列表信息
        /// </summary>
        /// <returns></returns>
        public List<Recruitment> GetAllRecList()
        {
            return new DAL.RecruitmentService().GetAllRecList();
        }

        /// <summary>
        /// 根据发布编号查询详细职位信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public Recruitment GetPostById(string postId)
        {
            return new DAL.RecruitmentService().GetPostById(postId);
        }

        /// <summary>
        /// 修改招聘信息
        /// </summary>
        /// <param name="objRecruitment"></param>
        /// <returns></returns>
        public int ModifyRecruiment(Recruitment objRecruitment)
        {
            return new DAL.RecruitmentService().ModifyRecruiment(objRecruitment);
        }

        /// <summary>
        /// 删除招聘信息
        /// </summary>
        /// <param name="postId"></param>
        /// <returns></returns>
        public int DeleteRecruiment(string postId)
        {
            return new DAL.RecruitmentService().DeleteRecruiment(postId);
        }



        }
}
