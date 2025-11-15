using OpenCCNET.ConversionPipeline;

namespace OpenCCNET
{
    public static partial class ZhConverter
    {
        /// <summary>
        /// 是否启用并行处理
        /// </summary>
        public static bool IsParallelEnabled { get; set; } = false;

        /// <summary>
        /// 创建转换管道
        /// </summary>
        /// <param name="text">待转换的文本</param>
        /// <returns>转换管道实例</returns>
        private static IConversionPipeline CreatePipeline(string text)
        {
            return ZhSegment.CurrentMode switch
            {
                SegmentMode.Jieba => new JiebaConversionPipeline(text),
                SegmentMode.MaxMatch => new MaxMatchConversionPipeline(text),
                SegmentMode.Custom => new PreSegmentConversionPipeline(text),
                _ => new JiebaConversionPipeline(text)
            };
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dictionaryDirectory">字典文件夹路径</param>
        /// <param name="jiebaResourceDirectory">Jieba.NET资源路径</param>
        /// <param name="isParallelEnabled">是否启用并行处理。默认为 false</param>
        /// <param name="segmentMode">分词模式。默认为 Jieba</param>
        public static void Initialize(string dictionaryDirectory = "Dictionary",
            string jiebaResourceDirectory = "JiebaResource",
            bool isParallelEnabled = false,
            SegmentMode segmentMode = SegmentMode.Jieba)
        {
            ZhSegment.Initialize(jiebaResourceDirectory, segmentMode);
            ZhDictionary.Initialize(dictionaryDirectory);
            IsParallelEnabled = isParallelEnabled;
        }

        #region 简体中文

        /// <summary>
        /// 简体中文=>繁体中文（OpenCC标准）
        /// </summary>
        public static string HansToHant(string text)
        {
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.STPhrases, ZhDictionary.STCharacters)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.STPhrases, ZhDictionary.STCharacters)
                .ConvertBy(isIdiomConvert ? ZhDictionary.TWPhrases : null)
                .ConvertBy(ZhDictionary.TWVariants)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.STPhrases, ZhDictionary.STCharacters)
                .ConvertBy(ZhDictionary.HKVariants)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.TSPhrases, ZhDictionary.TSCharacters)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(isIdiomConvert ? ZhDictionary.TWPhrases : null)
                .ConvertBy(ZhDictionary.TWVariants)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.HKVariants)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.TWVariantsRev, ZhDictionary.TWVariantsRevPhrases)
                .ConvertBy(isIdiomConvert ? ZhDictionary.TWPhrasesRev : null)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.TWVariantsRev, ZhDictionary.TWVariantsRevPhrases)
                .ConvertBy(isIdiomConvert ? ZhDictionary.TWPhrasesRev : null)
                .ConvertBy(ZhDictionary.TSPhrases, ZhDictionary.TSCharacters)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.HKVariantsRevPhrases, ZhDictionary.HKVariantsRev)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.HKVariantsRevPhrases, ZhDictionary.HKVariantsRev)
                .ConvertBy(ZhDictionary.TSPhrases, ZhDictionary.TSCharacters)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.JPVariants)
                .GetResult();
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
            return CreatePipeline(text)
                .ConvertBy(ZhDictionary.JPShinjitaiPhrases, ZhDictionary.JPShinjitaiCharacters,
                    ZhDictionary.JPVariantsRev)
                .GetResult();
        }

        /// <summary>
        /// 日语（新字体）=>日语（旧字体）
        /// </summary>
        public static string ToKyuuFromShin(this string text)
        {
            return ShinToKyuu(text);
        }

        #endregion
    }
}