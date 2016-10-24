namespace Jueci.MobileWeb.Lottery.Models
{
    /// <summary>
    /// 缓存计划更新接口参数Model
    /// </summary>
    public class PlanCacheArgs
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int Uid { get; set; }

        /// <summary>
        /// 服务id
        /// </summary>
        public int Sid { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 签    名 <br/>
        /// 签名方法： ssh256(uid+sid+timestamp+secret_key )
        /// </summary>
        /// <remarks>
        /// ssh256(uid+sid+timestamp+secret_key )
        /// </remarks>
        public string Sign { get; set; }
    }
}