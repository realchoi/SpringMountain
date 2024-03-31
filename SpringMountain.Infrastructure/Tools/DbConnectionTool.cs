using System.Text.RegularExpressions;
using Npgsql;

namespace SpringMountain.Infrastructure.Tools;

/// <summary>
/// 数据库连接工具类
/// </summary>
public static class DbConnectionTool
{
    /// <summary>
    /// 需要转义的字符
    /// </summary>
    private static readonly List<char> SpecialCharacters = new List<char>
    {
        '%', '$', '@', '(', ')', ';', '?', '*', '#'
    };

    /// <summary>
    /// 获取可直接用来初始化 <see cref="NpgsqlConnection"/> 对象的数据库连接字符串（处理转义字符）
    /// </summary>
    /// <remarks>Modified on 2023-07-21：增加了对用户名和密码中转义字符的处理</remarks>
    /// <param name="dataSourceConnectString">数据库连接，形如：postgres://username:password@server:port/database+undefined</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">若传入的连接字符串不是 postgres 协议，则会抛出不合法的参数异常</exception>
    public static string GetPgSqlConnStr(string dataSourceConnectString)
    {
        if (dataSourceConnectString.IsNullOrEmpty())
        {
            throw new ArgumentException("连接字符串不能为空");
        }

        // 正则匹配捕获用户名和密码信息
        const string pattern = @"\/\/(\w+\:.+)@";
        var match = Regex.Match(dataSourceConnectString, pattern);
        if (!match.Success)
        {
            throw new ArgumentException("连接字符串格式不正确");
        }

        // 原始用户名和密码信息
        var originalUserInfo = match.Groups[1].Value;
        var originalUserInfoSplit = originalUserInfo.Split(":");
        var originalUser = originalUserInfoSplit[0];
        var originalPassword = originalUserInfoSplit[1];
        // 如果用户名和密码中包含需要转义的字符，则进行转义
        var escapedUser = originalUser.Any(c => SpecialCharacters.Contains(c))
            ? Uri.EscapeDataString(originalUser)
            : originalUser;
        var escapedPassword = originalPassword.Any(c => SpecialCharacters.Contains(c))
            ? Uri.EscapeDataString(originalPassword)
            : originalPassword;
        var escapedUserInfo = $"{escapedUser}:{escapedPassword}";
        var escapedConnString = dataSourceConnectString.Replace(originalUserInfo, escapedUserInfo);

        // var uri = new Uri(escapedConnString);
        if (!Uri.TryCreate(escapedConnString, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException("连接字符串填写不正确");
        }

        if (!uri.Scheme.Equals("postgres", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new ArgumentException("当前只支持 postgres 数据库");
        }

        // 如果原始用户名和密码中包含需要转义的字符，说明上面的逻辑中已经对其进行了转义
        // 所以这里传入到 NpgsqlConnectionStringBuilder 时需要反转义
        var userInfo = uri.UserInfo.Split(":");
        var user = originalUser.Any(c => SpecialCharacters.Contains(c))
            ? Uri.UnescapeDataString(userInfo[0])
            : userInfo[0];
        var password = originalPassword.Any(c => SpecialCharacters.Contains(c))
            ? Uri.UnescapeDataString(userInfo[1])
            : userInfo[1];

        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = uri.Host,
            Port = uri.Port,
            Username = user,
            Password = password,
            Database = uri.AbsolutePath.TrimStart('/').Split('+').First()
        };
        return connectionStringBuilder.ConnectionString;
    }
}