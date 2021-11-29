using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCCNET.Dictionary;

namespace OpenCCNET
{
    public static class ZhConverter
    {
        /// <summary>
        /// 简体中文=>繁体中文（OpenCC标准）
        /// </summary>
        public static string HansToHant(string text)
        {
            var phrases = ZhUtil.Segment(text);
            var textBuilder = new StringBuilder(text.Length * 2);
            foreach (var phrase in phrases)
            {
                // 整词转换
                if (ZhDictionary.STPhrases.ContainsKey(phrase))
                {
                    textBuilder.Append(ZhDictionary.STPhrases[phrase]);
                }
                else
                {
                    // 逐字转换
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (ZhDictionary.STCharacters.ContainsKey(character))
                        {
                            textBuilder.Append(ZhDictionary.STCharacters[character]);
                        }
                        else
                        {
                            textBuilder.Append(character);
                        }
                    }
                }
            }

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
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string HansToTW(string text, bool replacePhrases = false)
        {
            var phrases = ZhUtil.Segment(text);
            var textBuilder = new StringBuilder(text.Length * 2);
            var phraseBuilder = new StringBuilder(10);
            foreach (var phrase in phrases)
            {
                string hantPhrase;
                if (ZhDictionary.STPhrases.ContainsKey(phrase))
                {
                    hantPhrase = ZhDictionary.STPhrases[phrase];
                }
                else
                {
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (ZhDictionary.STCharacters.ContainsKey(character))
                        {
                            phraseBuilder.Append(ZhDictionary.STCharacters[character]);
                        }
                        else
                        {
                            phraseBuilder.Append(character);
                        }
                    }

                    hantPhrase = phraseBuilder.ToString();
                    phraseBuilder.Clear();
                }

                // 转换为台湾地区常用词汇
                if (replacePhrases && ZhDictionary.TWPhrases.ContainsKey(hantPhrase))
                {
                    textBuilder.Append(ZhDictionary.TWPhrases[hantPhrase]);
                }
                else
                {
                    textBuilder.Append(hantPhrase);
                }
            }

            // 转换为台湾字形
            foreach (var variant in ZhDictionary.TWVariants.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.TWVariants[variant]);
            }

