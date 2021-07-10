using System;
using System.IO;

namespace Ao.Lang.Lookup
{
    public class LangLookupBox
    {
        public LangLookupBox(string path)
            : this(path, null)
        {
            Path = path;
        }
        public LangLookupBox(string path, Stream stream)
            : this(path, stream, true)
        {
        }
        public LangLookupBox(string path, Stream stream, bool optional)
            : this(path, stream, optional, false)
        {
        }
        public LangLookupBox(string path, Stream stream, bool optional, bool reloadOnChanged)
        {
            Path = path;
            Stream = stream;
            Optional = optional;
            ReloadOnChanged = reloadOnChanged;
        }
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(Path))
                {
                    return null;
                }
                return System.IO.Path.GetFileName(Path);
            }
        }
        public string Extension
        {
            get
            {
                var name = Name;
                if (string.IsNullOrEmpty(name))
                {
                    return null;
                }
                var last = name.LastIndexOf('.');
                if (last == -1)
                {
                    return null;
                }
                return name.Substring(last+1, name.Length - last-1);
            }
        }

        public string Path { get; }

        public Stream Stream { get; }

        public bool Optional { get; }

        public bool ReloadOnChanged { get; }

        public override string ToString()
        {
            return $"{{Path: {Path}, Optional:{Optional}, ReloadOnChanged: {ReloadOnChanged}}}";
        }
        public string GetLangIdentity(char split)
        {
            return GetLangIdentity(split, 1);
        }
        public string GetLangIdentity(char split, int lastRevIndex)
        {
            var name = Name;
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            var sps = name.Split(new[] { split }, StringSplitOptions.RemoveEmptyEntries);
            if (sps.Length > lastRevIndex)
            {
                return sps[sps.Length - lastRevIndex-1];
            }
            return null;
        }
    }
}
