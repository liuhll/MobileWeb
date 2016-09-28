using System;
using System.Collections.Generic;
using Camew.Lottery.AppService;
using Jueci.MobileWeb.Common.Caches;

namespace Jueci.MobileWeb.Lottery.Service.LotteryPlanManager
{
    public class PlanComputionData : ICacheObject
    {
        private List<PlanComputionInfo> _planComputionList;

        public List<PlanComputionInfo> PlanComputionList {
            get { return _planComputionList; }
        }

        public PlanComputionData(List<PlanComputionInfo> planComputionList)
        {
            _planComputionList = planComputionList;
            CacheDateTime = DateTime.Now;
            OperateTime = DateTime.Now;
        }

        public DateTime CacheDateTime { get; }
        public DateTime OperateTime { get; set; }
    }
}