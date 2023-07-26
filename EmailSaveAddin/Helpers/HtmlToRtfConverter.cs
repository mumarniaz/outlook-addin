using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace EmailSaveAddin.Helpers
{
    public static class HtmlToRtfConverter
    {
        public static string ConvertRtfToHtml(string rtfContent)
        {
            string htmlContent = string.Empty;

            try
            {
                // Create a temporary RichTextBox control
                using (RichTextBox tempRichTextBox = new RichTextBox())
                {
                    // Load the RTF content into the temporary RichTextBox
                    tempRichTextBox.Rtf = rtfContent;
                    // Get the HTML content from the temporary RichTextBox
                    htmlContent = tempRichTextBox.SelectedRtf;
                    // Convert the RTF to HTML using Clipboard
                    tempRichTextBox.SelectAll();
                    tempRichTextBox.Copy();

                    for (int i = 0; i < 10; i++)
                    {
                        try
                        {
                            

                            IDataObject iData = Clipboard.GetDataObject();

                            if (iData.GetDataPresent(DataFormats.Html))
                            {
                                htmlContent = (string)iData.GetData(DataFormats.Html);
                            }

                            // Parse and clean html
                            var index = htmlContent.IndexOf("<html>");
                            if (index > 0)
                            {
                                htmlContent = htmlContent.Remove(0, index);
                            }
                            return htmlContent;
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                            Thread.Sleep(100);
                        }
                    }

                    
                }
            }
            catch (Exception ex)
            {
                // Handle any potential conversion errors
                Console.WriteLine($"Error converting RTF to HTML: {ex.Message}");
            }

            return htmlContent;
        }
    }
}
