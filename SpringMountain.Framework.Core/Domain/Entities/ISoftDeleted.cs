namespace SpringMountain.Framework.Core.Domain.Entities;

/// <summary>
/// 实体软删除
/// </summary>
public interface ISoftDeleted
{
    /// <summary>
    /// 是否删除
    /// </summary>
    public bool IsDeleted { get; set; }
}