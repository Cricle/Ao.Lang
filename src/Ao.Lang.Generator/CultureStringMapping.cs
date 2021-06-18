using System;
using System.Collections.Generic;

namespace Ao.Lang.Generator
{
    public class CultureStringMapping : Dictionary<string, string>, IDictionary<string, string>
    {
        #region 构造函数

        public CultureStringMapping()
        {
        }

        public CultureStringMapping(IDictionary<string, string> dictionary) : base(dictionary)
        {
        }

        public CultureStringMapping(IDictionary<string, string> dictionary, IEqualityComparer<string> comparer) : base(dictionary, comparer)
        {
        }

        public CultureStringMapping(IEqualityComparer<string> comparer) : base(comparer)
        {
        }

        public CultureStringMapping(int capacity) : base(capacity)
        {
        }

        public CultureStringMapping(int capacity, IEqualityComparer<string> comparer) : base(capacity, comparer)
        {
        }

        #endregion
        string IDictionary<string, string>.this[string key]
        {
            get => base[key];
            set
            {
                ThrowHelper.ThrowIfCultureNotFound(key);
                base[key] = value;
            }
        }
        void IDictionary<string, string>.Add(string key, string value)
        {
            ThrowHelper.ThrowIfCultureNotFound(key);
            base.Add(key, value);
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
