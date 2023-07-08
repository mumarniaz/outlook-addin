using System.Collections.Generic;
using System.Windows.Forms;

namespace EmailSaveAddin.Helpers
{
    public class Utilities
    {
        private static int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        private static int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        private static double TaskPaneWidthFactor = 0.22; //380 (ideal width) / 1920 (perfect for ideal width) = 0.1979 | Changed to .22 to be a bit larger for Hannon
        private static double ScrollViewerHeightFactor = 0.22;

        public static int TaskPaneWidth = (int)(screenWidth * TaskPaneWidthFactor);
        public static double ScrollHeight = (screenHeight * ScrollViewerHeightFactor);

        internal static bool IsInternalEmail(string email)
        {
            List<string> list = new List<string>() 
            {
                "Tarun Kapoor",
                "Tom Cleavland"
            };

            return list.Contains(email);
        }
    }
}
