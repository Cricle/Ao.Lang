using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;

namespace Microsoft.Extensions.Configuration.Resources
{
    internal static class ResourceHelper
    {
        public static IDictionary<string, string> GetData(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            if (!stream.CanRead)
            {
                throw new InvalidOperationException("Stream can't read");
            }

            var datas = new Dictionary<string, string>();
            using (var resx = new ResourceReader(stream))
            {
                var enu = resx.GetEnumerator();
                while (enu.MoveNext())
                {
                    if (enu.Value is string value)
                    {
                        datas.Add(enu.Key.ToString(), value);
                    }
                }
            }
            return datas;
        }
    }
}

