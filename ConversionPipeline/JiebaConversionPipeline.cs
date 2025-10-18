using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace OpenCCNET.ConversionPipeline
{
    /// <summary>
    /// 用于结巴分词的转换管道
    /// </summary>
    public class JiebaConversionPipeline : IConversionPipeline
    {
        private readonly List<string> _phrases;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text">待转换的文本</param>
        public JiebaConversionPipeline(string text)
        {
            // 只分词一次
            _phrases = ZhConverter.ZhSegment.Segment(text).ToList();
        }

        /// <summary>
        /// 内部构造函数，用于链式调用
        /// </summary>
        /// <param name="phrases">已分词的词组</param>
        private JiebaConversionPipeline(List<string> phrases)
        {
            _phrases = phrases;
        }

        /// <summary>
        /// 使用字典进行转换
        /// </summary>
        public IConversionPipeline ConvertBy(params IDictionary<string, string>[] dictionaries)
        {
            if (dictionaries is not { Length: > 0 })
            {
                return this;
            }

            var phrasesList = _phrases is IList<string> tempList ? tempList : _phrases.ToList();

            List<string> result;
            if (ZhConverter.IsParallelEnabled)
            {
                result = phrasesList.AsParallel()
                    .AsOrdered()
                    .Select(phrase => ConvertPhrase(phrase, dictionaries))
                    .ToList();
            }
            else
            {
                result = phrasesList.Select(phrase => ConvertPhrase(phrase, dictionaries)).ToList();
            }

            return new JiebaConversionPipeline(result);
        }

        /// <summary>
        /// 获取最终转换结果
        /// </summary>
        public string GetResult()
        {
            return string.Join("", _phrases);
        }

        /// <summary>
        /// 转换单个词组
        /// </summary>
        private static string ConvertPhrase(string phrase, params IDictionary<string, string>[] dictionaries)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return phrase;
            }

            // 整词转换：按字典顺序优先匹配
            foreach (var dictionary in dictionaries)
            {
                if (dictionary != null && dictionary.TryGetValue(phrase, out var converted))
                {
                    return converted;
                }
            }

            // 逐字转换：如果整词未匹配
            var textElements = StringInfo.GetTextElementEnumerator(phrase);
            var resultBuilder = new StringBuilder(phrase.Length * 2);
            var hasConversion = false;

            while (textElements.MoveNext())
            {
                var textElement = textElements.GetTextElement();
                var convertedElement = textElement;
                
                foreach (var dictionary in dictionaries)
                {
                    if (dictionary != null && dictionary.TryGetValue(textElement, out var converted))
                    {
                        convertedElement = converted;
                        hasConversion = true;
                        break;
                    }
                }

                resultBuilder.Append(convertedElement);
            }

            return hasConversion ? resultBuilder.ToString() : phrase;
        }
    }
}
