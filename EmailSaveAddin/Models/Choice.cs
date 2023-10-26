using GalaSoft.MvvmLight;

namespace EmailSaveAddin.Models
{
    public class Choice : ViewModelBase
    {
        private string _option;
        public string Option
        {
            get { return _option; }
            set { _option = value; RaisePropertyChanged(() => Option); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; RaisePropertyChanged(() => IsSelected); }
        }
    }
}
