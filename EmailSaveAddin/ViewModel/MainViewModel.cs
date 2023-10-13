using EmailSaveAddin.Helpers;
using EmailSaveAddin.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace EmailSaveAddin.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool _loginViewVisible;
        public bool LoginViewVisible
        {
            get { return _loginViewVisible; }
            set { _loginViewVisible = value; RaisePropertyChanged(() => LoginViewVisible); }
        }

        private bool _saveEmailViewVisible;
        public bool SaveEmailViewVisible
        {
            get { return _saveEmailViewVisible; }
            set { _saveEmailViewVisible = value; RaisePropertyChanged(() => SaveEmailViewVisible); }
        }

        private bool _saveContactViewVisible;
        public bool SaveContactViewVisible
        {
            get { return _saveContactViewVisible; }
            set { _saveContactViewVisible = value; RaisePropertyChanged(() => SaveContactViewVisible); }
        }

        public MainViewModel()
        {
            LoginViewVisible = true;
            SaveEmailViewVisible = false;
            SaveContactViewVisible = false;

            Messenger.Default.Register<LoginMessage>(this, OnLoginMessageReceived);
            Messenger.Default.Register<ContactSavedMessage>(this, OnContactSavedMessageReceived);
            Messenger.Default.Register<AddContactMessage>(this, OnAddContactMessageReceived);
        }

        private void OnAddContactMessageReceived(AddContactMessage message)
        {
            ClearVisible();
            SaveContactViewVisible = true;
        }

        private void OnContactSavedMessageReceived(ContactSavedMessage message)
        {
            ClearVisible();
            SaveEmailViewVisible = true;
        }

        private void OnLoginMessageReceived(LoginMessage obj)
        {
            ClearVisible();
            if (obj.IsLoggedIn)
            {
                SaveEmailViewVisible = true;
                Utilities.IsSignedIn = true;
                MessengerHelper.BroadcastMessage(new SaveEmailViewVisibleMessage());
            }
            else
            {
                Utilities.IsSignedIn = false;
                LoginViewVisible = true;
            }
        }

        private void ClearVisible()
        {
            LoginViewVisible = false;
            SaveEmailViewVisible = false;
            SaveContactViewVisible = false;
        }
    }
}