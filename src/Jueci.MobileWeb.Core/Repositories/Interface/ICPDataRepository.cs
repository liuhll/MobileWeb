using Abp.Dependency;
using Camew;
using Camew.Lottery;

namespace Jueci.MobileWeb.Repositories
{
    public interface ICPDataRepository : ITransientDependency
    {
        ResultObject GetLatestCPData(CPType id, int i, int latestDataFlag, int maxDataCount);
    }
}