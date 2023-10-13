using EmailSaveAddin.Models;
using System.Collections.Generic;
using System.Linq;
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
        public static bool IsSignedIn { get; set; }

        internal static bool IsInternalContact(string email)
        {
            List<string> list = new List<string>() 
            {
                "Tarun Kapoor",
                "Tom Cleavland"
            };

            return list.Contains(email);
        }

        internal static bool IsInternalContact(Contact contact)
        {
            List<Contact> list = new List<Contact>()
            {
                new Contact()
                {
                    FirstName = "Tarun",
                    LastName = "Kapoor",
                    Email = "tarun.kapoor@gmail.com"
                },
                new Contact()
                {
                    FirstName = "Tom",
                    LastName = "Cleavland",
                    Email = "tom.cleavland@gmail.com"
                }
            };

            return list.Any(t => t.Equals(contact));
        }
    }
}
