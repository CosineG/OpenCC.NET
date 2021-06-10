# OpenCC.NET

## 介绍

OpenCC.NET是OpenCC(Open Chinese Convert, 开放中文转换)的C#非官方版本，支持中文简繁体之间词汇级别的转换，同时还支持地域间异体字以及词汇的转换。

### 特点

- 严格区分「一简对多繁」和「一简对多异」
- 完全兼容异体字，可以实现动态替换
- 严格审校一简对多繁词条，原则为「能分则不合」
- 支持港/台异体字转换，以及大陆/台湾常用词汇转换
- 完全兼容OpenCC原生词库，且词库和函数库完全分离，可以自由修改、导入、扩展
- 基于.Net Standard 2.0，同时支持.NET Framework 4.6.1和.NET Core 2.0及以上版本

## 开始

### 获取
Nuget搜索OpenCCNET并安装，在项目代码中引入命名空间OpenCCNET。下载仓库中的Dictionary(字典文件)和JiebaResource(Jieba.NET运行所需的词典及其它数据文件)文件夹放入项目程序所在目录。

### API
OpenCC.NET提供了两种风格的API。

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
|HKToHans(string)|繁体中文（香港）=>简体中文||

```
var input = "为我的电脑换了内存，开启电脑后感觉网络速度更快了";
// 爲我的電腦換了內存，開啓電腦後感覺網絡速度更快了
Console.WriteLine(ZhConverter.HansToHant(input));
// 為我的電腦換了記憶體，開啟電腦後感覺網路速度更快了
Console.WriteLine(ZhConverter.HansToTW(input, true));
// 為我的電腦換了內存，開啓電腦後感覺網絡速度更快了
Console.WriteLine(ZhConverter.HansToHK(input));
```

#### string类扩展方法

|方法|简介|备注|
|----|----|----|
|ToHantFromHans()|简体中文=>繁体中文（OpenCC标准）||
|ToTWFromHans(bool=false)|简体中文=>繁体中文（台湾）|bool参数决定是否转换为台湾地区常用词汇|
|ToHKFromHans()|简体中文=>繁体中文（香港）||
|ToHansFromHant()|繁体中文=>简体中文||
|ToTWFromHant(bool=false)|繁体中文=>繁体中文（台湾）|bool参数决定是否转换为台湾地区常用词汇|
|ToHKFromHant(string)|繁体中文=>繁体中文（香港）||
|ToHansFromTW(bool=false)|繁体中文（台湾）=>简体中文|bool参数决定是否转换为大陆地区常用词汇|
|ToHansFromHK(string)|繁体中文（香港）=>简体中文||

```
var input = "為我的電腦換了記憶體，開啟電腦后感覺網路速度更快了";
// 爲我的電腦換了內存，開啓電腦后感覺網絡速度更快了
Console.WriteLine(input.ToHantFromTW(true));
// 为我的电脑换了记忆体，开启电脑后感觉网路速度更快了
Console.WriteLine(input.ToHansFromTW());
// 为我的电脑换了内存，开启电脑后感觉网络速度更快了
Console.WriteLine(input.ToHansFromTW(true));
```

### 自定义

#### 分词

OpenCC.NET默认使用jieba.NET实现分词，因此第一次调用API时需要加载所需文件，可能会花费少许时间。项目中使用了静态的JiebaSegmenter
```
public static JiebaSegmenter Jieba = new JiebaSegmenter();
```
因此可以通过`ZhUtil.Jieba`进行自定义设置，详情请见[jieba.NET](https://github.com/anderscui/jieba.NET)。

同时OpenCC.NET支持自定义分词实现：
```
ZhUtil.Segment = input =>
{
    // 输入为string，输出为IEnumerable<string>
    return new List<string>();
};
```

调用`ResetSegment()`可重新指定使用Jieba分词并且重置Jieba参数

## 引用

### OpenCC

[OpenCC](https://github.com/BYVoid/OpenCC)提供词库。

### jieba.NET

[jieba.NET](https://github.com/anderscui/jieba.NET)提供分词。