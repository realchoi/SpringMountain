using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace System;

/// <summary>
/// <see cref="string"/> 类扩展方法
/// </summary>
public static class AbpStringExtensions
{
    /// <summary>
    /// 如果给定的字符串不是以某个字符结尾，则向其尾部添加该字符，以确保该字符串以该字符结尾。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="c"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static string EnsureEndsWith(this string str, char c,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(str, nameof(str));

        if (str.EndsWith(c.ToString(), comparisonType))
        {
            return str;
        }

        return str + c;
    }

    /// <summary>
    /// 如果给定的字符串不是以某个字符开头，则向其头部添加该字符，以确保该字符串以该字符开头。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="c"></param>
    /// <param name="comparisonType"></param>
    /// <returns></returns>
    public static string EnsureStartsWith(this string str, char c,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(str, nameof(str));

        if (str.StartsWith(c.ToString(), comparisonType))
        {
            return str;
        }

        return c + str;
    }

    /// <summary>
    /// 判断给定的字符串是否是 null 或者空字符串。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// 判断给定的字符串是否是 null、空字符串或者空白字符。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// 从给定的字符串左侧开始，截取指定长度的子串。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public static string Left(this string str, int len)
    {
        ArgumentNullException.ThrowIfNull(str, nameof(str));

        if (str.Length < len)
        {
            throw new ArgumentException($"参数 {nameof(len)} 不能比给定的字符串的长度大！");
        }

        // 以下语句可以替换为：return str[..len];
        return str.Substring(0, len);
    }

    /// <summary>
    /// 将给定字符串的换行符转换为 <see cref="Environment.NewLine"/>。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string NormalizeLineEndings(this string str)
    {
        ArgumentNullException.ThrowIfNull(str, nameof(str));

        return str.Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Replace("\n", Environment.NewLine);
    }

    /// <summary>
    /// 获取给定的字符串中第 n 次出现的指定的字符的索引。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="c"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int NthIndexOf(this string str, char c, int n)
    {
        ArgumentNullException.ThrowIfNull(str, nameof(str));

        var count = 0;
        for (var i = 0; i < str.Length; i++)
        {
            if (str[i] != c)
            {
                continue;
            }

            if ((++count) == n)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 从给定的字符串的末尾删除第一次出现的后缀。
    /// </summary>
    /// <param name="str">给定的字符串</param>
    /// <param name="postFixes">一个或多个需要删除的后缀</param>
    /// <returns>如果给定的字符串以指定的字符串结尾，则返回删除指定后缀后的字符串；否则返回原字符串。</returns>
    public static string RemovePostFix(this string str, params string[] postFixes)
    {
        return str.RemovePostFix(StringComparison.Ordinal, postFixes);
    }

    /// <summary>
    /// 从给定的字符串的末尾删除第一次出现的后缀。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="comparisonType"></param>
    /// <param name="postFixes"></param>
    /// <returns>如果给定的字符串以指定的字符串结尾，则返回删除指定后缀后的字符串；否则返回原字符串。</returns>
    public static string RemovePostFix(this string str, StringComparison comparisonType, params string[] postFixes)
    {
        if (str.IsNullOrEmpty())
        {
            return str;
        }

        if (postFixes.IsNullOrEmpty())
        {
            return str;
        }

        foreach (var postFix in postFixes)
        {
            if (str.EndsWith(postFix, comparisonType))
            {
                return str.Left(str.Length - postFix.Length);
            }
        }

        return str;
    }

    /// <summary>
    /// 从给定的字符串的开头删除第一次出现的前缀。
    /// </summary>
    /// <param name="str">给定的字符串</param>
    /// <param name="preFixes">一个或多个需要删除的前缀</param>
    /// <returns>如果给定的字符串以指定的字符串开头，则返回删除指定前缀后的字符串；否则返回原字符串。</returns>
    public static string RemovePreFix(this string str, params string[] preFixes)
    {
        return str.RemovePreFix(StringComparison.Ordinal, preFixes);
    }

    /// <summary>
    /// 从给定的字符串的开头删除第一次出现的前缀。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="comparisonType"></param>
    /// <param name="preFixes"></param>
    /// <returns>如果给定的字符串以指定的字符串开头，则返回删除指定前缀后的字符串；否则返回原字符串。</returns>
    public static string RemovePreFix(this string str, StringComparison comparisonType, params string[] preFixes)
    {
        if (str.IsNullOrEmpty())
        {
            return str;
        }

        if (preFixes.IsNullOrEmpty())
        {
            return str;
        }

        foreach (var preFix in preFixes)
        {
            if (str.StartsWith(preFix, comparisonType))
            {
                return str.Right(str.Length - preFix.Length);
            }
        }

        return str;
    }

    /// <summary>
    /// 将给定的字符串中的某一个子串替换为指定的内容。
    /// </summary>
    /// <param name="str">给定的字符串</param>
    /// <param name="search">需要替换掉的子串</param>
    /// <param name="replace">需要替换为的内容</param>
    /// <param name="comparisonType">comparisonType</param>
    /// <returns></returns>
    public static string ReplaceFirst(this string str, string search, string replace,
        StringComparison comparisonType = StringComparison.Ordinal)
    {
        ArgumentNullException.ThrowIfNull(str);

        var pos = str.IndexOf(search, comparisonType);
        if (pos < 0)
        {
            return str;
        }

        // 以下语句可以替换为：return string.Concat(str.AsSpan(0, pos), replace, str.AsSpan(pos + search.Length));
        return str.Substring(0, pos) + replace + str.Substring(pos + search.Length);
    }

    /// <summary>
    /// 从给定的字符串右侧开始，截取指定长度的子串。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
    public static string Right(this string str, int len)
    {
        ArgumentNullException.ThrowIfNull(str, nameof(str));

        if (str.Length < len)
        {
            throw new ArgumentException($"参数 {nameof(len)} 不能比给定的字符串的长度大！");
        }

        return str.Substring(str.Length - len, len);
    }

    /// <summary>
    /// 按指定的分隔符分割给定的字符串。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string[] Split(this string str, string separator)
    {
        return str.Split(new[] { separator }, StringSplitOptions.None);
    }

    /// <summary>
    /// 按指定的分隔符分割给定的字符串。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="separator"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string[] Split(this string str, string separator, StringSplitOptions options)
    {
        return str.Split(new[] { separator }, options);
    }

    /// <summary>
    /// 使用换行符 <see cref="Environment.NewLine"/> 分割字符串。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] SplitToLines(this string str)
    {
        return str.Split(Environment.NewLine);
    }

    /// <summary>
    /// 使用换行符 <see cref="Environment.NewLine"/> 分割字符串。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string[] SplitToLines(this string str, StringSplitOptions options)
    {
        return str.Split(Environment.NewLine, options);
    }

    /// <summary>
    /// 将 PascalCase 格式的字符串转换为 camelCase 格式。
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    /// <param name="handleAbbreviations">set true to if you want to convert 'XYZ' to 'xyz'.</param>
    /// <returns>camelCase of the string</returns>
    public static string ToCamelCase(this string str, bool useCurrentCulture = false, bool handleAbbreviations = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        if (handleAbbreviations && IsAllUpperCase(str))
        {
            return useCurrentCulture ? str.ToLower() : str.ToLowerInvariant();
        }

        return (useCurrentCulture ? char.ToLower(str[0]) : char.ToLowerInvariant(str[0])) + str.Substring(1);
    }

    /// <summary>
    /// 将 camelCase 格式的字符串转换为 PascalCase 格式。
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    /// <returns>PascalCase of the string</returns>
    public static string ToPascalCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return useCurrentCulture ? str.ToUpper() : str.ToUpperInvariant();
        }

        return (useCurrentCulture ? char.ToUpper(str[0]) : char.ToUpperInvariant(str[0])) + str.Substring(1);
    }

    /// <summary>
    /// 将给定的 PascalCase/camelCase 格式的字符串，转换为一个长句。
    /// 例如："ThisIsSampleSentence" 将会被转换为 "This is a sample sentence"。
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    public static string ToSentenceCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return useCurrentCulture
            ? Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]))
            : Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLowerInvariant(m.Value[1]));
    }

    /// <summary>
    /// 将 PascalCase/camelCase 格式的字符串转换为 kebab-case 格式。
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="useCurrentCulture">set true to use current culture. Otherwise, invariant culture will be used.</param>
    public static string ToKebabCase(this string str, bool useCurrentCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        str = str.ToCamelCase();

        return useCurrentCulture
            ? Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLower(m.Value[1]))
            : Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + "-" + char.ToLowerInvariant(m.Value[1]));
    }

    /// <summary>
    /// 将 PascalCase/camelCase 格式的字符串转换为 snake_case 格式。
    /// 例如："ThisIsSampleSentence" 将会被转换为 "this_is_a_sample_sentence"。
    /// https://github.com/npgsql/npgsql/blob/dev/src/Npgsql/NameTranslation/NpgsqlSnakeCaseNameTranslator.cs#L51
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <returns></returns>
    public static string ToSnakeCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        var builder = new StringBuilder(str.Length + Math.Min(2, str.Length / 5));
        var previousCategory = default(UnicodeCategory?);

        for (var currentIndex = 0; currentIndex < str.Length; currentIndex++)
        {
            var currentChar = str[currentIndex];
            if (currentChar == '_')
            {
                builder.Append('_');
                previousCategory = null;
                continue;
            }

            var currentCategory = char.GetUnicodeCategory(currentChar);
            switch (currentCategory)
            {
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                    if (previousCategory == UnicodeCategory.SpaceSeparator ||
                        previousCategory == UnicodeCategory.LowercaseLetter ||
                        previousCategory != UnicodeCategory.DecimalDigitNumber &&
                        previousCategory != null &&
                        currentIndex > 0 &&
                        currentIndex + 1 < str.Length &&
                        char.IsLower(str[currentIndex + 1]))
                    {
                        builder.Append('_');
                    }

                    currentChar = char.ToLower(currentChar);
                    break;

                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.DecimalDigitNumber:
                    if (previousCategory == UnicodeCategory.SpaceSeparator)
                    {
                        builder.Append('_');
                    }

                    break;

                default:
                    if (previousCategory != null)
                    {
                        previousCategory = UnicodeCategory.SpaceSeparator;
                    }

                    continue;
            }

            builder.Append(currentChar);
            previousCategory = currentCategory;
        }

        return builder.ToString();
    }

    /// <summary>
    /// 将字符串转换为指定的枚举值。
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="value">String value to convert</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string value)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(value);
        return (T)Enum.Parse(typeof(T), value);
    }

    /// <summary>
    /// 将字符串转换为指定的枚举值，并指定是否忽略大小写。
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="value">String value to convert</param>
    /// <param name="ignoreCase">Ignore case</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string value, bool ignoreCase)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(value);
        return (T)Enum.Parse(typeof(T), value, ignoreCase);
    }

    /// <summary>
    /// 将字符串转换为 MD5 摘要。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToMd5(this string str)
    {
        using (var md5 = MD5.Create())
        {
            var inputBytes = Encoding.UTF8.GetBytes(str);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.Append(hashByte.ToString("X2"));
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// 从给定的字符串开头开始，截取指定长度的子串。
    /// </summary>
    public static string? Truncate(this string str, int maxLength)
    {
        if (str == null)
        {
            return null;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        return str.Left(maxLength);
    }

    /// <summary>
    /// 从给定的字符串尾部开始，截取指定长度的子串。
    /// </summary>
    public static string? TruncateFromEnding(this string str, int maxLength)
    {
        if (str == null)
        {
            return null;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        return str.Right(maxLength);
    }

    /// <summary>
    /// 从给定的字符串开头开始，截取指定长度的子串；截取后，将会在字符串尾部添加后缀 `...`。
    /// 返回的字符串的长度不能比参数 maxLength 大。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="maxLength"></param>
    /// <returns></returns>
    public static string? TruncateWithPostfix(this string str, int maxLength)
    {
        return TruncateWithPostfix(str, maxLength, "...");
    }

    /// <summary>
    /// 从给定的字符串开头开始，截取指定长度的子串；截取后，将会在字符串尾部添加指定的后缀。
    /// 返回的字符串的长度不能比参数 maxLength 大。
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string? TruncateWithPostfix(this string str, int maxLength, string postfix)
    {
        if (str == null)
        {
            return null;
        }

        if (str == string.Empty || maxLength == 0)
        {
            return string.Empty;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        if (maxLength <= postfix.Length)
        {
            return postfix.Left(maxLength);
        }

        return str.Left(maxLength - postfix.Length) + postfix;
    }

    /// <summary>
    /// 使用 <see cref="Encoding.UTF8"/> 编码格式，将给定的字符串转换为字节数组。
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] GetBytes(this string str)
    {
        return str.GetBytes(Encoding.UTF8);
    }

    /// <summary>
    /// 使用指定的编码格式，将给定的字符串转换为字节数组。
    /// </summary>
    /// <param name="str"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static byte[] GetBytes(this string str, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(str);
        ArgumentNullException.ThrowIfNull(encoding);

        return encoding.GetBytes(str);
    }

    /// <summary>
    /// 判断字符串是否都是由大写字母组成。
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static bool IsAllUpperCase(string input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
            {
                return false;
            }
        }

        return true;
    }
}