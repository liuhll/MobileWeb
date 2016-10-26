using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jueci.MobileWeb.Common
{
    internal class MessageTips
    {
        /// <summary>
        /// 还没有从数据库中拉取到最近的开奖信息
        /// </summary>
        public const string NoDmsmResult = "还没有获取到开奖信息，请稍后再尝试！";

        public const string WaitingServiceStart = "应用正在启动,请稍后尝试!";

        public const string NoThisPlanDetail = "没有该名称为{0}的计划信息!";

        public const string NoExitPlanLib = "不存在该计划库，请检查的您的Uid和Sid是否正确！";

        public const string NotValidTime = "该请求的时间已经失效";

        public const string NotLegalSign = "非法签名";

        public const string StartCallApiLog = "开始调用{0}_Api接口，参数值为{1}";

        public const string EndCallApiLog = "接口{0}调用成功，返回值为{1}";

        public const string NoAccessRight = "没有访问权限,请输入访问码后再尝试访问";

        public const string AccessCodeError = "您输入的访问码错误,请重新输入";

        public const string AccessCodeChange = "访问超时或是访问码已经被所有者修改，请重新输入";
    }
}
