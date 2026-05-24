using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace LegacyCRM.Core.Resources
{
    [GeneratedCode("Manual", "1.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    public class Strings
    {
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;

        public static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    resourceMan = new ResourceManager("LegacyCRM.Core.Resources.Strings", typeof(Strings).Assembly);
                }

                return resourceMan;
            }
        }

        public static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        public static string WelcomeMessage
        {
            get { return ResourceManager.GetString("WelcomeMessage", resourceCulture); }
        }

        public static string DashboardTitle
        {
            get { return ResourceManager.GetString("DashboardTitle", resourceCulture); }
        }
    }
}
