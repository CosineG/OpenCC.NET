using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCCNET.ConversionPipeline
{
    /// <summary>
    /// 基于最大匹配的转换管道
    /// 在转换链的每一步都根据传入的字典进行最大正向匹配
    /// </summary>
    public class MaxMatchConversionPipeline : IConversionPipeline
    {
        private const int MaxWordLength = 20; // 最大词长限制
        private string _text;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">待转换的文本</param>
        public MaxMatchConversionPipeline(string text)
        {
            _text = text ?? string.Empty;
        }

        /// <summary>
        /// 使用字典进行转换（最大正向匹配）
        /// </summary>
        public IConversionPipeline ConvertBy(params IDictionary<string, string>[] dictionaries)
        {
            if (dictionaries is not { Length: > 0 })
            {
                return this;
            }

            // 依次应用每个字典，每次都重新进行最大匹配
            foreach (var dictionary in dictionaries)
            {
                if (dictionary != null)
                {
                    _text = MaxMatchConvert(_text, dictionary);
                }
            }

            return this;
        }

        /// <summary>
        /// 获取最终转换结果
        /// </summary>
        public string GetResult()
        {
            return _text;
        }

        /// <summary>
        /// 使用最大正向匹配算法转换文本
        /// </summary>
        /// <param name="text">输入文本</param>
        /// <param name="dictionary">转换字典</param>
        /// <returns>转换后的文本</returns>
        private string MaxMatchConvert(string text, IDictionary<string, string> dictionary)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            var result = new StringBuilder(text.Length * 2);
            var i = 0;

            while (i < text.Length)
            {
                var matched = false;
                var maxLen = Math.Min(MaxWordLength, text.Length - i);

                // 从最长的可能匹配开始尝试（贪心策略）
                for (var len = maxLen; len > 0; len--)
                {
                    var substr = text.Substring(i, len);
                    
                    if (dictionary.TryGetValue(substr, out var converted))
                    {
                        result.Append(converted);
                        i += len;
                        matched = true;
                        break;
                    }
                }

                // 如果没有匹配到，保留原字符
                if (!matched)
                {
                    result.Append(text[i]);
                    i++;
                }
            }

            return result.ToString();
        }
    }
}
