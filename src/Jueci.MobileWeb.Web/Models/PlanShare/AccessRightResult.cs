namespace Jueci.MobileWeb.Web.Models.PlanShare
{
    public class AccessRightResult
    {
        public string ReturnUrl { get; set; }

        public string MessageTips { get; set; }

        public bool HasAccessRight { get; set; }

        public AccessRightResult(bool hasAccessRight)
        {
            HasAccessRight = hasAccessRight;
        }

        public AccessRightResult(bool hasAccessRight, string msg) : this(hasAccessRight)
        {
            MessageTips = msg;
        }

        public AccessRightResult(bool hasAccessRight, string msg, string url) : this(hasAccessRight, msg)
        {
            ReturnUrl = url;
            MessageTips = msg;
        }


    }
}