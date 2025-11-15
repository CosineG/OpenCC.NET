using JiebaNet.Segmenter;
using System;
using System.Collections.Generic;
using System.IO;

namespace OpenCCNET
{
    public static partial class ZhConverter
    {   
        /// <summary>
        /// 提供中文分词相关的功能实现
        /// </summary>
        public static class ZhSegment
        {
            private static SegmentMode _currentMode = SegmentMode.Jieba;
            private static Func<string, IEnumerable<string>> _segment = SegmentByJieba;

            /// <summary>
            /// 分词委托函数
            /// 设置此属性会自动切换到 Custom 模式
            /// </summary>
            public static Func<string, IEnumerable<string>> Segment
            {
                get => _segment;
                set
                {
                    _segment = value ?? throw new ArgumentNullException(nameof(value));
                    _currentMode = SegmentMode.Custom;
                }
            }

            /// <summary>
            /// jieba.NET分词器实例
            /// </summary>
            public static JiebaSegmenter Jieba = new JiebaSegmenter();

            /// <summary>
            /// 初始化分词器，加载必要的词典资源
            /// </summary>
            /// <param name="jiebaResourceDirectory">结巴分词资源目录路径</param>
            /// <param name="mode">分词模式</param>
            /// <exception cref="DirectoryNotFoundException">当指定的资源目录不存在时抛出</exception>
            public static void Initialize(string jiebaResourceDirectory = "JiebaResource", 
                SegmentMode mode = SegmentMode.Jieba)
            {
                SetMode(mode, jiebaResourceDirectory);
            }

            /// <summary>
            /// 设置分词模式
            /// </summary>
            /// <param name="mode">分词模式</param>
            /// <param name="jiebaResourceDirectory">结巴分词资源目录路径（仅在切换到Jieba模式时需要）</param>
            public static void SetMode(SegmentMode mode, string jiebaResourceDirectory = "JiebaResource")
            {
                _currentMode = mode;

                switch (mode)
                {
                    case SegmentMode.Jieba:
                        if (!Directory.Exists(jiebaResourceDirectory))
                        {
                            throw new DirectoryNotFoundException($"结巴分词资源目录不存在：{jiebaResourceDirectory}");
                        }

                        ConfigManager.ConfigFileBaseDir = jiebaResourceDirectory;
                        try
                        {
                            // 通过调用一次jieba分词来提前加载所需资源
                            Jieba.Cut(string.Empty);
                        }
                        catch (Exception ex)
                        {
                            throw new InvalidOperationException("初始化结巴分词器失败", ex);
                        }

                        _segment = SegmentByJieba;
                        break;

                    case SegmentMode.MaxMatch:
                        _segment = NoSegmentImpl;
                        break;

                    case SegmentMode.Custom:
                        // Custom模式：默认使用最大匹配实现
                        if (_segment == SegmentByJieba || _segment == NoSegmentImpl)
                        {
                            _segment = NoSegmentImpl;
                        }
                        // 否则保持用户已设置的自定义分词函数
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
                }
            }

            /// <summary>
            /// 设置自定义分词函数
            /// </summary>
            /// <param name="customSegment">自定义分词委托</param>
            public static void SetCustomSegment(Func<string, IEnumerable<string>> customSegment)
            {
                _segment = customSegment ?? throw new ArgumentNullException(nameof(customSegment));
                _currentMode = SegmentMode.Custom;
            }

            /// <summary>
            /// 获取当前分词模式
            /// </summary>
            public static SegmentMode CurrentMode => _currentMode;

            /// <summary>
            /// 使用结巴分词进行文本分词
            /// </summary>
            /// <param name="text">待分词的文本</param>
            /// <returns>分词结果序列</returns>
            private static IEnumerable<string> SegmentByJieba(string text)
            {
                return string.IsNullOrWhiteSpace(text) ? new List<string> {text} : Jieba.Cut(text);
            }

            /// <summary>
            /// 不分词，返回原始文本（用于最大匹配算法）
            /// </summary>
            /// <param name="text">待处理的文本</param>
            /// <returns>包含原始文本的序列</returns>
            private static IEnumerable<string> NoSegmentImpl(string text)
            {
                return new List<string> { string.IsNullOrWhiteSpace(text) ? string.Empty : text};
            }

            /// <summary>
            /// 重置分词器到默认状态
            /// </summary>
            public static void ResetSegment()
            {
                _segment = SegmentByJieba;
                Jieba = new JiebaSegmenter();
                _currentMode = SegmentMode.Jieba;
            }
        }
    }
}