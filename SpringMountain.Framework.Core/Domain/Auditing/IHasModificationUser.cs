namespace SpringMountain.Framework.Core.Domain.Auditing;

public interface IHasModificationUser
{
    /// <summary>
    /// 修改用户
    /// </summary>
    public string ModificationUser { get; set; }
}