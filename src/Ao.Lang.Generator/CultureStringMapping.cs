using System;
using System.Collections.Generic;

namespace Ao.Lang.Generator
{
    public class CultureStringMapping : Dictionary<string, string>, IDictionary<string, string>
    {
        #region 构造函数


        public CultureStringMapping(IDictionary<string, string> dictionary)
            : base(dictionary, StringComparer.OrdinalIgnoreCase)
        {
        }

        public CultureStringMapping()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        public CultureStringMapping(int capacity)
            : base(capacity, StringComparer.OrdinalIgnoreCase)
        {
        }

        #endregion
        string IDictionary<string, string>.this[string key]
        {
            get => this[key];
            set
            {
                this[key] = value;
            }
        }
        void IDictionary<string, string>.Add(string key, string value)
        {
            this.Add(key, value);
        }
        public new string this[string key]
        {
            get => base[key];
            set
            {
                ThrowHelper.ThrowIfCultureNotFound(key);
                base[key] = value;
            }
        }

        public new void Add(string key, string value)
        {
            ThrowHelper.ThrowIfCultureNotFound(key);
            base.Add(key, value);
        }
    }
}
