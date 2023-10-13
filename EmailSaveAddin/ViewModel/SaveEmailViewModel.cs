using EmailSaveAddin.Helpers;
using EmailSaveAddin.Messages;
using EmailSaveAddin.Models;
using EmailSaveAddin.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailSaveAddin.ViewModel
{
    public class SaveEmailViewModel : ViewModelBase
    {
        public ObservableCollection<Contact> From { get; set; }
        public ObservableCollection<Contact> To { get; set; }
        public ObservableCollection<Contact> Cc { get; set; }

        private IApiService _apiService;

        public ObservableCollection<string> Organizations { get; set; }

        private string _selectedOrganization;
        public string SelectedOrganization
        {
            get { return _selectedOrganization; }
            set { _selectedOrganization = value; RaisePropertyChanged(() => SelectedOrganization); }
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

        private string _subject;
        public string Subject
        {
            get { return _subject; }
            set { _subject = value; RaisePropertyChanged(() => Subject); }
        }

        public System.Action OnSave { get; set; }

        public SaveEmailViewModel(IApiService apiService)
        {
            From = new ObservableCollection<Contact>()
            {
                new Contact()
                {
                    Email = "uniaz@gmail.com",
                    FirstName = "Umar",
                    LastName = "Niaz"
                }
            };

            To = new ObservableCollection<Contact>()
            {
                new Contact()
                {
                    FirstName = "Tarun",
                    LastName = "Kapoor",
                    Email = "tarun.kapoor@gmail.com"
                },
                new Contact() 
                {
                    FirstName = "Jason",
                    LastName = "Moor",
                    Email = "jason@gmail.com"
                }
            };

            Cc = new ObservableCollection<Contact>()
            {
                new Contact()
                {
                    FirstName = "Rudy",
                    LastName = "Hobson",
                    Email = "rudy.hobson@gmail.com"
                },
                new Contact()
                {
                    FirstName = "Tom",
                    LastName = "Cleavland",
                    Email = "tom.cleavland@gmail.com"
                }
            };

            Organizations = new ObservableCollection<string>();

            _apiService = apiService;
            IsActive = false;

            Subject = "This is Test Subject";

            SaveCommand = new RelayCommand(ExecuteSaveCommand);
            LogoutCommand = new RelayCommand(ExecuteLogoutCommand);

            Messenger.Default.Register<SaveEmailViewVisibleMessage>(this, OnSaveEmailViewVisibleMessageReceived);
            Messenger.Default.Register<ValidateContactMessage>(this, OnValidateContactMessageReceived);
        }

        private void OnValidateContactMessageReceived(ValidateContactMessage message)
        {
            var contact = message.Contact;
        }

        private async void OnSaveEmailViewVisibleMessageReceived(SaveEmailViewVisibleMessage obj)
        {
            IsActive = true;
            await LoadOrganizations("");
            if (Organizations.Count > 0)
            {
                await SetOrganization(Organizations[0]);
            }
            
            IsActive = false;
        }

        internal async Task SetOrganization(string text)
        {
            var org = await _apiService.GetSelectedOrgainization(text);
            if (String.IsNullOrEmpty(org) 
                && Organizations.Count > 0)
            {
                org = Organizations[0];
            }
            SelectedOrganization = org;
        }

        internal async Task LoadOrganizations(string text)
        {
            if (Organizations.Count > 0)
            {
                if (Organizations.Any(t => t == text))
                {
                    return;
                }
            }
            var orgs = await _apiService.GetOrganizations(text);
            foreach (var item in orgs)
            {
                if (!Organizations.Any(t => t == item))
                {
                    Organizations.Add(item);
                }
            }
        }


        #region SaveCommand

        public RelayCommand SaveCommand { get; private set; }

        public void ExecuteSaveCommand()
        {
            OnSave();
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
