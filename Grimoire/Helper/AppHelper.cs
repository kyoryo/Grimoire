using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Grimoire.Helper
{
    public class AppHelper
    {
        public static string GetFontFile()
        {
            return ConfigurationManager.AppSettings["font"];
        }
    }
}
