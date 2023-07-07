using System.Windows;
using System.Windows.Controls;

namespace EmailSaveAddin.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Button_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            passwordTxtBox.Text = passwordText.Password;
            passwordText.Visibility = Visibility.Collapsed;
            passwordTxtBox.Visibility = Visibility.Visible;
        }

        private void Button_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            passwordText.Password = passwordTxtBox.Text;
            passwordTxtBox.Visibility = Visibility.Collapsed;
            passwordText.Visibility = Visibility.Visible;
        }
    }
}
