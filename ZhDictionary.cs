using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

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
            /// 缓存了 <see cref="ZhDictionary"/> 类中所有公共静态的 <see cref="IDictionary{TKey, TValue}"/> 属性的元数据
            /// </summary>
            private static readonly PropertyInfo[] WordDictionaryProperties = typeof(ZhDictionary)
                .GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(IDictionary<string, string>))
                .ToArray();

            /// <summary>
            /// 所有字典中词的最大长度
            /// </summary>
            internal static int MaxWordLength => WordDictionaryProperties
                .Select(p => p.GetValue(null) as IDictionary<string, string>)
                .Where(d => d is { Count: > 0 })
                .SelectMany(d => d.Keys)
                .DefaultIfEmpty(string.Empty)
                .Max(k => k.Length);

            /// <summary>
            /// 加载所有字典文件
            /// </summary>
            /// <param name="dictionaryDirectory"></param>
            public static void Initialize(string dictionaryDirectory = "Dictionary")
            {
                _dictionaryDirectory = dictionaryDirectory;
                STCharacters = LoadDictionary("STCharacters");
                STPhrases = LoadDictionary("STPhrases");
                TSCharacters = LoadDictionary("TSCharacters");
                TSPhrases = LoadDictionary("TSPhrases");
                TWVariants = LoadDictionary("TWVariants");
                TWPhrases = LoadDictionary("TWPhrasesIT", "TWPhrasesName", "TWPhrasesOther");
                TWVariantsRev = LoadDictionaryReversed("TWVariants");
                TWVariantsRevPhrases = LoadDictionary("TWVariantsRevPhrases");
                TWPhrasesRev = LoadDictionaryReversed("TWPhrasesIT", "TWPhrasesName", "TWPhrasesOther");
                HKVariants = LoadDictionary("HKVariants");
                HKVariantsRev = LoadDictionaryReversed("HKVariants");
                HKVariantsRevPhrases = LoadDictionary("HKVariantsRevPhrases");
                JPVariants = LoadDictionary("JPVariants");
                JPVariantsRev = LoadDictionaryReversed("JPVariants");
                JPShinjitaiCharacters = LoadDictionary("JPShinjitaiCharacters");
                JPShinjitaiPhrases = LoadDictionary("JPShinjitaiPhrases");
            }

            /// <summary>
            /// 加载字典文件
            /// </summary>
            /// <param name="dictionaryNames">字典名称</param>
            private static IDictionary<string, string> LoadDictionary(params string[] dictionaryNames)
            {
                return LoadDictionaryInternal(dictionaryNames, (items, dictionary) =>
                {
                    dictionary[items[0]] = items[1];
                });
            }

            /// <summary>
            /// 反向加载字典文件
            /// </summary>
            /// <param name="dictionaryNames">字典名称</param>
            private static IDictionary<string, string> LoadDictionaryReversed(params string[] dictionaryNames)
            {
                return LoadDictionaryInternal(dictionaryNames, (items, dictionary) =>
                {
                    for (var i = 1; i < items.Count; i++)
                    {
                        dictionary[items[i]] = items[0];
                    }
                });
            }

            /// <summary>
            /// 内部字典加载方法
            /// </summary>
            private static IDictionary<string, string> LoadDictionaryInternal(IList<string> dictionaryNames, Action<IList<string>, Dictionary<string, string>> processLine)
            {
                if (string.IsNullOrEmpty(_dictionaryDirectory))
                {
                    throw new InvalidOperationException("字典目录未初始化，请先调用Initialize方法");
                }

                var dictionary = new Dictionary<string, string>(10000); // 预分配合理的初始容量
                var dictionaryPaths = dictionaryNames.Select(name => Path.Combine(_dictionaryDirectory, $"{name}.txt"));

                foreach (var path in dictionaryPaths)
                {
                    if (!File.Exists(path))
                    {
                        throw new FileNotFoundException($"找不到字典文件：{path}");
                    }

                    try
                    {
                        var lines = File.ReadAllLines(path);
                        foreach (var line in lines)
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;
                            
                            var items = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                            if (items.Length < 2) continue;

                            processLine(items, dictionary);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new IOException($"加载字典文件 {path} 时发生错误", ex);
                    }
                }

                return dictionary;
            }
        }
    }
}