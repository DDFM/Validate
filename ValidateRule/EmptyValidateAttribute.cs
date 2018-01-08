using Validate.Enum;

namespace Validate.ValidateRule
{
    public class EmptyValidateAttribute : ValidateAttribute
    {
        /// <summary>
        /// 空、null验证
        /// </summary>
        /// <param name="mode"></param>
        public EmptyValidateAttribute(EnumValidateMode mode = EnumValidateMode.NotPass)
        {
            Mode = mode;
        }

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
            if (ob == null || string.IsNullOrEmpty(ob.ToString()))
            {
                ErrorMessage = "字段不能为空";
                ErrorType = EnumValidateFlag.数据项不合规;
                return false;
            }
            ErrorType = EnumValidateFlag.合规;
            return true;
        }
    }
}
