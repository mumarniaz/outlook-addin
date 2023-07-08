using EmailSaveAddin.Helpers;
using EmailSaveAddin.Messages;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace EmailSaveAddin.ViewModel
{
    public class SaveEmailViewModel
    {
        public ObservableCollection<string> From { get; set; }
        public ObservableCollection<string> To { get; set; }
        public ObservableCollection<string> Cc { get; set; }
        public SaveEmailViewModel()
        {
            From = new ObservableCollection<string>()
            {
                "uniaz@gmail.com"
            };

            To = new ObservableCollection<string>()
            {
                "Tarun Kapoor", "jason@gmail.com"
            };

            Cc = new ObservableCollection<string>()
            {
                "Rudy Hobson", "Tom Cleavland"
            };

            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            LogoutCommand = new RelayCommand(ExecuteLogoutCommand);
        }


        #region SaveCommand

        public RelayCommand SaveCommand { get; private set; }

        public void ExecuteSaveCommand()
        {
            MessageBox.Show("Saved successfully!", "Addin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MessengerHelper.BroadcastMessage(new ClosePaneMessage());
        }

        #endregion

        #region LogoutCommand

        public RelayCommand LogoutCommand { get; private set; }

        public void ExecuteLogoutCommand()
        {
            MessengerHelper.BroadcastMessage(new LoginMessage() { IsLoggedIn = false });
        }

        #endregion
    }
}
