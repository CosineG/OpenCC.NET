using System.Collections.Generic;

namespace OpenCCNET.ConversionPipeline
{
    /// <summary>
    /// 转换管道接口
    /// </summary>
    public interface IConversionPipeline
    {
        /// <summary>
        /// 使用字典进行转换（支持链式调用）
        /// </summary>
        /// <param name="dictionaries">字典列表</param>
        /// <returns>转换管道实例，支持链式调用</returns>
        IConversionPipeline ConvertBy(params IDictionary<string, string>[] dictionaries);
        
        /// <summary>
        /// 获取最终转换结果
        /// </summary>
        /// <returns>转换后的文本</returns>
        string GetResult();
    }
}
