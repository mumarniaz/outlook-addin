using EmailSaveAddin.Helpers;
using EmailSaveAddin.Host;
using EmailSaveAddin.Messages;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Tools;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Office = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;


namespace EmailSaveAddin
{
    [ComVisible(true)]
    public class Ribbon1 : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;
        private CustomTaskPane _taskPane;

        public Ribbon1()
        {
            Messenger.Default.Register<ClosePaneMessage>(this, OnClosePaneMessageReceived);
        }

        private void OnClosePaneMessageReceived(ClosePaneMessage obj)
        {
            if (_taskPane != null
                && _taskPane.Visible)
            {
                _taskPane.Visible = false;
            }
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("EmailSaveAddin.Ribbon1.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit https://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion

        public void OnSaveEmailButtonClick(Office.IRibbonControl control)
        {
            if (control.Context is Selection)
            {
                Selection selectedItem = control.Context as Selection;
                if (selectedItem.Count == 1)
                {
                    if (selectedItem[1] is MailItem)
                    {
                        MailItem outLookMailItem = selectedItem[1] as MailItem;
                        if (outLookMailItem != null)
                        {
                            var senderEmailAddress = outLookMailItem.SenderEmailAddress;
                            var html = outLookMailItem.HTMLBody;

                            if (outLookMailItem.Sender.AddressEntryUserType == Microsoft.Office.Interop.Outlook.OlAddressEntryUserType.olExchangeUserAddressEntry)
                            {
                                senderEmailAddress = outLookMailItem.Sender.GetExchangeUser().PrimarySmtpAddress;
                            }

                            if (_taskPane != null
                            && !_taskPane.Visible)
                            {
                                _taskPane.Visible = true;
                                if (Utilities.IsSignedIn)
                                {
                                    MessengerHelper.BroadcastMessage(new SaveEmailViewVisibleMessage());
                                }
                            }
                            else
                            {
                                SetupTaskPane();
                            }

                            Inspector insp = outLookMailItem.GetInspector;
                            Word.Document wordDocMeeting = insp.WordEditor as Word.Document;
                            wordDocMeeting.SaveAs2("meeting.rtf", Word.WdSaveFormat.wdFormatRTF);

                            MessengerHelper.BroadcastMessage(new EmailBodyMessage());
                        }
                    }
                }
            }
        }

        private void SetupTaskPane()
        {
            var host = new TaskPaneWpfControlHost("");
            _taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(host, "Pinnakl CRM");

            if (_taskPane != null)
            {
                _taskPane.Width = Utilities.TaskPaneWidth;
                _taskPane.Visible = _taskPane == null ? false : true;
            }
        }


    }
}
