using EmailSaveAddin.Helpers;
using EmailSaveAddin.Messages;
using EmailSaveAddin.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace EmailSaveAddin.ViewModel
{
    public class SaveContactViewModel : ViewModelBase
    {
        private Contact _contact;
        public Contact Contact
        {
            get { return _contact; }
            set { _contact = value; RaisePropertyChanged(() => Contact); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; RaisePropertyChanged(() => FirstName); SaveCommand.RaiseCanExecuteChanged(); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; RaisePropertyChanged(() => LastName); SaveCommand.RaiseCanExecuteChanged(); }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; RaisePropertyChanged(() => Email); SaveCommand.RaiseCanExecuteChanged(); }
        }

        public ObservableCollection<Choice> Choices { get; set; }

        private Choice _selectedChoice;
        public Choice SelectedChoice
        {
            get { return _selectedChoice; }
            set
            {
                _selectedChoice = value;
                RaisePropertyChanged(() => SelectedChoice);
            }
        }

        public SaveContactViewModel()
        {
            SaveCommand = new RelayCommand(ExecuteSaveCommand, CanExecute);
            CancelCommand = new RelayCommand(ExecuteCancelCommand);

            Messenger.Default.Register<ContactMessage>(this, OnContactMessageReceived);

            Choices = new ObservableCollection<Choice>()
            {
                new Choice()
                {
                    Option = "LLC Organization",
                    IsSelected = false
                },
                new Choice()
                {
                    Option = "Shell Company Ltd.",
                    IsSelected = false
                },
                new Choice()
                {
                    Option = "M&M Private Ltd.",
                    IsSelected = false
                },
                new Choice()
                {
                    Option = "Shell Company Ltd.",
                    IsSelected = false
                },
                new Choice()
                {
                    Option = "M&M Private Ltd.",
                    IsSelected = false
                }
            };
        }

        private void OnContactMessageReceived(ContactMessage message)
        {
            Contact = message.Contact;
            FirstName = Contact.FirstName;
            LastName = Contact.LastName;
            Email = Contact.Email;
        }

        #region SaveCommand

        public bool CanExecute()
        {
            if (!String.IsNullOrEmpty(FirstName)
                && !String.IsNullOrEmpty(LastName)
                && !String.IsNullOrEmpty(Email))
            {
                return true;
            }
            return false;
        }

        public RelayCommand SaveCommand { get; private set; }

        public void ExecuteSaveCommand()
        {
            MessageBox.Show("Saved successfully!", "Addin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Go back
            MessengerHelper.BroadcastMessage(new ContactSavedMessage());
            // Send message to Email view to validate the current contact
            MessengerHelper.BroadcastMessage(new ValidateContactMessage() { Contact = Contact });
        }

        #endregion

        #region CancelCommand

        public RelayCommand CancelCommand { get; private set; }

        public void ExecuteCancelCommand()
        {
            MessengerHelper.BroadcastMessage(new ContactSavedMessage());
        }


        #endregion
    }
}
