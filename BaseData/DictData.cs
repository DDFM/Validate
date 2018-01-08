using Validate.Enum;
using System;
using System.Collections.Generic;
using System.Web;

namespace Validate.BaseData
{
    /// <summary>
    /// 字典数据
    /// </summary>
    public static class DictData
    {
        /// <summary>
        /// 初始化时加载字典数据，可进行字典的特性验证
        /// </summary>
        public static IDictionary<EnumValidateDict, Dictionary<string, object>> Dict { get; set; }

        /// <summary>
        /// 缓存模式
        /// </summary>
        public static Dictionary<EnumValidateDict, Dictionary<string, object>> DictCache
        {
            get
            {
                if (HttpRuntime.Cache.Get("dataDictionary") != null)
                    return HttpRuntime.Cache.Get("dataDictionary") as Dictionary<EnumValidateDict, Dictionary<string, object>>;
                var res = new Dictionary<EnumValidateDict, Dictionary<string, object>>();//数据库交互，拿到数据
                HttpRuntime.Cache.Insert("dataDictionary", res, null, DateTime.Now.AddMinutes(20), System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Default, null);
                return res;
            }
        }
    }
}
