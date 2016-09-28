using System.Collections.Generic;
using Camew.Lottery.AppService;
using Jueci.MobileWeb.Common.Enums;

namespace Jueci.MobileWeb.Lottery.Models.Transfer
{
    /// <summary>
    /// 用户计划信息
    /// </summary>
    public class UserPlanInfo
    {
     
        /// <summary>
        /// 彩种名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 计划区域
        /// </summary>
        public string PlanSection { get; set; }

        /// <summary>
        /// 预测值
        /// </summary>
        public string GuessValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<RightOrWrongEnum> GuessResultList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float GuessPercent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? EndIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DMSMType DsType { get; set; }
    }
}