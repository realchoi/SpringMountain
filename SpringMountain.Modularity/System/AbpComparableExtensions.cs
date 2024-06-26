﻿namespace System;

/// <summary>
/// <see cref="IComparable{T}"/> 扩展方法
/// </summary>
public static class AbpComparableExtensions
{
    /// <summary>
    /// 检查给定的值是否在指定的最小值和最大值之间。
    /// </summary>
    /// <param name="value">The value to be checked</param>
    /// <param name="minInclusiveValue">Minimum (inclusive) value</param>
    /// <param name="maxInclusiveValue">Maximum (inclusive) value</param>
    public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
    {
        return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
    }
}