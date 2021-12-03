using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCCNET
{
    public static partial class ZhConverter
    {
        /// <summary>
        /// 转换词汇
        /// </summary>
        /// <param name="textBuilder">存放结果的StringBuilder</param>
        /// <param name="phrases">分词后的词组</param>
        /// <param name="targetCharacters">目标单字词典</param>
        /// <param name="targetPhrases">目标词汇词典</param>
        private static void ConvertPhrase(this StringBuilder textBuilder, IEnumerable<string> phrases,
            Dictionary<string, string> targetCharacters, Dictionary<string, string> targetPhrases)
        {
            textBuilder.Clear();
            foreach (var phrase in phrases)
            {
                // 整词转换
                if (targetPhrases.ContainsKey(phrase))
                {
                    textBuilder.Append(targetPhrases[phrase]);
                }
                else
                {
                    // 逐字转换
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (targetCharacters.ContainsKey(character))
                        {
                            textBuilder.Append(targetCharacters[character]);
                        }
                        else
                        {
                            textBuilder.Append(character);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 转换词汇和地域常用词(先转换词汇，后替换常用词)
        /// </summary>
        /// <param name="textBuilder">存放结果的StringBuilder</param>
        /// <param name="phrases">分词后的词组</param>
        /// <param name="targetCharacters">目标单字词典</param>
        /// <param name="targetPhrases">目标词汇词典</param>
        /// <param name="targetIdiom">目标地域常用词汇词典</param>
        /// <param name="isIdiomConvert">是否转换地域常用词</param>
        private static void ConvertPhraseAndIdiom(this StringBuilder textBuilder,
            IEnumerable<string> phrases,
            Dictionary<string, string> targetCharacters, Dictionary<string, string> targetPhrases,
            Dictionary<string, string> targetIdiom, bool isIdiomConvert)
        {
            textBuilder.Clear();
            var phraseBuilder = new StringBuilder(10);
            foreach (var phrase in phrases)
            {
                string hantPhrase;
                if (targetPhrases.ContainsKey(phrase))
                {
                    hantPhrase = targetPhrases[phrase];
                }
                else
                {
                    foreach (var character in phrase.Select(c => c.ToString()))
                    {
                        if (targetCharacters.ContainsKey(character))
                        {
                            phraseBuilder.Append(targetCharacters[character]);
                        }
                        else
                        {
                            phraseBuilder.Append(character);
                        }
                    }

                    hantPhrase = phraseBuilder.ToString();
                    phraseBuilder.Clear();
                }

                // 转换为地区常用词汇
                if (isIdiomConvert && targetIdiom.ContainsKey(hantPhrase))
                {
                    textBuilder.Append(targetIdiom[hantPhrase]);
                }
                else
                {
                    textBuilder.Append(hantPhrase);
                }
            }
        }

        /// <summary>
        /// 转换词汇和地域常用词(先替换常用词，后转换词汇)
        /// </summary>
        /// <param name="textBuilder">存放结果的StringBuilder</param>
        /// <param name="phrases">分词后的词组</param>
        /// <param name="targetCharacters">目标单字词典</param>
        /// <param name="targetPhrases">目标词汇词典</param>
        /// <param name="targetIdiom">目标地域常用词汇词典</param>
        /// <param name="isIdiomConvert">是否转换地域常用词</param>
        private static void ConvertPhraseAndIdiomReverse(this StringBuilder textBuilder,
            IEnumerable<string> phrases,
            Dictionary<string, string> targetCharacters, Dictionary<string, string> targetPhrases,
            Dictionary<string, string> targetIdiom, bool isIdiomConvert)
        {
            textBuilder.Clear();
            var phraseBuilder = new StringBuilder(10);
            foreach (var phrase in phrases)
            {
                string hantPhrase;
                if (isIdiomConvert && targetIdiom.ContainsKey(phrase))
                {
                    hantPhrase = targetIdiom[phrase];
                }
                else
                {
                    hantPhrase = phrase;
                }
                
                if (targetPhrases.ContainsKey(hantPhrase))
                {
                    textBuilder.Append(targetPhrases[hantPhrase]);
                }
                else
                {
                    foreach (var character in hantPhrase.Select(c => c.ToString()))
                    {
                        if (targetCharacters.ContainsKey(character))
                        {
                            phraseBuilder.Append(targetCharacters[character]);
                        }
                        else
                        {
                            phraseBuilder.Append(character);
                        }
                    }

                    textBuilder.Append(phraseBuilder);
                    phraseBuilder.Clear();
                }
            }
        }

        /// <summary>
        /// 替换地域常用词
        /// </summary>
        /// <param name="textBuilder">存放结果的StringBuilder</param>
        /// <param name="phrases">分词后的词组</param>
        /// <param name="targetIdiom">目标地域常用词汇词典</param>
        /// <param name="isIdiomConvert">是否转换地域常用词</param>
        private static void ConvertIdiom(this StringBuilder textBuilder,
            IEnumerable<string> phrases,
            Dictionary<string, string> targetIdiom, bool isIdiomConvert)
        {
            if (!isIdiomConvert) return;
            textBuilder.Clear();
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

        /// <summary>
        /// 转换异体字
        /// </summary>
        /// <param name="textBuilder">存放结果的StringBuilder</param>
        /// <param name="targetVariants">目标异体字词典</param>
        private static void ConvertVariant(this StringBuilder textBuilder, Dictionary<string, string> targetVariants)
        {
            foreach (var variant in targetVariants.Keys)
            {
                textBuilder.Replace(variant, targetVariants[variant]);
            }
        }
    }
}