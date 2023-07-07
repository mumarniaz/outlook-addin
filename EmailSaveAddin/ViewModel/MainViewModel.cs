using EmailSaveAddin.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Runtime.Remoting.Contexts;

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

        public MainViewModel()
        {
            LoginViewVisible = true;
            SaveEmailViewVisible = false;

            Messenger.Default.Register<LoginMessage>(this, OnLoginMessageReceived);
        }

        private void OnLoginMessageReceived(LoginMessage obj)
        {
            ClearVisible();
            if (obj.IsLoggedIn)
            {
                SaveEmailViewVisible = true;
            }
            else
            {
                LoginViewVisible = true;
            }
        }

        private void ClearVisible()
        {
            LoginViewVisible = false;
            SaveEmailViewVisible = false;
        }
    }
}