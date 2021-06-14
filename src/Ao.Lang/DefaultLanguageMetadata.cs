using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace Ao.Lang
{
    public class DefaultLanguageMetadata : List<IConfigurationSource>, ILanguageMetadata
    {
        protected DefaultLanguageMetadata()
        {

        }
        public DefaultLanguageMetadata(CultureInfo cultureInfo)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            Culture = cultureInfo;
        }
        public DefaultLanguageMetadata(CultureInfo cultureInfo, IEnumerable<IConfigurationSource> sources)
        {
            if (cultureInfo is null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            if (sources is null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            Culture = cultureInfo;
            AddRange(sources);
        }

        public virtual CultureInfo Culture { get; }
    }
}
