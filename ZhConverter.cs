using System.Linq;
using System.Text;

namespace OpenCCNET
{
    public static partial class ZhConverter
    {
        /// <summary>
        /// 简体中文=>繁体中文（OpenCC标准）
        /// </summary>
        public static string HansToHant(string text)
        {
            var phrases = ZhUtil.Segment(text);
            var textBuilder = new StringBuilder(text.Length * 2);
            // 转换词汇(包含单字)
            textBuilder.ConvertPhrase(phrases, ZhDictionary.STCharacters, ZhDictionary.STPhrases);
            return textBuilder.ToString();
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
            var phrases = ZhUtil.Segment(text);
            var textBuilder = new StringBuilder(text.Length * 2);
            // 转换词汇(常用词替换可选)
            textBuilder.ConvertPhraseAndIdiom(phrases, ZhDictionary.STCharacters,
                ZhDictionary.STPhrases, ZhDictionary.TWPhrases, isIdiomConvert);
            // 转换为台湾字形
            textBuilder.ConvertVariant(ZhDictionary.TWVariants);

            return textBuilder.ToString();
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
            var phrases = ZhUtil.Segment(text);
            var textBuilder = new StringBuilder(text.Length * 2);
            textBuilder.ConvertPhrase(phrases, ZhDictionary.STCharacters, ZhDictionary.STPhrases);
            textBuilder.ConvertVariant(ZhDictionary.HKVariants);
            return textBuilder.ToString();
        }

        /// <summary>
        /// 简体中文=>繁体中文（香港）
        /// </summary>
        public static string ToHKFromHans(this string text)
        {
            return HansToHK(text);
        }

        /// <summary>
        /// 简体中文=>繁体中文（中国大陆）
        /// </summary>
        public static string HansToCN(string text)
        {
            var phrases = ZhUtil.Segment(text);
            var textBuilder = new StringBuilder(text.Length * 2);
            textBuilder.ConvertPhrase(phrases, ZhDictionary.STCharacters, ZhDictionary.STPhrases);
            textBuilder.ConvertVariant(ZhDictionary.CNVariants);
            return textBuilder.ToString();
        }

        /// <summary>
        /// 简体中文=>繁体中文（中国大陆）
        /// </summary>
        public static string ToCNFromHans(this string text)
        {
            return HansToCN(text);
        }


        /// <summary>
        /// 繁体中文（OpenCC标准）=>简体中文
        /// </summary>
        public static string HantToHans(string text)
        {
            var phrases = ZhUtil.Segment(text);
            var textBuilder = new StringBuilder(text.Length * 2);
            textBuilder.ConvertPhrase(phrases, ZhDictionary.TSCharacters, ZhDictionary.TSPhrases);
            return textBuilder.ToString();
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
            var phrases = ZhUtil.Segment(text);
            StringBuilder textBuilder = new StringBuilder(text.Length * 2);
            textBuilder.ConvertIdiom(phrases, ZhDictionary.TWPhrases, isIdiomConvert);
            textBuilder.ConvertVariant(ZhDictionary.TWVariants);

            return textBuilder.ToString();
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
            var textBuilder = new StringBuilder(text);
            textBuilder.ConvertVariant(ZhDictionary.HKVariants);
            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（香港）
        /// </summary>
        public static string ToHKFromHant(this string text)
        {
            return HantToHK(text);
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（中国大陆）
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string HantToCN(string text, bool isIdiomConvert = false)
        {
            var phrases = ZhUtil.Segment(text);
            StringBuilder textBuilder = new StringBuilder(text.Length * 2);
            textBuilder.ConvertIdiom(phrases, ZhDictionary.TWPhrases, isIdiomConvert);
            textBuilder.ConvertVariant(ZhDictionary.CNVariants);
            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（中国大陆）
        /// </summary>
        public static string ToCNFromHant(this string text)
        {
            return HantToCN(text);
        }

        /// <summary>
        /// 繁体中文（台湾）=>繁体中文（OpenCC标准）
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string TWToHant(string text, bool isIdiomConvert = false)
        {
            var textBuilder = new StringBuilder(text);
            // 字形转回OpenCC标准
            textBuilder.ConvertVariant(ZhDictionary.TWVariantsReversed);
            text = textBuilder.ToString();
            var phrases = ZhUtil.Segment(text);
            textBuilder.ConvertIdiom(phrases, ZhDictionary.TWPhrases, isIdiomConvert);
            return text;
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
            var textBuilder = new StringBuilder(text, text.Length * 2);
            textBuilder.ConvertVariant(ZhDictionary.TWVariantsReversed);
            text = textBuilder.ToString();
            var phrases = ZhUtil.Segment(text);
            // 先替换常用词，再转换
            textBuilder.ConvertPhraseAndIdiomReverse(phrases, ZhDictionary.TSCharacters, ZhDictionary.TSPhrases,
                ZhDictionary.TWPhrasesReversed, isIdiomConvert);
            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（台湾）=>简体中文
        /// </summary>
        /// <param name="isIdiomConvert">是否转换地区词汇</param>
        public static string ToHansFromTW(this string text, bool isIdiomConvert = false)
        {
            return TWToHans(text, isIdiomConvert);
        }

        /// <summary>
        /// 繁体中文（香港）=>繁体中文（OpenCC标准）
        /// </summary>
        public static string HKToHant(string text)
        {
            var textBuilder = new StringBuilder(text, text.Length * 2);
            textBuilder.ConvertVariant(ZhDictionary.HKVariantsReversed);
            return textBuilder.ToString();
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
            var textBuilder = new StringBuilder(text, text.Length * 2);
            textBuilder.ConvertVariant(ZhDictionary.HKVariantsReversed);
            text = textBuilder.ToString();
            var phrases = ZhUtil.Segment(text);
            textBuilder.ConvertPhrase(phrases, ZhDictionary.TSCharacters, ZhDictionary.TSPhrases);

            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（香港）=>简体中文
        /// </summary>
        public static string ToHansFromHK(this string text)
        {
            return HKToHans(text);
        }


        /// <summary>
        /// 繁体中文（中国大陆）=>繁体中文（OpenCC标准）
        /// </summary>
        public static string CNToHant(string text)
        {
            var textBuilder = new StringBuilder(text, text.Length * 2);
            textBuilder.ConvertVariant(ZhDictionary.CNVariantsReversed);
            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（中国大陆）=>繁体中文（OpenCC标准）
        /// </summary>
        public static string ToHantFromCN(this string text)
        {
            return CNToHant(text);
        }

        /// <summary>
        /// 繁体中文（中国大陆）=>简体中文
        /// </summary>
        public static string CNToHans(string text)
        {
            var textBuilder = new StringBuilder(text, text.Length * 2);
            textBuilder.ConvertVariant(ZhDictionary.CNVariantsReversed);
            text = textBuilder.ToString();
            var phrases = ZhUtil.Segment(text);
            textBuilder.ConvertPhrase(phrases, ZhDictionary.TSCharacters, ZhDictionary.TSPhrases);
            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（中国大陆）=>简体中文
        /// </summary>
        public static string ToHansFromCN(this string text)
        {
            return CNToHans(text);
        }

        public static void Initialize()
        {
            HansToHant("");
        }
    }
}