using System;
using System.Collections.Generic;

namespace Ao.Lang.Generator
{
    public class LangIdentity : IEquatable<LangIdentity>, ILangIdentity
    {
        public string A { get; set; }

        public string B { get; set; }

        public string C { get; set; }

        public string D { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as LangIdentity);
        }

        public bool Equals(LangIdentity other)
        {
            if (other == null)
            {
                return false;
            }
            return other.A == A &&
                other.B == B &&
                other.C == C &&
                other.D == D;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public string[] GetIdentityBlocks()
        {
            var combineParts = new List<string>();
            if (!string.IsNullOrEmpty(A))
            {
                combineParts.Add(A);
            }
            if (!string.IsNullOrEmpty(B))
            {
                combineParts.Add(B);
            }
            if (!string.IsNullOrEmpty(C))
            {
                combineParts.Add(C);
            }
            if (!string.IsNullOrEmpty(D))
            {
                combineParts.Add(D);
            }
            return combineParts.ToArray();
        }

        public override string ToString()
        {
            return $"{{Type: {A}, Module: {B}, Field: {C}, Data: {D}}}";
        }
    }
}
