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
            /// <summary>
            /// 分词委托函数，默认使用结巴分词实现
            /// </summary>
            public static Func<string, IEnumerable<string>> Segment = SegmentByJieba;

            /// <summary>
            /// jieba.NET分词器实例
            /// </summary>
            public static JiebaSegmenter Jieba = new JiebaSegmenter();

            /// <summary>
            /// 初始化分词器，加载必要的词典资源
            /// </summary>
            /// <param name="jiebaResourceDirectory">结巴分词资源目录路径</param>
            /// <exception cref="DirectoryNotFoundException">当指定的资源目录不存在时抛出</exception>
            public static void Initialize(string jiebaResourceDirectory = "JiebaResource")
            {
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
            }

            /// <summary>
            /// 使用结巴分词进行文本分词
            /// </summary>
            /// <param name="text">待分词的文本</param>
            /// <returns>分词结果序列</returns>
            private static IEnumerable<string> SegmentByJieba(string text)
            {
                if (string.IsNullOrWhiteSpace(text))
                {
                    return [text];
                }

                return Jieba.Cut(text);
            }

            /// <summary>
            /// 重置分词器到默认状态
            /// </summary>
            public static void ResetSegment()
            {
                Segment = SegmentByJieba;
                Jieba = new JiebaSegmenter();
            }
        }
    }
}