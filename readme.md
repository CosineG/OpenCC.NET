# OpenCC.NET

[![GitHub license](https://img.shields.io/github/license/CosineG/OpenCC.NET)](https://github.com/CosineG/OpenCC.NET/blob/master/LICENSE) 
[![Nuget](https://img.shields.io/nuget/v/OpenCCNET)](https://www.nuget.org/packages/OpenCCNET/) 
[![Nuget](https://img.shields.io/nuget/dt/OpenCCNET?label=nuget-downloads)](https://www.nuget.org/packages/OpenCCNET/) 
[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/CosineG/OpenCC.NET/nuget.yml
)](https://github.com/CosineG/OpenCC.NET/actions/workflows/nuget.yml)

简体中文 | [English](readme-en.md)

## 介绍

OpenCC.NET 是 OpenCC (Open Chinese Convert, 开放中文转换) 的 C# 非官方版本，支持中文简繁体之间词汇级别的转换，同时还支持地域间异体字以及词汇的转换。

### 特点

- 严格区分「一简对多繁」和「一简对多异」
- 完全兼容异体字
- 严格审校一简对多繁词条，原则为「能分则不合」
- 支持港/台异体字转换，以及大陆/台湾常用词汇转换
- 完全兼容OpenCC原生词库，可以自由修改、导入、扩展
- 支持自定义分词逻辑
- 基于 .NET Standard 2.0，同时支持 .NET Framework 4.6.1 和 .NET Core 2.0 及以上版本

## 开始

### 获取
Nuget 搜索 OpenCCNET 并安装，在项目代码中引入命名空间 OpenCCNET。Nuget 包中自带 Dictionary（字典文件）和 JiebaResource（Jieba.NET运行所需的词典及其它数据文件），默认会复制到程序输出目录。

### 使用
在使用前请调用`ZhConverter.Initialize()`，含四个默认参数：
- `dictionaryDirectory`: 字典文件路径（默认为"Dictionary"）
- `jiebaResourceDirectory`: Jieba.NET资源路径（默认为"JiebaResource"）
- `isParallelEnabled`: 是否启用并行处理（默认为false）
- `segmentMode`: 分词模式（默认为结巴分词）

```csharp
// 默认初始化（使用结巴分词）
ZhConverter.Initialize();

// 或者指定分词模式（例如：OpenCC 的原版最大匹配分词算法）
ZhConverter.Initialize(segmentMode: SegmentMode.MaxMatch);
```

OpenCC.NET 提供了两种风格的API：

#### ZhConverter静态类

|方法|简介|备注|
|----|----|----|
|HansToHant(string)|简体中文=>繁体中文（OpenCC标准）||
|HansToTW(string, bool=false)|简体中文=>繁体中文（台湾）|bool参数决定是否转换为台湾地区常用词汇|
|HansToHK(string)|简体中文=>繁体中文（香港）||
|HantToHans(string)|繁体中文=>简体中文||
|HantToTW(string, bool=false)|繁体中文=>繁体中文（台湾）|bool参数决定是否转换为台湾地区常用词汇|
|HantToHK(string)|繁体中文=>繁体中文（香港）||
|TWToHans(string, bool=false)|繁体中文（台湾）=>简体中文|bool参数决定是否转换为大陆地区常用词汇|
|TWToHant(string, bool=false)|繁体中文（台湾）=>繁体中文（OpenCC标准）|bool参数决定是否转换为大陆地区常用词汇|
|HKToHans(string)|繁体中文（香港）=>简体中文||
|HKToHant(string)|繁体中文（香港）=>繁体中文（OpenCC标准）||
|KyuuToShin(string)|日语（旧字体）=>日语（新字体）||
|ShinToKyuu(string)|日语（新字体）=>日语（旧字体）||

```csharp
var input = "为我的电脑换了新的内存，开启电脑后感觉看网络视频更加流畅了";

// 爲我的電腦換了新的內存，開啓電腦後感覺看網絡視頻更加流暢了
Console.WriteLine(ZhConverter.HansToHant(input));

// 為我的電腦換了新的內存，開啟電腦後感覺看網絡視頻更加流暢了
Console.WriteLine(ZhConverter.HansToTW(input));

// 為我的電腦換了新的記憶體，開啟電腦後感覺看網路影片更加流暢了
Console.WriteLine(ZhConverter.HansToTW(input, true));

// 為我的電腦換了新的內存，開啓電腦後感覺看網絡視頻更加流暢了
Console.WriteLine(ZhConverter.HansToHK(input));

// 沖繩縣內の學校
Console.WriteLine(ZhConverter.ShinToKyuu("沖縄県内の学校"));
```

#### string类扩展方法

|方法|简介|备注|
|----|----|----|
|ToHantFromHans()|简体中文=>繁体中文（OpenCC标准）||
|ToTWFromHans(bool=false)|简体中文=>繁体中文（台湾）|bool参数决定是否转换为台湾地区常用词汇|
|ToHKFromHans()|简体中文=>繁体中文（香港）||
|ToHansFromHant()|繁体中文=>简体中文||
|ToTWFromHant(bool=false)|繁体中文=>繁体中文（台湾）|bool参数决定是否转换为台湾地区常用词汇|
|ToHKFromHant()|繁体中文=>繁体中文（香港）||
|ToHansFromTW(bool=false)|繁体中文（台湾）=>简体中文|bool参数决定是否转换为大陆地区常用词汇|
|ToHantFromTW(bool=false)|繁体中文（台湾）=>繁体中文（OpenCC标准）|bool参数决定是否转换为大陆地区常用词汇|
|ToHansFromHK()|繁体中文（香港）=>简体中文||
|ToHantFromHK()|繁体中文（香港）=>繁体中文（OpenCC标准）||
|ToShinFromKyuu()|日语（旧字体）=>日语（新字体）||
|ToKyuuFromShin()|日语（新字体）=>日语（旧字体）||

```csharp
var input = "為我的電腦換了新的記憶體，開啟電腦後感覺看網路影片更加流暢了";

// 爲我的電腦換了新的記憶體，開啓電腦後感覺看網路影片更加流暢了
Console.WriteLine(input.ToHantFromTW());

// 为我的电脑换了新的记忆体，开启电脑后感觉看网路影片更加流畅了
Console.WriteLine(input.ToHansFromTW());

// 为我的电脑换了新的内存，打开电脑后感觉看网络视频更加流畅了
Console.WriteLine(input.ToHansFromTW(true));

// 独逸連邦共和国
Console.WriteLine("獨逸聯邦共和國".ToShinFromKyuu());
```

### 自定义

#### 分词模式

OpenCC.NET 支持三种分词模式，可以根据需求灵活切换：

##### 1. 结巴分词模式（Jieba）- 默认

使用 jieba.NET 进行中文分词。默认设置 Jieba.NET 资源路径为 "JiebaResource"，可以自行指定。

```csharp
// 初始化时指定
ZhConverter.Initialize(segmentMode: SegmentMode.Jieba);

// 或运行时切换
ZhConverter.ZhSegment.SetMode(SegmentMode.Jieba);
```

##### 2. 最大匹配算法模式（MaxMatch）

使用 OpenCC 原版的最大匹配分词算法。

```csharp
// 初始化时指定
ZhConverter.Initialize(segmentMode: SegmentMode.MaxMatch);

// 或运行时切换
ZhConverter.ZhSegment.SetMode(SegmentMode.MaxMatch);
```

##### 3. 自定义分词模式（Custom）

使用用户自定义的分词算法，先分词一次，然后在转换链中重复使用分词结果。

```csharp
// 方式1：直接设置分词委托（自动切换到 Custom 模式）（兼容老版本）
ZhConverter.ZhSegment.Segment = input =>
{
    // 自定义分词逻辑，例如按空格分词
    return input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
};

// 方式2：使用 SetCustomSegment 方法
ZhConverter.ZhSegment.SetCustomSegment(input =>
{
    // 自定义分词逻辑，例如按字符分词
    return input.Select(c => c.ToString());
});
```

#### Jieba 分词自定义

OpenCC.NET默认使用jieba.NET实现分词，项目中使用了静态的JiebaSegmenter
```csharp
public static JiebaSegmenter Jieba = new JiebaSegmenter();
```
因此可以通过`ZhConverter.ZhSegment.Jieba`进行自定义设置，详情请见[jieba.NET](https://github.com/anderscui/jieba.NET)。

调用`ResetSegment()`可重新指定使用Jieba分词并且重置Jieba参数。

#### 并行处理

对于大量文本的转换，可以启用并行处理来提高性能：

```csharp
// 初始化时启用
ZhConverter.Initialize(isParallelEnabled: true);

// 或运行时设置
ZhConverter.IsParallelEnabled = true;
```

## 引用

### OpenCC

[BYVoid/OpenCC](https://github.com/BYVoid/OpenCC) 提供词库。

### jieba.NET

[anderscui/jieba.NET](https://github.com/anderscui/jieba.NET) 提供分词功能。

