using Grimoire.Helper;
using Grimoire.UI;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire
{
    public class Program
    {
        private static readonly int _charWidth = 8;
        private static readonly int _charHeight = 8;
        private static RLRootConsole _rootConsole;
        private static RLConsole mapConsole;
        private static RLConsole messageConsole;
        private static RLConsole statusConsole;
        private static RLConsole inventoryConsole;

        static void Main()
        {
            string fontFile = AppHelper.GetFontFile();
            //string fontFile = "terminal8x8.png";
            string title = "Tes Level 1";
            var map = new RLConsole(Frame.statusWidth(), Frame.statusHeight());

            _rootConsole = new RLRootConsole(fontFile, Frame.GetScreenWidth(), Frame.GetScreenHeight(), _charWidth, _charHeight, 1f, title);
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            _rootConsole.Run();

        }
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            _rootConsole.Print(10, 10, "tes", RLColor.White);
        }
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            _rootConsole.Draw();
        }
    }
}