            return textBuilder.ToString();
        }

        /// <summary>
        /// 简体中文=>繁体中文（台湾）
        /// </summary>
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string ToTWFromHans(this string text, bool replacePhrases = false)
        {
            return HansToTW(text, replacePhrases);
        }

        /// <summary>
        /// 简体中文=>繁体中文（香港）
        /// </summary>
        public static string HansToHK(string text)
        {
            var phrases = ZhUtil.Segment(text);
            var textBuilder = new StringBuilder(text.Length * 2);
            foreach (var phrase in phrases)
            {
                if (ZhDictionary.STPhrases.ContainsKey(phrase))
                {
                    textBuilder.Append(ZhDictionary.STPhrases[phrase]);
                }
                else
                {
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (ZhDictionary.STCharacters.ContainsKey(character))
                        {
                            textBuilder.Append(ZhDictionary.STCharacters[character]);
                        }
                        else
                        {
                            textBuilder.Append(character);
                        }
                    }
                }
            }

            foreach (var variant in ZhDictionary.HKVariants.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.HKVariants[variant]);
            }

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
            foreach (var phrase in phrases)
            {
                if (ZhDictionary.STPhrases.ContainsKey(phrase))
                {
                    textBuilder.Append(ZhDictionary.STPhrases[phrase]);
                }
                else
                {
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (ZhDictionary.STCharacters.ContainsKey(character))
                        {
                            textBuilder.Append(ZhDictionary.STCharacters[character]);
                        }
                        else
                        {
                            textBuilder.Append(character);
                        }
                    }
                }
            }

            foreach (var variant in ZhDictionary.CNVariants.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.CNVariants[variant]);
            }

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
            foreach (var phrase in phrases)
            {
                // 整词转换
                if (ZhDictionary.TSPhrases.ContainsKey(phrase))
                {
                    textBuilder.Append(ZhDictionary.TSPhrases[phrase]);
                }
                else
                {
                    // 逐字转换
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (ZhDictionary.TSCharacters.ContainsKey(character))
                        {
                            textBuilder.Append(ZhDictionary.TSCharacters[character]);
                        }
                        else
                        {
                            textBuilder.Append(character);
                        }
                    }
                }
            }

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
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string HantToTW(string text, bool replacePhrases = false)
        {
            var phrases = ZhUtil.Segment(text);
            StringBuilder textBuilder;
            // 转换地区词汇
            if (replacePhrases)
            {
                textBuilder = new StringBuilder(text.Length * 2);
                foreach (var phrase in phrases)
                {
                    if (ZhDictionary.TWPhrases.ContainsKey(phrase))
                    {
                        textBuilder.Append(ZhDictionary.TWPhrases[phrase]);
                    }
                    else
                    {
                        textBuilder.Append(phrase);
                    }
                }
            }
            else
            {
                textBuilder = new StringBuilder(text);
            }

            // 转换为台湾字形
            foreach (var variant in ZhDictionary.TWVariants.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.TWVariants[variant]);
            }

            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（台湾）
        /// </summary>
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string ToTWFromHant(this string text, bool replacePhrases = false)
        {
            return HantToTW(text, replacePhrases);
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（香港）
        /// </summary>
        public static string HantToHK(string text)
        {
            var textBuilder = new StringBuilder(text);
            foreach (var variant in ZhDictionary.HKVariants.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.HKVariants[variant]);
            }

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
        public static string HantToCN(string text)
        {
            var textBuilder = new StringBuilder(text);
            foreach (var variant in ZhDictionary.CNVariants.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.CNVariants[variant]);
            }

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
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string TWToHant(string text, bool replacePhrases = false)
        {
            var textBuilder = new StringBuilder(text, text.Length * 2);

            // 字形转回OpenCC标准
            foreach (var variant in ZhDictionary.TWVariantsReversed.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.TWVariantsReversed[variant]);
            }

            text = textBuilder.ToString();
            textBuilder.Clear();

            // 转换地区词汇
            if (replacePhrases)
            {
                var phrases = ZhUtil.Segment(text);

                foreach (var phrase in phrases)
                {
                    if (ZhDictionary.TWPhrasesReversed.ContainsKey(phrase))
                    {
                        textBuilder.Append(ZhDictionary.TWPhrasesReversed[phrase]);
                    }
                    else
                    {
                        textBuilder.Append(phrase);
                    }
                }

                return textBuilder.ToString();
            }

            return text;
        }

        /// <summary>
        /// 繁体中文（台湾）=>繁体中文（OpenCC标准）
        /// </summary>
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string ToHantFromTW(this string text, bool replacePhrases = false)
        {
            return TWToHant(text, replacePhrases);
        }

        /// <summary>
        /// 繁体中文（台湾）=>简体中文
        /// </summary>
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string TWToHans(string text, bool replacePhrases = false)
        {
            var textBuilder = new StringBuilder(text, text.Length * 2);

            // 字形转回OpenCC标准
            foreach (var variant in ZhDictionary.TWVariantsReversed.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.TWVariantsReversed[variant]);
            }

            text = textBuilder.ToString();
            textBuilder.Clear();

            var phrases = ZhUtil.Segment(text);
            var phraseBuilder = new StringBuilder(10);
            foreach (var phrase in phrases)
            {
                // 转换地区词汇
                string hantPhrase;
                if (replacePhrases && ZhDictionary.TWPhrasesReversed.ContainsKey(phrase))
                {
                    hantPhrase = ZhDictionary.TWPhrasesReversed[phrase];
                }
                else
                {
                    hantPhrase = phrase;
                }

                // 转换至简体
                if (ZhDictionary.TSPhrases.ContainsKey(hantPhrase))
                {
                    textBuilder.Append(ZhDictionary.TSPhrases[hantPhrase]);
                }
                else
                {
                    foreach (var character in hantPhrase.Select(c => c.ToString()))
                    {
                        if (ZhDictionary.TSCharacters.ContainsKey(character))
                        {
                            phraseBuilder.Append(ZhDictionary.TSCharacters[character]);
                        }
                        else
                        {
                            phraseBuilder.Append(character);
                        }
                    }

                    textBuilder.Append(phraseBuilder.ToString());
                    phraseBuilder.Clear();
                }
            }

            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（台湾）=>简体中文
        /// </summary>
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string ToHansFromTW(this string text, bool replacePhrases = false)
        {
            return TWToHans(text, replacePhrases);
        }

        /// <summary>
        /// 繁体中文（香港）=>繁体中文（OpenCC标准）
        /// </summary>
        public static string HKToHant(string text)
        {
            var textBuilder = new StringBuilder(text, text.Length * 2);
            foreach (var variant in ZhDictionary.HKVariantsReversed.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.HKVariantsReversed[variant]);
            }

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
            // 字形转回OpenCC标准
            foreach (var variant in ZhDictionary.HKVariantsReversed.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.HKVariantsReversed[variant]);
            }

            text = textBuilder.ToString();
            textBuilder.Clear();

            // 转换至简体
            var phrases = ZhUtil.Segment(text);
            var phraseBuilder = new StringBuilder(10);
            foreach (var phrase in phrases)
            {
                if (ZhDictionary.TSPhrases.ContainsKey(phrase))
                {
                    textBuilder.Append(ZhDictionary.TSPhrases[phrase]);
                }
                else
                {
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (ZhDictionary.TSCharacters.ContainsKey(character))
                        {
                            phraseBuilder.Append(ZhDictionary.TSCharacters[character]);
                        }
                        else
                        {
                            phraseBuilder.Append(character);
                        }
                    }

                    textBuilder.Append(phraseBuilder.ToString());
                    phraseBuilder.Clear();
                }
            }

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
            foreach (var variant in ZhDictionary.CNVariantsReversed.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.CNVariantsReversed[variant]);
            }

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
            // 字形转回OpenCC标准
            foreach (var variant in ZhDictionary.CNVariantsReversed.Keys)
            {
                textBuilder.Replace(variant, ZhDictionary.CNVariantsReversed[variant]);
            }

            text = textBuilder.ToString();
            textBuilder.Clear();

            // 转换至简体
            var phrases = ZhUtil.Segment(text);
            var phraseBuilder = new StringBuilder(10);
            foreach (var phrase in phrases)
            {
                if (ZhDictionary.TSPhrases.ContainsKey(phrase))
                {
                    textBuilder.Append(ZhDictionary.TSPhrases[phrase]);
                }
                else
                {
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (ZhDictionary.TSCharacters.ContainsKey(character))
                        {
                            phraseBuilder.Append(ZhDictionary.TSCharacters[character]);
                        }
                        else
                        {
                            phraseBuilder.Append(character);
                        }
                    }

                    textBuilder.Append(phraseBuilder.ToString());
                    phraseBuilder.Clear();
                }
            }

            return textBuilder.ToString();
        }

        /// <summary>
        /// 繁体中文（中国大陆）=>简体中文
        /// </summary>
        public static string ToHansFromCN(this string text)
        {
            return CNToHans(text);
        }
    }
}