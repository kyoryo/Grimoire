using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Grimoire.Helper
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
