using System;
using System.Collections.Generic;
using Camew.Lottery.AppService;
using Jueci.MobileWeb.Common.Caches;
using Jueci.MobileWeb.Lottery.Models;

namespace Jueci.MobileWeb.Lottery.Service.LotteryPlanManager
{
    public class PlanComputionData : ICacheObject
    {
        private List<PlanComputionInfo> _planComputionList;

        public List<PlanComputionInfo> PlanComputionList {
            get { return _planComputionList; }
        }

        public PlanComputionData(List<PlanComputionInfo> planComputionList,LotteryPlanLib lotteryPlanLib)
        {
            _planComputionList = planComputionList;
            CacheDateTime = DateTime.Now;
            OperateTime = DateTime.Now;
            LotteryPlanLib = lotteryPlanLib;
        }

        public LotteryPlanLib LotteryPlanLib { get; }

        public DateTime CacheDateTime { get; }

        public DateTime OperateTime { get; set; }
    }
}