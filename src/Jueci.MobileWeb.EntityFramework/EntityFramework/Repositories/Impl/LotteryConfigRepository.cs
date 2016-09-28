using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Abp.EntityFramework;
using Camew;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Repositories;

namespace Jueci.MobileWeb.EntityFramework.Repositories.Impl
{
    public class LotteryConfigRepository : MobileWebRepositoryBase<LotteryConfig,string>, ILotteryConfigRepository
    {
        public LotteryConfigRepository(IDbContextProvider<MobileWebDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public ResultObject<XElement> GetServiceInitConfig(CPType cpType)
        {
            ResultObject<XElement> ret = new ResultObject<XElement>();
            var lotteryConfig = Single(p => p.CpType == cpType.ToString());
            if (lotteryConfig == null)
            {
                ret.Result = -2;
                ret.Remarks = "配置文件不存在！";
            }
            else
            {
                ret.Data = XElement.Parse(lotteryConfig.ConfigData);
                ret.Result = 0;
            }
          
            return ret;
        }

      
    }
}