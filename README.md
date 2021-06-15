# What is this [![.NET](https://github.com/Cricle/Ao.Lang/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Cricle/Ao.Lang/actions/workflows/dotnet.yml)

It is i18n libraries. It given many solutions make your app support i18n.

And it support *.resources or *.resx file or stream load.

# Why develop it

Because in many framework, has many different way to support i18n, but it can't be standard, so using `Microsoft.Extensions.Configuration` Key-Value mode can easy to manage i18n string block. And this library is support manay platforms.

I imagine when you want development an International application, install packages, write language files, add less code to locate files √.

I hope that international application does not spend a lot of time on designing multilingual framework, but on how to make users understand these words.

# How can it do that

It is base on `Microsoft.Extensions.Configuration`, because i18n is Key-Value mode.

So it support hot reload, any different types of files(json/xml/ini etc...)

# How to use

1. Install package `Ao.Lang`
2. Chose your like string provider file type
    - You can only install `Microsoft.Extensions.Configuration.*` libraries
    - You can install `Ao.Lang.Sources`(It will support *.json, *.xml, *.ini, *.yaml, *.resx, *.resources files)
    - You can development what you want.
3. Create `LanguageService` or use default `LanguageService.Default`
4. Load your strings
    - You can determine files target cultures
    - You can use automatic load, invoke `LanguageMetadataExtensions.EnableAll` to enable all know file types, and invoke `ILangLookup.RaiseDirectory` to load directory all files, it according to `*.culture.ext` to intercept `culture` block to determine culture, `ext` block to determine source.
    - You can use assembly load, invoke `LanguageMetadataExtensions.RaiseAssemblyResources` to load assembly embedded resources.
5. In normal enviroment, you can do this to use string
```csharp
var langSer = new LanguageService();
//....
var zhRoot = langSer.GetRoot("zh-cn");
var enRoot = langSer.GetRoot("en-us");

var zhTitle=zhRoot["title"];
var enTitle=enRoot["title"];

//...
```
6. If you want it automatic load when resource changed, you can set `Microsoft.Extensions.Configuration` source property `ReloadOnChanged` = `true`
7. If you want it support in wpf, you can install package `Ao.Lang.Wpf`, using `Lang` markupextensions to bind string resource
```xml
<Window xmlns:l="clr-namespace:System.Windows.Data;assembly=Ao.Lang.Wpf">
    <TextBlock Text="{l:Lang Title}"/>
</Window>
```
If you want to change language provider, you can change property `LanguageManager.LangService`
If you want to change culture, you can change property `LanguageManager.CultureInfo`
If you want to fixed the language string text, you can
```xml
<Window xmlns:l="clr-namespace:System.Windows.Data;assembly=Ao.Lang.Wpf">
    <TextBlock Text="{l:Lang Title,FixedCulture=zh-cn}"/>
</Window>
```
If you want to provider default value, you can
```xml
<Window xmlns:l="clr-namespace:System.Windows.Data;assembly=Ao.Lang.Wpf">
    <TextBlock Text="{l:Lang Title,DefaultValue=title}"/>
</Window>
```
If you want to non-automatic update culture string, you can
```xml
<Window xmlns:l="clr-namespace:System.Windows.Data;assembly=Ao.Lang.Wpf">
    <TextBlock Text="{l:Lang Title,NoUpdate=true}"/>
</Window>
```
If you want to background binding, you can
```csharp
var textblock = new TextBlock();
textblock.BindLang(TextBlock.TextProperty, "Title");
```
Or
```csharp
textblock = new TextBlock();
textblock.BindText("Title");
```

# After

- [ ] Add more unit test
- [ ] Make it support MAUI, Blazor, AvaloniaUI...

# Samples

## Simple use

```csharp

var langSer = new LanguageService();
langSer.EnsureGetLangNode("zh-cn")
    .AddJsonFile("lang.zh-cn.json");
langSer.EnsureGetLangNode("en-us")
    .AddJsonFile("lang.en-us.json");

var root = langSer.GetRoot(CultureInfo.CurrentCulture);

var title=root["titl"];
//.....

```

## Use in wpf

Has file
```txt
Strings
    en_us
        lang.resx
    zh_cn
        lang.resx
```
```csharp
var ser = LanguageManager.Instance.LangService;
ser.RaiseAssemblyResources<App>(2);
//Because resx file will compile to namespace.folders.fileName.resources
//So 2 is get from last select file block en_us/zh_cn(symbol `_` will replace to `-`)
```

You can watch `samples\Ao.Lang.Sample` or `samples\Ao.Lang.Wpf.Sample`

## Project schedule

|Version|Status|
|:-:|:-:|
|1.x.x|In [Nuget](https://www.nuget.org/packages/Ao.Lang/)|
|3.0.0|In [Nuget](https://www.nuget.org/packages/Ao.Lang/), Is was a break change for 1.x.x|
|4.0.0|In development(It was a break change for 3.0.0)|

# Project extensions

## [Ao.SavableConfig](https://github.com/Cricle/Ao.SavableConfig)

An can bind two way config

## [Structing](https://github.com/Cricle/Structing)

Structing your app like `Asp.Net Core` startup