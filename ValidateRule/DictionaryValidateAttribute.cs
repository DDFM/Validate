using Validate.BaseData;
using Validate.Enum;
using System.Collections.Generic;

namespace Validate.ValidateRule
{
    public class DictionaryValidateAttribute : ValidateAttribute
    {
        /// <summary>
        /// 字典验证
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="mode"></param>
        public DictionaryValidateAttribute(EnumValidateDict dict, EnumValidateMode mode = EnumValidateMode.NotPass)
        {
            enumDict = dict;
            //Dict = DictData.Dict[dict];//进行验证运算的时候进行赋值
            Mode = mode;
        }

        /// <summary>
        /// 字典key
        /// </summary>
        private EnumValidateDict enumDict { get; set; }

        /// <summary>
        /// 字典
        /// </summary>
        private Dictionary<string, object> Dict;

        /// <summary>
        /// 错误信息
        /// </summary>
        public override string ErrorMessage { get; set; }

        /// <summary>
        /// 验证模式
        /// </summary>
        public override EnumValidateMode Mode { get; set; }

        /// <summary>
        /// 错误类型，提供位运算结果
        /// </summary>
        public override EnumValidateFlag ErrorType { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public override bool ToValidate(object ob)
        {
            if (ob != null && !string.IsNullOrEmpty(ob.ToString()))
            {
                Dict = DictData.Dict[enumDict];
                if (!Dict.ContainsKey(ob.ToString()))
                {
                    if (enumDict == EnumValidateDict.诊断编码字典表)
                    {
                        ErrorType = EnumValidateFlag.数据项不合规;
                    }
                    else
                    {
                        ErrorType = EnumValidateFlag.数据项不合规;
                    }
                    return false;
                }
            }
            ErrorType = EnumValidateFlag.合规;
            return true;
        }
    }
}
