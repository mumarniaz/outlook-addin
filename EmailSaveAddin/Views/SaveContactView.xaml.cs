using EmailSaveAddin.Models;
using EmailSaveAddin.ViewModel;
using System.Windows.Controls;

namespace EmailSaveAddin.Views
{
    /// <summary>
    /// Interaction logic for SaveContactView.xaml
    /// </summary>
    public partial class SaveContactView : UserControl
    {
        public SaveContactView()
        {
            InitializeComponent();
        }

        private void Cb_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            Choice choice = chk.DataContext as Choice;
            choice.IsSelected = chk.IsChecked.Value;
            combobox.SelectedItem = combobox.SelectedItem == choice ? null : choice;

            if (combobox.SelectedItem == null)
            {
                var list = (combobox.DataContext as SaveContactViewModel).Choices;
                foreach (var item in list)
                {
                    if (item.IsSelected)
                    {
                        combobox.SelectedItem = item;
                    }
                }
            }
        }
    }
}
