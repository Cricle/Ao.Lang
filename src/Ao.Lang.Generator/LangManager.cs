using System;
using System.IO;

namespace Ao.Lang.Generator
{
    public class LangManager
    {
        public LangManager(DirectoryInfo workspace)
        {
            Workspace = workspace ?? throw new ArgumentNullException(nameof(workspace));
        }

        public DirectoryInfo Workspace { get; }

    }
}
