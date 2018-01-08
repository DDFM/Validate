using Validate.Enum;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Validate
{
    public class ValidateHelper
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        public static List<string> ErrorMsg { get; private set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public static List<string> Msg { get; private set; }

        /// <summary>
        /// 验证是否成功
        /// </summary>
        public static bool IsValidate { get; private set; }

        /// <summary>
        /// 错误类型，位运算<see cref="EnumValidateFlag"/>
        /// </summary>
        public static int ErrorCode { get; private set; }

        static ValidateHelper()
        {
            Init();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private static void Init()
        {
            ErrorMsg = new List<string>();
            Msg = new List<string>();
            IsValidate = true;
            ErrorCode = 0;
        }
        private static void Getres(object entityObject)
        {
            if (entityObject == null) return;
            Type type = entityObject.GetType();
            if (type.IsGenericType && type.Name.Contains("List"))
            {
                IEnumerable<object> d = entityObject as IEnumerable<object>;
                if (d != null)
                    foreach (var item in d)
                        Getres(item);
            }
            else
            {
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    //获取属性的值 
                    object value = property.GetValue(entityObject, null);
                    if (value != null && !value.GetType().IsValueType && value.GetType() != typeof(string))
                        Getres(value);
                    else
                    {
                        //获取验证特性
                        object[] validateContent = property.GetCustomAttributes(typeof(ValidateAttribute), true);
                        if (validateContent != null)
                        {
                            string resErrorVali = string.Empty;
                            string resVali = string.Empty;
                            foreach (ValidateAttribute validateAttribute in validateContent)
                            {
                                if (!validateAttribute.ToValidate(value))
                                {
                                    if ((ErrorCode & Convert.ToInt32(validateAttribute.ErrorType)) <= 0)
                                    {
                                        ErrorCode = ErrorCode | Convert.ToInt32(validateAttribute.ErrorType);
                                    }
                                    var msg = validateAttribute.ErrorMessage;
                                    if (validateAttribute.Mode == EnumValidateMode.NotPass)
                                    {
                                        IsValidate = false;
                                        if (!string.IsNullOrEmpty(msg))
                                        {
                                            resErrorVali += msg;
                                        }
                                    }
                                    else if (validateAttribute.Mode == EnumValidateMode.Pass)
                                    {
                                        if (!string.IsNullOrEmpty(msg))
                                        {
                                            resVali += msg;
                                        }
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(resErrorVali))
                            {
                                ErrorMsg.Add(type.Name + "-字段：" + property.Name + "，" + resErrorVali);

                            }
                            if (!string.IsNullOrEmpty(resVali))
                            {
                                Msg.Add(resVali);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 验证对象字段
        /// </summary>
        /// <param name="obj">验证对象</param>
        /// <returns>是否成功</returns>
        public static bool ToValidate(object obj)
        {
            Init();
            Getres(obj);
            return IsValidate;
        }
    }
}
