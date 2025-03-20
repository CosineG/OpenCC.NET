using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace OpenCCNET
{
    public static partial class ZhConverter
    {
        /// <summary>
        /// 是否启用并行处理
        /// </summary>
        public static bool IsParallelEnabled { get; set; } = false;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dictionaryDirectory">字典文件夹路径</param>
        /// <param name="jiebaResourceDirectory">Jieba.NET资源路径</param>
        /// <param name="isParallelEnabled">是否启用并行处理。默认为 false</param>
        public static void Initialize(string dictionaryDirectory = "Dictionary",
            string jiebaResourceDirectory = "JiebaResource",
            bool isParallelEnabled = false)
        {
            ZhSegment.Initialize(jiebaResourceDirectory);
            ZhDictionary.Initialize(dictionaryDirectory);
            IsParallelEnabled = isParallelEnabled;
        }

        #region 简体中文

        /// <summary>
        /// 简体中文=>繁体中文（OpenCC标准）
        /// </summary>
        public static string HansToHant(string text)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.STPhrases, ZhDictionary.STCharacters)
                .Join();
        }

        /// <summary>
        /// 简体中文=>繁体中文（OpenCC标准）
        /// </summary>
        public static string ToHantFromHans(this string text)
        {
            return HansToHant(text);
        }


        /// <summary>
        /// 简体中文=>繁体中文（台湾）
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string HansToTW(string text, bool isIdiomConvert = false)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.STPhrases, ZhDictionary.STCharacters)
                    .ConvertBy(isIdiomConvert ? ZhDictionary.TWPhrases : null)
                    .ConvertBy(ZhDictionary.TWVariants)
                    .Join();
        }

        /// <summary>
        /// 简体中文=>繁体中文（台湾）
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string ToTWFromHans(this string text, bool isIdiomConvert = false)
        {
            return HansToTW(text, isIdiomConvert);
        }

        /// <summary>
        /// 简体中文=>繁体中文（香港）
        /// </summary>
        public static string HansToHK(string text)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.STPhrases, ZhDictionary.STCharacters)
                .ConvertBy(ZhDictionary.HKVariants)
                .Join();
        }

        /// <summary>
        /// 简体中文=>繁体中文（香港）
        /// </summary>
        public static string ToHKFromHans(this string text)
        {
            return HansToHK(text);
        }

        #endregion

        #region 繁体中文（OpenCC标准）

        /// <summary>
        /// 繁体中文（OpenCC标准）=>简体中文
        /// </summary>
        public static string HantToHans(string text)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.TSPhrases, ZhDictionary.TSCharacters)
                .Join();
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>简体中文
        /// </summary>
        public static string ToHansFromHant(this string text)
        {
            return HantToHans(text);
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（台湾）
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string HantToTW(string text, bool isIdiomConvert = false)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(isIdiomConvert ? ZhDictionary.TWPhrases : null)
                    .ConvertBy(ZhDictionary.TWVariants)
                    .Join();
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（台湾）
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string ToTWFromHant(this string text, bool isIdiomConvert = false)
        {
            return HantToTW(text, isIdiomConvert);
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（香港）
        /// </summary>
        public static string HantToHK(string text)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.HKVariants)
                .Join();
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（香港）
        /// </summary>
        public static string ToHKFromHant(this string text)
        {
            return HantToHK(text);
        }

        #endregion

        #region 繁体中文（台湾）

        /// <summary>
        /// 繁体中文（台湾）=>繁体中文（OpenCC标准）
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string TWToHant(string text, bool isIdiomConvert = false)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.TWVariantsRev, ZhDictionary.TWVariantsRevPhrases)
                    .ConvertBy(isIdiomConvert ? ZhDictionary.TWPhrasesRev : null)
                    .Join();
        }

        /// <summary>
        /// 繁体中文（台湾）=>繁体中文（OpenCC标准）
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string ToHantFromTW(this string text, bool isIdiomConvert = false)
        {
            return TWToHant(text, isIdiomConvert);
        }

        /// <summary>
        /// 繁体中文（台湾）=>简体中文
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string TWToHans(string text, bool isIdiomConvert = false)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.TWVariantsRev, ZhDictionary.TWVariantsRevPhrases)
                    .ConvertBy(isIdiomConvert ? ZhDictionary.TWPhrasesRev : null)
                    .ConvertBy(ZhDictionary.TSPhrases, ZhDictionary.TSCharacters)
                    .Join();
        }

        /// <summary>
        /// 繁体中文（台湾）=>简体中文
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string ToHansFromTW(this string text, bool isIdiomConvert = false)
        {
            return TWToHans(text, isIdiomConvert);
        }

        #endregion

        #region 繁体中文（香港）

        /// <summary>
        /// 繁体中文（香港）=>繁体中文（OpenCC标准）
        /// </summary>
        public static string HKToHant(string text)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.HKVariantsRevPhrases, ZhDictionary.HKVariantsRev)
                .Join();
        }

        /// <summary>
        /// 繁体中文（香港）=>繁体中文（OpenCC标准）
        /// </summary>
        public static string ToHantFromHK(this string text)
        {
            return HKToHant(text);
        }

        /// <summary>
        /// 繁体中文（香港）=>简体中文
        /// </summary>
        public static string HKToHans(string text)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.HKVariantsRevPhrases, ZhDictionary.HKVariantsRev)
                .ConvertBy(ZhDictionary.TSPhrases, ZhDictionary.TSCharacters)
                .Join();
        }

        /// <summary>
        /// 繁体中文（香港）=>简体中文
        /// </summary>
        public static string ToHansFromHK(this string text)
        {
            return HKToHans(text);
        }

        #endregion

        #region 日语汉字

        /// <summary>
        /// 日语（旧字体）=>日语（新字体）
        /// </summary>
        public static string KyuuToShin(string text)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.JPVariants)
                .Join();
        }

        /// <summary>
        /// 日语（旧字体）=>日语（新字体）
        /// </summary>
        public static string ToShinFromKyuu(this string text)
        {
            return KyuuToShin(text);
        }

        /// <summary>
        /// 日语（新字体）=>日语（旧字体）
        /// </summary>
        public static string ShinToKyuu(string text)
        {
            var phrases = ZhSegment.Segment(text);
            return phrases.ConvertBy(ZhDictionary.JPShinjitaiPhrases, ZhDictionary.JPShinjitaiCharacters,
                    ZhDictionary.JPVariantsRev)
                .Join();
        }

        /// <summary>
        /// 日语（新字体）=>日语（旧字体）
        /// </summary>
        public static string ToKyuuFromShin(this string text)
        {
            return ShinToKyuu(text);
        }

        #endregion

        #region 扩展方法

        /// <summary>
        /// 利用字典进行转换
        /// </summary>
        /// <param name="phrases">词组</param>
        /// <param name="dictionaries">字典</param>
        /// <returns></returns>
        private static IEnumerable<string> ConvertBy(this IEnumerable<string> phrases,
            params IDictionary<string, string>[] dictionaries)
        {
            if (phrases == null)
            {
                throw new ArgumentNullException(nameof(phrases));
            }

            if (dictionaries is not { Length: > 0 })
            {
                return phrases;
            }

            var phrasesList = phrases is IList<string> tempList ? tempList : phrases.ToList();

            // 是否使用并行处理
            if (IsParallelEnabled)
            {
                return phrasesList.AsParallel()
                    .AsOrdered()
                    .Select(phrase => ConvertPhrase(phrase, dictionaries))
                    .ToList();
            }
            else
            {
                return phrasesList.Select(phrase => ConvertPhrase(phrase, dictionaries)).ToList();
            }
        }

        private static string ConvertPhrase(string phrase, params IDictionary<string, string>[] dictionaries)
        {
            if (string.IsNullOrEmpty(phrase))
            {
                return phrase;
            }

            // 整词转换
            foreach (var dictionary in dictionaries)
            {
                if (dictionary != null && dictionary.TryGetValue(phrase, out var converted))
                {
                    return converted;
                }
            }

            // 逐字转换（考虑多字节字符）
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

        /// <summary>
        /// 合并字符串组
        /// </summary>
        /// <param name="values">字符串组</param>
        /// <param name="separator">间隔符号，默认为无间隔</param>
        /// <returns></returns>
        private static string Join(this IEnumerable<string> values, string separator = "")
        {
            return string.Join(separator, values);
        }

        #endregion
    }
}