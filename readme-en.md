# OpenCC.NET

[![GitHub license](https://img.shields.io/github/license/CosineG/OpenCC.NET)](https://github.com/CosineG/OpenCC.NET/blob/master/LICENSE) 
[![Nuget](https://img.shields.io/nuget/v/OpenCCNET)](https://www.nuget.org/packages/OpenCCNET/) 
[![Nuget](https://img.shields.io/nuget/dt/OpenCCNET?label=nuget-downloads)](https://www.nuget.org/packages/OpenCCNET/) 
[![GitHub Workflow Status](https://img.shields.io/github/workflow/status/CosineG/OpenCC.NET/publish%20to%20nuget)](https://github.com/CosineG/OpenCC.NET/actions/workflows/nuget.yml)

[简体中文](readme.md) | English

## Introduction

OpenCC.NET is the unofficial C# version of OpenCC (Open Chinese Convert), which supports vocabulary level conversion between Simplified and Traditional Chinese, as well as the conversion of variants and words between regions.

### Features

- Strictly distinguish between "one simplified to many traditional" and "one simplified to many variants".
- Fully compatible with variant characters.
- Strictly review one simple word against many complicated words, the principle is "If it can be divided, it will be divided".
- Support Hong Kong / Taiwan variants conversion, and China mainland / Taiwan regional idioms conversion.
- It is fully compatible with OpenCC native dictionaries, and the dictionaries can be modified, imported and extended freely.
- Based on .NET Standard 2.0, supporting .NET Framework 4.6.1 and .NET Core 2.0 and above.

#### Update

Updated for 1.0 release:

 - Refactored and simplified the project structure and processing logic to a chain-like process similar to OpenCC.
 - Fixed the bug that 「著作」=>「着作」 in the conversion of Traditional Chinese (Taiwan) to Simplified Chinese.
 - Now when OpenCCNET package is introduced into the project, the packaged dictionaries and resource files can be copied to the program output directory automatically.
 - Added the function of converting Kyūjitai and Shinjitai to Japanese kanji.

## Start

### Installation
Search for OpenCCNET in Nuget and install it, introduce the namespace OpenCCNET in the project. The package comes with Dictionary (dictionary files) and JiebaResource (resource files needed to run Jieba.NET), which will be copied to the program output directory by default.

### Usage
Please call `ZhConverter.Initialize()`, which with 2 default parameters, the path of dictionaries and the Jieba.NET resources, before use. (the original `ZhUtil.Initialize()` is deprecated).

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

#### Segment

By default, OpenCC.NET uses jieba.NET to implement segmentation, and the project uses a static JiebaSegmenter

```csharp
public static JiebaSegmenter Jieba = new JiebaSegmenter();
```
So it can be customized by `ZhConverter.ZhSegment.Jieba`, please see [jieba.NET](https://github.com/anderscui/jieba.NET) for details.

OpenCC.NET also supports custom segment implementation:

```csharp
ZhConverter.ZhSegment.Segment = input =>
{
    // input is string，and output is IEnumerable<string>
    return new List<string>();
};
```

Call `ResetSegment()` to reuse the jieba.NET segmentation and reset the `JiebaSegmenter`.

## Acknowledgments

### OpenCC

[BYVoid/OpenCC](https://github.com/BYVoid/OpenCC) provides dictionaries.

### jieba.NET

[anderscui/jieba.NET](https://github.com/anderscui/jieba.NET) provides the implementation of segmentation.
