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
                    foreach (var character in phrase)
                    {
                        if (ZhDictionary.STCharacters.ContainsKey(character.ToString()))
                        {
                            textBuilder.Append(ZhDictionary.STCharacters[character.ToString()]);
                        }
                        else
                        {
                            textBuilder.Append(character.ToString());
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
                    foreach (var character in phrase)
                    {
                        if (ZhDictionary.STCharacters.ContainsKey(character.ToString()))
                        {
                            phraseBuilder.Append(ZhDictionary.STCharacters[character.ToString()]);
                        }
                        else
                        {
                            phraseBuilder.Append(character.ToString());
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
                    foreach (var character in phrase)
                    {
                        if (ZhDictionary.STCharacters.ContainsKey(character.ToString()))
                        {
                            textBuilder.Append(ZhDictionary.STCharacters[character.ToString()]);
                        }
                        else
                        {
                            textBuilder.Append(character.ToString());
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
        /// 繁体中文（OpenCC标准）=>简体中文
        /// </summary>
        public static string HantToHans(string text)
        {
            return text;
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>简体中文
        /// </summary>
        public static string ToHansFromHant(this string text)
        {
            return HansToHant(text);
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（台湾）
        /// </summary>
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string HantToTW(string text, bool replacePhrases = false)
        {
            return text;
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（台湾）
        /// </summary>
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string ToTWFromHant(this string text, bool replacePhrases = false)
        {
            return HantToTW(text);
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（香港）
        /// </summary>
        public static string HantToHK(string text)
        {
            return text;
        }

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（香港）
        /// </summary>
        public static string ToHKFromHant(this string text)
        {
            return HantToHK(text);
        }

        /// <summary>
        /// 繁体中文（台湾）=>繁体中文（OpenCC标准）
        /// </summary>
        /// <param name="replacePhrases">是否转换地区词汇</param>
        public static string TWToHant(string text, bool replacePhrases = false)
        {
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
            return text;
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
            return text;
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
            return text;
        }

        /// <summary>
        /// 繁体中文（香港）=>简体中文
        /// </summary>
        public static string ToHansFromHK(this string text)
        {
            return HKToHans(text);
        }
    }
}