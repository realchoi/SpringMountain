namespace SpringMountain.Framework.Core.Domain.Auditing;

public interface IHasModificationTime
{
    /// <summary>
    /// 修改时间
    /// </summary>
    DateTime? ModificationTime { get; set; }
}