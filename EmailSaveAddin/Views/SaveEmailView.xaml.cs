using EmailSaveAddin.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
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
        }

        private void Chip_DeleteClick(object sender, RoutedEventArgs e)
        {
            var chip = (sender as Chip);
            var presener = VisualTreeHelper.GetParent(chip);
            if (presener != null)
            {
                var wrapPanel = VisualTreeHelper.GetParent(presener);
                if (wrapPanel != null)
                {
                    var vm = (wrapPanel as WrapPanel).DataContext as SaveEmailViewModel;
                    var tag = chip.Tag.ToString();
                    if (tag == "TO")
                    {
                        vm.To.Remove(chip.DataContext.ToString());
                    }
                    else if (tag == "CC")
                    {
                        vm.Cc.Remove(chip.DataContext.ToString());
                    }
                    else
                    {
                        vm.From.Remove(chip.DataContext.ToString());

                    }
                }
            }
        }

        private void TextBox_PreviewKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
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
                if (textBox.Name == "fromTxtbox")
                {
                    vm.From.Add(text);
                }
                else if (textBox.Name == "toTxtbox")
                {
                    vm.To.Add(text);
                }
                else
                {
                    vm.Cc.Add(text);
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
                cb.IsDropDownOpen = true;
                var vm = this.DataContext as SaveEmailViewModel;
                vm.IsActive = true;
                await vm.LoadOrganizations(text);
                vm.IsActive = false;
            }
        }
    }
}
