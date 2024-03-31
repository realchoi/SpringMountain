namespace SpringMountain.Framework.Core.Domain.Auditing;

public interface IHasCreationUser
{
    /// <summary>
    /// 创建用户
    /// </summary>
    public string CreationUser { get; set; }
}