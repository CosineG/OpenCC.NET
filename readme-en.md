# OpenCC.NET

[![GitHub license](https://img.shields.io/github/license/CosineG/OpenCC.NET)](https://github.com/CosineG/OpenCC.NET/blob/master/LICENSE) 
[![Nuget](https://img.shields.io/nuget/v/OpenCCNET)](https://www.nuget.org/packages/OpenCCNET/) 
[![Nuget](https://img.shields.io/nuget/dt/OpenCCNET?label=nuget-downloads)](https://www.nuget.org/packages/OpenCCNET/) 
[![GitHub Workflow Status](https://img.shields.io/github/actions/workflow/status/CosineG/OpenCC.NET/nuget.yml
)](https://github.com/CosineG/OpenCC.NET/actions/workflows/nuget.yml)

[简体中文](readme.md) | English

## Introduction

OpenCC.NET is the unofficial C# version of OpenCC (Open Chinese Convert), which supports vocabulary level conversion between Simplified and Traditional Chinese, as well as the conversion of variants and words between regions.

### Features

- Strictly distinguish between "one simplified to many traditional" and "one simplified to many variants".
- Fully compatible with variant characters.
- Strictly review one simple word against many complicated words, the principle is "If it can be divided, it will be divided".
- Support Hong Kong / Taiwan variants conversion, and China mainland / Taiwan regional idioms conversion.
- It is fully compatible with OpenCC native dictionaries, and the dictionaries can be modified, imported and extended freely.
- Support custom segmentation logic.
- Based on .NET Standard 2.0, supporting .NET Framework 4.6.1 and .NET Core 2.0 and above.

## Start

### Installation
Search for OpenCCNET in Nuget and install it, introduce the namespace OpenCCNET in the project. The package comes with Dictionary (dictionary files) and JiebaResource (resource files needed to run Jieba.NET), which will be copied to the program output directory by default.

### Usage
Please call `ZhConverter.Initialize()` before use, which includes four default parameters:
- `dictionaryDirectory`: Path to dictionary files (default is "Dictionary")
- `jiebaResourceDirectory`: Path to Jieba.NET resources (default is "JiebaResource")
- `isParallelEnabled`: Whether to enable parallel processing (default is false)
- `segmentMode`: Segmentation mode (default is Jieba)

```csharp
// Default initialization (using Jieba segmentation)
ZhConverter.Initialize();

// Or specify segmentation mode (e.g., OpenCC's original maximum matching algorithm)
ZhConverter.Initialize(segmentMode: SegmentMode.MaxMatch);
```

OpenCC.NET provides two styles of APIs:

#### ZhConverter Static Class

|Method|Introduction|Note|
|----|----|----|
|HansToHant(string)|Simplified Chinese => Traditional Chinese (OpenCC standard)||
|HansToTW(string, bool=false)|Simplified Chinese => Traditional Chinese (Taiwan) |bool parameter determines whether to convert to regional idioms in Taiwan|
|HansToHK(string)|Simplified Chinese => Traditional Chinese (Hong Kong)||
|HantToHans(string)|Traditional Chinese => Simplified Chinese||
|HantToTW(string, bool=false)|Traditional Chinese => Traditional Chinese (Taiwan)|bool parameter determines whether to convert to regional idioms in Taiwan|
|HantToHK(string)|Traditional Chinese => Traditional Chinese (Hong Kong)||
|TWToHans(string, bool=false)|Traditional Chinese (Taiwan) => Simplified Chinese|bool parameter determines whether to convert to regional idioms in mainland China|
|TWToHant(string, bool=false)|Traditional Chinese (Taiwan) => Traditional Chinese (OpenCC standard)|bool parameter determines whether to convert to regional idioms in mainland China|
|HKToHans(string)|Traditional Chinese (Hong Kong) => Simplified Chinese||
|HKToHant(string, bool=false)|Traditional Chinese (Hong Kong) => Traditional Chinese (OpenCC standard)||
|KyuuToShin(string)|Japanese (Kyūjitai) => Japanese (Shinjitai)||
|ShinToKyuu(string)|Japanese (Shinjitai) => Japanese (Kyūjitai)||

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

#### Extension Methods of string

|Method|Introduction|Note|
|----|----|----|
|ToHantFromHans()|Simplified Chinese => Traditional Chinese (OpenCC standard)||
|ToTWFromHans(bool=false)|Simplified Chinese => Traditional Chinese (Taiwan) |bool parameter determines whether to convert to regional idioms in Taiwan|
|ToHKFromHans()|Simplified Chinese => Traditional Chinese (Hong Kong)||
|ToHansFromHant()|Traditional Chinese => Simplified Chinese||
|ToTWFromHant(bool=false)|Traditional Chinese => Traditional Chinese (Taiwan)|bool parameter determines whether to convert to regional idioms in Taiwan|
|ToHKFromHant()|Traditional Chinese => Traditional Chinese (Hong Kong)||
|ToHansFromTW(bool=false)|Traditional Chinese (Taiwan) => Simplified Chinese|bool parameter determines whether to convert to regional idioms in mainland China|
|ToHantFromTW(bool=false)|Traditional Chinese (Taiwan) => Traditional Chinese (OpenCC standard)|bool parameter determines whether to convert to regional idioms in mainland China|
|ToHansFromHK()|Traditional Chinese (Hong Kong) => Simplified Chinese||
|ToHantFromHK()|Traditional Chinese (Hong Kong) => Traditional Chinese (OpenCC standard)||
|ToShinFromKyuu()|Japanese (Kyūjitai) => Japanese (Shinjitai)||
|ToKyuuFromShin()|Japanese (Shinjitai) => Japanese (Kyūjitai)||

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

### Customization

#### Segmentation Modes

OpenCC.NET supports three segmentation modes that can be flexibly switched according to your needs:

##### 1. Jieba Mode (Default)

Uses jieba.NET for Chinese word segmentation. The default Jieba.NET resource path is "JiebaResource", which can be customized.

```csharp
// Specify during initialization
ZhConverter.Initialize(segmentMode: SegmentMode.Jieba);

// Or switch at runtime
ZhConverter.ZhSegment.SetMode(SegmentMode.Jieba);
```

##### 2. MaxMatch Mode

Uses OpenCC's original maximum matching segmentation algorithm.

```csharp
// Specify during initialization
ZhConverter.Initialize(segmentMode: SegmentMode.MaxMatch);

// Or switch at runtime
ZhConverter.ZhSegment.SetMode(SegmentMode.MaxMatch);
```

##### 3. Custom Mode

Uses user-defined segmentation algorithm. The text is segmented once, and the results are reused throughout the conversion chain.

```csharp
// Method 1: Directly set the segmentation delegate (automatically switches to Custom mode) (for backward compatibility)
ZhConverter.ZhSegment.Segment = input =>
{
    // Custom segmentation logic, e.g., split by spaces
    return input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
};

// Method 2: Use SetCustomSegment method
ZhConverter.ZhSegment.SetCustomSegment(input =>
{
    // Custom segmentation logic, e.g., split by characters
    return input.Select(c => c.ToString());
});
```

#### Jieba Segmentation Customization

By default, OpenCC.NET uses jieba.NET to implement segmentation, and the project uses a static JiebaSegmenter

```csharp
public static JiebaSegmenter Jieba = new JiebaSegmenter();
```
So it can be customized by `ZhConverter.ZhSegment.Jieba`, please see [jieba.NET](https://github.com/anderscui/jieba.NET) for details.

Call `ResetSegment()` to reuse the jieba.NET segmentation and reset the `JiebaSegmenter`.

#### Parallel Processing

For converting large amounts of text, you can enable parallel processing to improve performance:

```csharp
// Enable during initialization
ZhConverter.Initialize(isParallelEnabled: true);

// Or set at runtime
ZhConverter.IsParallelEnabled = true;
```

## Acknowledgments

### OpenCC

[BYVoid/OpenCC](https://github.com/BYVoid/OpenCC) provides dictionaries.

### jieba.NET

[anderscui/jieba.NET](https://github.com/anderscui/jieba.NET) provides the implementation of segmentation.
