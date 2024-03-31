using System.ComponentModel;
using System.Reflection;

namespace SpringMountain.Infrastructure.Tools;

/// <summary>
/// 枚举工具类
/// </summary>
public static class EnumTool
{
    /// <summary>
    /// 根据枚举的描述获取枚举值
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="description">枚举描述</param>
    /// <returns></returns>
    public static int? GetValue<T>(string description) where T : Enum
    {
        var enumType = typeof(T);
        var enumValue = Enum.GetNames(enumType).Select(c => new
        {
            Description = enumType.GetField(c)?.GetCustomAttribute<DescriptionAttribute>()?.Description,
            Value = Convert.ToInt32(Enum.Parse(enumType, c))
        }).FirstOrDefault(x => x.Description == description);

        return enumValue?.Value;
    }


    /// <summary>
    /// 获取指定枚举值的描述
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="enumValue">枚举值</param>
    /// <returns></returns>
    public static string GetDescription<T>(T enumValue) where T : Enum
    {
        if (enumValue == null) return "";

        // 获取枚举类型
        var enumType = enumValue.GetType();
        // 获取成员
        var memberInfos = enumType.GetMember(enumValue.ToString());
        if (memberInfos.Length > 0)
        {
            if (memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false)
                is DescriptionAttribute[] { Length: > 0 } attrs)
            {
                // 返回当前描述
                return attrs[0].Description;
            }
        }

        return enumValue.ToString();
    }


    /// <summary>
    /// 获取枚举类所有的描述信息
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IEnumerable<string> GetAllDescriptions<T>() where T : Enum
    {
        var enumType = typeof(T);
        if (!enumType.IsEnum)
            throw new Exception("参数类型 T 必须为枚举类型");

        var fieldInfo = enumType.GetFields();
        foreach (var item in fieldInfo)
        {
            var attributes = item.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length != 0)
            {
                var desAttr = (DescriptionAttribute)attributes[0];
                yield return desAttr.Description;
            }
        }
    }


    /// <summary>
    /// 获取所有的枚举项
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<EnumMember<T>> GetAllMembers<T>() where T : Enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentException($"{nameof(T)} must be an enumerated type");

        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(value => new EnumMember<T>(value))
            .ToList();
    }
}

/// <summary>
/// 枚举成员类
/// </summary>
/// <typeparam name="T"></typeparam>
public class EnumMember<T> where T : Enum
{
    /// <summary>
    /// 枚举值
    /// </summary>
    public T Value { get; set; }

    /// <summary>
    /// 枚举描述
    /// </summary>
    public string Description { get; set; }

    public EnumMember(T value)
    {
        Value = value;
        Description = EnumTool.GetDescription(value);
    }
}