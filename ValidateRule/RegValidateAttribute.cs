using Validate.Enum;

namespace Validate.ValidateRule
{
    class RegValidateAttribute : ValidateAttribute
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="Reg">正则表达式</param>
        public RegValidateAttribute(string Reg, EnumValidateMode mode = EnumValidateMode.NotPass)
        {
            this.Reg = Reg;
            Mode = mode;
            ErrorType = EnumValidateFlag.合规;
        }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string Reg { get; private set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public override string ErrorMessage { get; set; }

        /// <summary>
        /// 错误类型，提供位运算结果
        /// </summary>
        public override EnumValidateFlag ErrorType { get; set; }

        /// <summary>
        /// 验证模式
        /// </summary>
        public override EnumValidateMode Mode { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public override bool ToValidate(object ob)
        {
            if (ob != null
                && !string.IsNullOrEmpty(ob.ToString())
                && !System.Text.RegularExpressions.Regex.IsMatch(ob.ToString(), Reg)
                )
            {
                ErrorMessage = "参数验证失败";
                ErrorType = EnumValidateFlag.数据项不合规;
                return false;
            }
            return true;
        }
    }
}
