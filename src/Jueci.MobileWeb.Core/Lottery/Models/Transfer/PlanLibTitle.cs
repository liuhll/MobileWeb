﻿using System;
using Camew.Lottery;
using Jueci.MobileWeb.Common.Enums;

namespace Jueci.MobileWeb.Lottery.Models.Transfer
{
    public class PlanLibTitle
    {
        private string _planTitle;

        private string _subPlanTitle;

        public PlanLibTitle(CPType cpType, PlanLibState planLibState)
        {
            CpType = cpType;
            PlanLibState = planLibState;
        }

        public string PlanTitle
        {
            get { return _planTitle; }
            set
            {
                _planTitle = this.PlanLibState == PlanLibState.Team && !string.IsNullOrEmpty(value) ?
                    value : "掌赢专家";
            }
        }

        public string SubPlanTitle
        {
            get { return _subPlanTitle; }
            set
            {
                _subPlanTitle = this.PlanLibState == PlanLibState.Team && !string.IsNullOrEmpty(value) ?
                    value : GetSubPlanTitle();
            }
        }

        private string GetSubPlanTitle()
        {
            string subPlanTitle = string.Empty;
            switch (CpType)
            {
                case CPType.cqssc:
                    subPlanTitle = "重庆时时彩计划";
                    break;
                case CPType.pks:
                    subPlanTitle = "北京PK10计划";
                    break;
                case CPType.gdklsf:
                    subPlanTitle = "广东快乐十分";
                    break;
                case CPType.cqklsf:
                    subPlanTitle = "幸运农场掌赢专家";
                    break;
                case CPType.jx11x5:
                    subPlanTitle = "江西11选5掌赢专家";
                    break;
                case CPType.gd11x5:
                    subPlanTitle = "广东11选5掌赢专家";
                    break;
                case CPType.sd11x5:
                    subPlanTitle = "11运夺金掌赢专家";
                    break;
                case CPType.jsks:
                    subPlanTitle = "江苏快3掌赢专家";
                    break;
                case CPType.kl8:
                    subPlanTitle = "快乐8掌赢专家";
                    break;

            }
            return subPlanTitle;
        }

        public PlanLibState PlanLibState { get; set; }

        public CPType CpType { get; set; }


    }
}