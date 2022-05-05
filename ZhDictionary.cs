using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenCCNET
{
    public static partial class ZhConverter
    {
        public static class ZhDictionary
        {
            /// <summary>
            /// 字典目录
            /// </summary>
            private static string _dictionaryDirectory;

            /// <summary>
            /// 简体中文=>繁体中文（OpenCC标准）单字转换字典
            /// </summary>
            public static IDictionary<string, string> STCharacters { get; set; }

            /// <summary>
            /// 简体中文=>繁体中文（OpenCC标准）词汇转换字典
            /// </summary>
            public static IDictionary<string, string> STPhrases { get; set; }

            /// <summary>
            /// 繁体中文（OpenCC标准）=>简体中文单字转换字典
            /// </summary>
            public static IDictionary<string, string> TSCharacters { get; set; }

            /// <summary>
            /// 繁体中文（OpenCC标准）=>简体中文词汇转换字典
            /// </summary>
            public static IDictionary<string, string> TSPhrases { get; set; }

            /// <summary>
            /// 繁体中文（OpenCC标准）=>繁体中文（台湾）单字转换字典
            /// </summary>
            public static IDictionary<string, string> TWVariants { get; set; }

            /// <summary>
            /// 繁体中文（OpenCC标准）=>繁体中文（台湾）词汇转换字典
            /// </summary>
            public static IDictionary<string, string> TWPhrases { get; set; }

            /// <summary>
            /// 繁体中文（台湾）=>繁体中文（OpenCC标准）单字转换字典
            /// </summary>
            public static IDictionary<string, string> TWVariantsRev { get; set; }

            /// <summary>
            /// 繁体中文（台湾）=>繁体中文（OpenCC标准）异体字词汇转换字典
            /// </summary>
            public static IDictionary<string, string> TWVariantsRevPhrases { get; set; }

            /// <summary>
            /// 繁体中文（台湾）=>繁体中文（OpenCC标准）词汇转换字典
            /// </summary>
            public static IDictionary<string, string> TWPhrasesRev { get; set; }

            /// <summary>
            /// 繁体中文（OpenCC标准）=>繁体中文（香港）单字转换字典
            /// </summary>
            public static IDictionary<string, string> HKVariants { get; set; }

            /// <summary>
            /// 繁体中文（香港）=>繁体中文（OpenCC标准）单字转换字典
            /// </summary>
            public static IDictionary<string, string> HKVariantsRev { get; set; }

            /// <summary>
            /// 繁体中文（香港）=>繁体中文（OpenCC标准）异体字词汇转换字典
            /// </summary>
            public static IDictionary<string, string> HKVariantsRevPhrases { get; set; }

            /// <summary>
            /// 日语（旧字体）=>日语（新字体）单字转换字典
            /// </summary>
            public static IDictionary<string, string> JPVariants { get; set; }

            /// <summary>
            /// 日语（新字体）=>日语（旧字体）单字转换字典
            /// </summary>
            public static IDictionary<string, string> JPVariantsRev { get; set; }

            /// <summary>
            /// 日语（新字体）=>日语（旧字体）异体字单字转换字典
            /// </summary>
            public static IDictionary<string, string> JPShinjitaiCharacters { get; set; }

            /// <summary>
            /// 日语（新字体）=>日语（旧字体）异体字词汇转换字典
            /// </summary>
            public static IDictionary<string, string> JPShinjitaiPhrases { get; set; }


            /// <summary>
            /// 加载所有字典文件
            /// </summary>
            /// <param name="dictionaryDirectory"></param>
            public static void Initialize(string dictionaryDirectory = "Dictionary")
            {
                _dictionaryDirectory = dictionaryDirectory;
                STCharacters = LoadDictionary(@"STCharacters");
                STPhrases = LoadDictionary(@"STPhrases");
                TSCharacters = LoadDictionary(@"TSCharacters");
                TSPhrases = LoadDictionary(@"TSPhrases");
                TWVariants = LoadDictionary(@"TWVariants");
                TWPhrases = LoadDictionary(@"TWPhrasesIT", @"TWPhrasesName", @"TWPhrasesOther");
                TWVariantsRev = LoadDictionaryReversed(@"TWVariants");
                TWVariantsRevPhrases = LoadDictionary(@"TWVariantsRevPhrases");
                TWPhrasesRev = LoadDictionaryReversed(@"TWPhrasesIT", @"TWPhrasesName", @"TWPhrasesOther");
                HKVariants = LoadDictionary(@"HKVariants");
                HKVariantsRev = LoadDictionaryReversed(@"HKVariants");
                HKVariantsRevPhrases = LoadDictionary(@"HKVariantsRevPhrases");
                JPVariants = LoadDictionary(@"JPVariants");
                JPVariantsRev = LoadDictionaryReversed(@"JPVariants");
                JPShinjitaiCharacters = LoadDictionary(@"JPShinjitaiCharacters");
                JPShinjitaiPhrases = LoadDictionary(@"JPShinjitaiPhrases");
            }

            /// <summary>
            /// 加载字典文件
            /// </summary>
            /// <param name="dictionaryNames">字典名称</param>
            private static IDictionary<string, string> LoadDictionary(params string[] dictionaryNames)
            {
                var dictionaryPaths = dictionaryNames.Select(name => Path.Combine(_dictionaryDirectory, $"{name}.txt"))
                    .ToList();
                var dictionary = new Dictionary<string, string>();
                foreach (var path in dictionaryPaths)
                {
                    using (var sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var items = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                            dictionary[items[0]] = items[1];
                        }
                    }
                }

                return dictionary;
            }

            /// <summary>
            /// 反向加载字典文件
            /// </summary>
            /// <param name="dictionaryNames">字典名称</param>
            private static IDictionary<string, string> LoadDictionaryReversed(params string[] dictionaryNames)
            {
                var dictionaryPaths = dictionaryNames.Select(name => Path.Combine(_dictionaryDirectory, $"{name}.txt"));
                var dictionary = new Dictionary<string, string>();
                foreach (var path in dictionaryPaths)
                {
                    using (var sr = new StreamReader(path))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            var items = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                            for (var i = 1; i < items.Length; i++)
                            {
                                dictionary[items[i]] = items[0];
                            }
                        }
                    }
                }

                return dictionary;
            }
        }
    }
}