using System.Configuration;

namespace Grimoire.Logic.Helpers
{
    /// <summary>
    /// Contain Helper, note: usefull!
    /// </summary>
    public static class AppHelper
    {
        public static string GetFontFile()
        {
            return ConfigurationManager.AppSettings["font"];
        }
    }
}
