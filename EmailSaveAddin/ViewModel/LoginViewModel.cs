using EmailSaveAddin.Helpers;
using EmailSaveAddin.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace EmailSaveAddin.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private PasswordBox _passwordBox;

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(() => Password); }
        }

        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
                RaisePropertyChanged(() => IsActive);
            }
        }

        private bool _isError;
        public bool IsError
        {
            get { return _isError; }
            set { _isError = value;  RaisePropertyChanged(() => IsError); }
        }

        private bool _showPassword;

        public bool ShowPassword
        {
            get { return _showPassword; }
            set { _showPassword = value; RaisePropertyChanged(() => ShowPassword); }
        }

        public LoginViewModel( )
        {
            SignInCommand = new RelayCommand(ExecuteSignIn, CanExecute);
            PasswordChangedCommand = new RelayCommand<object>(ExecutePasswordChange);

#if DEBUG
            UserName = "testemail@gmail.com";
#endif
            IsActive = false;
            IsError = false;
        }

        #region PasswordChangeCommand

        public RelayCommand<object> PasswordChangedCommand { get; private set; }

        public void ExecutePasswordChange(object obj)
        {
            _passwordBox = obj as PasswordBox;
            if (_passwordBox != null)
            {
                Password = _passwordBox.Password;
                SignInCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region SignInCommand

        public RelayCommand SignInCommand { get; private set; }

        public bool CanExecute()
        {
            ShowPassword = !String.IsNullOrEmpty(Password);
            if (!String.IsNullOrEmpty(UserName)
                && !String.IsNullOrEmpty(Password))
            {
                return true;
            }
            return false;
        }

        public async void ExecuteSignIn()
        {
            IsError = false;
            IsActive = true;

            await Task.Delay(2000);

            if (Password == "1234")
            {
                IsError = true;
                IsActive = false;
                return;
            }

            MessengerHelper.BroadcastMessage(new LoginMessage() { IsLoggedIn = true });

            IsActive = false;
        }
        #endregion
    }
}
