using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OpenCCNET
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
        public static Dictionary<string, string> STCharacters;

        /// <summary>
        /// 简体中文=>繁体中文（OpenCC标准）词汇转换字典
        /// </summary>
        public static Dictionary<string, string> STPhrases;

        /// <summary>
        /// 繁体中文（OpenCC标准）=>简体中文单字转换字典
        /// </summary>
        public static Dictionary<string, string> TSCharacters;

        /// <summary>
        /// 繁体中文（OpenCC标准）=>简体中文词汇转换字典
        /// </summary>
        public static Dictionary<string, string> TSPhrases;

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（台湾）单字转换字典
        /// </summary>
        public static Dictionary<string, string> TWVariants;

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（台湾）词汇转换字典
        /// </summary>
        public static Dictionary<string, string> TWPhrases;

        /// <summary>
        /// 繁体中文（台湾）=>繁体中文（OpenCC标准）单字转换字典
        /// </summary>
        public static Dictionary<string, string> TWVariantsReversed;

        /// <summary>
        /// 繁体中文（台湾）=>繁体中文（OpenCC标准）词汇转换字典
        /// </summary>
        public static Dictionary<string, string> TWPhrasesReversed;

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（香港）单字转换字典
        /// </summary>
        public static Dictionary<string, string> HKVariants;

        /// <summary>
        /// 繁体中文（香港）=>繁体中文（OpenCC标准）单字转换字典
        /// </summary>
        public static Dictionary<string, string> HKVariantsReversed;

        /// <summary>
        /// 繁体中文（OpenCC标准）=>繁体中文（中国大陆）单字转换字典
        /// </summary>
        public static Dictionary<string, string> CNVariants;

        /// <summary>
        /// 繁体中文（中国大陆）=>繁体中文（OpenCC标准）单字转换字典
        /// </summary>
        public static Dictionary<string, string> CNVariantsReversed;


        /// <summary>
        /// 加载单个字典文件
        /// </summary>
        /// <param name="dictionaryName">字典名称</param>
        /// <param name="reverse">是否反转</param>
        private static Dictionary<string, string> LoadDictionary(string dictionaryName, bool reverse = false)
        {
            var dictionaryPath = Path.Combine(_dictionaryDirectory, dictionaryName + ".txt");
            var dictionary = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(dictionaryPath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var items = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                    if (!reverse)
                    {
                        if (dictionary.ContainsKey(items[0])) continue;
                        dictionary.Add(items[0], items[1]);
                    }
                    else
                    {
                        for (int i = 1; i < items.Length; i++)
                        {
                            if (dictionary.ContainsKey(items[i])) continue;
                            dictionary.Add(items[i], items[0]);
                        }
                    }
                }
            }

            return dictionary;
        }

        /// <summary>
        /// 加载多个字典文件且合并
        /// </summary>
        /// <param name="dictionaryNames">字典名称</param>
        /// <param name="reverse">是否反转</param>
        private static Dictionary<string, string> LoadDictionary(string[] dictionaryNames, bool reverse = false)
        {
            var dictionaryPaths = dictionaryNames.Select(name => Path.Combine(_dictionaryDirectory, name + ".txt"))
                .ToList();
            var dictionary = new Dictionary<string, string>();
            foreach (var path in dictionaryPaths)
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var items = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                        if (!reverse)
                        {
                            if (dictionary.ContainsKey(items[0])) continue;
                            dictionary.Add(items[0], items[1]);
                        }
                        else
                        {
                            for (int i = 1; i < items.Length; i++)
                            {
                                if (dictionary.ContainsKey(items[i])) continue;
                                dictionary.Add(items[i], items[0]);
                            }
                        }
                    }
                }
            }

            return dictionary;
        }

        public static void Initialize(string dictionaryDirectory = "Dictionary")
        {
            _dictionaryDirectory = dictionaryDirectory;
            STCharacters = LoadDictionary(@"STCharacters");
            STPhrases = LoadDictionary(@"STPhrases");
            TSCharacters = LoadDictionary(@"TSCharacters");
            TSPhrases = LoadDictionary(@"TSPhrases");
            TWVariants = LoadDictionary(@"TWVariants");
            TWPhrases = LoadDictionary(new[] { @"TWPhrasesIT", @"TWPhrasesName", @"TWPhrasesOther" });
            TWVariantsReversed = LoadDictionary(@"TWVariants", true);
            TWPhrasesReversed = LoadDictionary(new[] { @"TWPhrasesIT", @"TWPhrasesName", @"TWPhrasesOther" }, true);
            HKVariants = LoadDictionary(@"HKVariants");
            HKVariantsReversed = LoadDictionary(@"HKVariants", true);
            CNVariants = LoadDictionary(@"CNVariants");
            CNVariantsReversed = LoadDictionary(@"CNVariants", true);
        }
    }
}