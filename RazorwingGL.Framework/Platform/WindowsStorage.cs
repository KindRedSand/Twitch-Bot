using System;

namespace RazorwingGL.Framework.Platform.Windows
{
    public class WindowsStorage : DesktopStorage
    {
        public WindowsStorage(string baseName)
            : base(baseName)
        {
        }

        protected override string LocateBasePath() => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
}
