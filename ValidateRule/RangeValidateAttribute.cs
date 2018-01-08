using Validate.Enum;

namespace Validate.ValidateRule
{
    public class RangeValidateAttribute: ValidateAttribute
    {
        /// <summary>
        /// 最大值
        /// </summary>
        public int MaxLength { get; private set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public int MinLength { get; private set; }

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
            if (ob != null)
            {
                if (ob.ToString().Length >= MinLength && ob.ToString().Length <= MaxLength)
                {
                    return true;
                }
                ErrorMessage = "长度：[" + MinLength + "-" + MaxLength + "]";
                ErrorType = EnumValidateFlag.数据项不合规;
                return false;
            }
            ErrorType = EnumValidateFlag.合规;
            return true;

        }
        /// <summary>
        /// 范围
        /// </summary>
        /// <param name="MinLength">最小值</param>
        /// <param name="MaxLength">最大值</param>
        public RangeValidateAttribute(int MinLength, int MaxLength, EnumValidateMode mode = EnumValidateMode.NotPass)
        {
            this.MaxLength = MaxLength;
            this.MinLength = MinLength;
            Mode = mode;
        }
        /// <summary>
        /// 范围
        /// </summary>
        /// <param name="MaxLength">最大值</param>
        public RangeValidateAttribute(int MaxLength) : this(0, MaxLength, EnumValidateMode.NotPass)
        {
        }
    }
}
