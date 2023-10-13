using EmailSaveAddin.Helpers;
using EmailSaveAddin.Messages;
using EmailSaveAddin.Models;
using EmailSaveAddin.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace EmailSaveAddin.Views
{
    /// <summary>
    /// Interaction logic for SaveEmailView.xaml
    /// </summary>
    public partial class SaveEmailView : UserControl
    {
        public SaveEmailView()
        {
            InitializeComponent();

            Messenger.Default.Register<EmailBodyMessage>(this, OnEmailBodyMessageReceived);

            var vm = this.DataContext as SaveEmailViewModel;
            if (vm != null)
            {
                vm.OnSave = new Action(OnSave);
            }
        }

        private void OnSave()
        {
            // Create a TextRange to read the RTF content
            try
            {
                TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                // Create a FileStream to save the RTF content to a file
                using (FileStream fileStream = new FileStream("meeting.rtf", FileMode.Create))
                {
                    // Save the RTF content to the file
                    textRange.Save(fileStream, DataFormats.Rtf);
                }

                string rtf = File.ReadAllText("meeting.rtf");
                var html = HtmlToRtfConverter.ConvertRtfToHtml(rtf);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void OnEmailBodyMessageReceived(EmailBodyMessage obj)
        {
            try
            {
                // Read the RTF content from the file
                string rtfContent = File.ReadAllText("meeting.rtf");

                // Create a TextRange to read the RTF content
                TextRange textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                // Create a MemoryStream and a StreamWriter to write the RTF content
                using (MemoryStream memoryStream = new MemoryStream())
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {
                    // Write the RTF content to the MemoryStream
                    streamWriter.Write(rtfContent);
                    streamWriter.Flush();
                    memoryStream.Position = 0;

                    // Load the RTF content into the RichTextBox
                    textRange.Load(memoryStream, DataFormats.Rtf);
                }

                File.Delete("meeting.rtf");
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine($"Error loading RTF content into RichTextBox: {ex.Message}", "Error");
            }
        }

        private void Chip_AddClick(object sender, RoutedEventArgs e)
        {
            var chip = (sender as Chip);
            var contact= chip.DataContext as Contact;

            MessengerHelper.BroadcastMessage(new AddContactMessage());

            MessengerHelper.BroadcastMessage(new ContactMessage()
            {
                Contact = contact
            });
        }

        private void TextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Add(sender, e.Key == Key.OemSemicolon);
            }
            e.Handled = true;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                var textBox = sender as TextBox;
                if (String.IsNullOrEmpty(textBox.Text))
                {
                    // Textbox is empty, now see if we have answers then clear the last one from list
                    var vm = textBox.DataContext as SaveEmailViewModel;
                    if (textBox.Name == "fromTxtbox")
                    {
                        if (vm.From.Count > 0)
                        {
                            vm.From.RemoveAt(vm.From.Count - 1);
                        }
                    }
                    else if (textBox.Name == "toTxtbox")
                    {
                        if (vm.To.Count > 0)
                        {
                            vm.To.RemoveAt(vm.To.Count - 1);
                        }
                    }
                    else
                    {
                        if (vm.Cc.Count > 0)
                        {
                            vm.Cc.RemoveAt(vm.Cc.Count - 1);
                        }
                    }
                }
            }
        }

        private void TextBox_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            Add(sender);
        }

        private void Add(object sender, bool removeLastCharacter = false)
        {
            var textBox = sender as TextBox;
            if (!String.IsNullOrEmpty(textBox.Text))
            {
                var text = textBox.Text;
                if (removeLastCharacter)
                {
                    text = text.Remove(text.Length - 1);
                }

                var vm = this.DataContext as SaveEmailViewModel;
                var contact = new Contact()
                {
                    Email = text,
                };
                if (textBox.Name == "fromTxtbox")
                {
                    vm.From.Add(contact);
                }
                else if (textBox.Name == "toTxtbox")
                {
                    vm.To.Add(contact);
                }
                else
                {
                    vm.Cc.Add(contact);
                }
                textBox.Text = "";
            }
        }

        private void WrapPanel_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.Source is WrapPanel)
            {
                var wrapPanel = sender as WrapPanel;
                var childrens = VisualTreeHelper.GetChildrenCount(wrapPanel);
                var textbox = VisualTreeHelper.GetChild(wrapPanel, childrens - 1) as TextBox;
                Dispatcher.BeginInvoke(new Action(() => {
                    textbox.Focus();
                    Keyboard.Focus(textbox);
                }), System.Windows.Threading.DispatcherPriority.Input);
            }
        }

        private async void ComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            var cb = sender as ComboBox;
            var text = cb.Text;
            if (text.Length >= 3)
            {
                var vm = this.DataContext as SaveEmailViewModel;
                vm.IsActive = true;
                await vm.LoadOrganizations(text);
                vm.IsActive = false;
                cb.IsDropDownOpen = true;
            }
        }
    }
}
