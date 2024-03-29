﻿using Ao.Lang.Generator.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Ao.Lang.Generator.Test.Editor
{
    [TestClass]
    public class JsonLanguageEditorTest
    {
        [TestMethod]
        public void Compile_FileMustJson()
        {
            var editor = new JsonLanguageEditor<LangBlock>(new DirectoryInfo(Environment.CurrentDirectory));
            var scope = editor.GetScope("hello");

            scope.LangBlocks = DataContains.CreateLangBlocks(10);

            scope.Compile(LangIdentityCompiler.Default);
            var fi = editor.GetCompiledFile("hello", "zh-cn");
            var content = File.ReadAllText(fi.FullName);
            Assert.IsFalse(string.IsNullOrEmpty(content));
        }
    }
}
