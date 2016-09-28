using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Camew;
using Camew.Lottery;
using Jueci.MobileWeb.Lottery.Models;
using Jueci.MobileWeb.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace Jueci.MobileWeb.EntityFramework.Repositories.Impl
{
    public class CPDataRepository : CpDbContext, ICPDataRepository
    {
        public ResultObject GetLatestCPData(CPType id, int i, int latestDataFlag, int maxDataCount)
        {

            ResultObject ret = new ResultObject();
            Logger.Info(string.Format("当前时间:{0},更新数据的类型:{1}",DateTime.Now,id));
            using (System.Data.Common.DbCommand cmd = Database.Connection.CreateCommand())
            {

                System.Data.SqlClient.SqlParameter[] parameters = {
                                                              new System.Data.SqlClient.SqlParameter("@CPType", (int)id),
                                                              new System.Data.SqlClient.SqlParameter("@LatestDataID", i),
                                                              new System.Data.SqlClient.SqlParameter("@LatestDataFlag", latestDataFlag),
                                                              new System.Data.SqlClient.SqlParameter("@MaxDataCount", maxDataCount),
                                                              new SqlParameter("@UpdteFlag", SqlDbType.Int),
                                                              };
                parameters[4].Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "P_GetLatestCPData2";
                cmd.Parameters.AddRange(parameters);
                if (cmd.Connection.State != System.Data.ConnectionState.Open)
                    cmd.Connection.Open();
                //  cmd.ExecuteNonQuery();
                List<CPData> list = new List<CPData>();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CPData item = new CPData();
                        item.ID = Convert.ToInt32(reader["ID"]);
                        item.Data = reader["CPData"].ToString();
                        item.CreateTime = Convert.ToDateTime(reader["CreateTime"]);
                        list.Add(item);
                    }
                }
                ret.Data = list;
                ret.Result = 0;
                ret.Remarks = parameters[4].Value.ToString();
                return ret;
            }
          
        }

       
    }
}