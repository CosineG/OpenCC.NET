namespace OpenCCNET;

/// <summary>
/// 分词模式
/// </summary>
public enum SegmentMode
{
    /// <summary>
    /// 使用结巴分词
    /// </summary>
    Jieba,
    /// <summary>
    /// 使用最大正向匹配算法（转换时再分词）
    /// </summary>
    MaxMatch,
    /// <summary>
    /// 使用自定义分词算法（默认使用最大匹配）
    /// </summary>
    Custom
}