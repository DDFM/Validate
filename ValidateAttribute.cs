using Validate.Enum;
using System;

namespace Validate
{
    //[AttributeUsage(AttributeTargets.All)]
    public abstract class ValidateAttribute: Attribute
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public abstract string ErrorMessage { get; set; }

        /// <summary>
        /// 错误类型，提供位运算结果
        /// </summary>
        public abstract EnumValidateFlag ErrorType { get; set; }

        /// <summary>
        /// 验证模式
        /// </summary>
        public abstract EnumValidateMode Mode { get; set; }

        /// <summary>
        /// 验证规则
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public abstract bool ToValidate(object ob);
    }
}
