using GalaSoft.MvvmLight;

namespace EmailSaveAddin.Models
{
    public class Contact : ViewModelBase
    {
		private string _firstName;
		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; RaisePropertyChanged(() => FirstName); }
		}

		private string _lastName;
		public string LastName
		{
			get { return _lastName; }
			set { _lastName = value; RaisePropertyChanged(() => LastName); }
		}

		private string _email;
		public string Email
		{
			get { return _email; }
			set { _email = value; RaisePropertyChanged(() => Email); }
		}

        public override string ToString()
        {
			if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
			{
				return $"{FirstName} {LastName}";

			}
			else if (!string.IsNullOrEmpty(FirstName))
			{
				return FirstName;
			}
			else
			{
				return Email;
			}
        }

        public override bool Equals(object obj)
        {
            // Check for null and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Contact other = (Contact)obj;

            // Compare the properties of the two objects for equality.
            return string.Equals(FirstName, other.FirstName) &&
                   string.Equals(LastName, other.LastName) &&
                   string.Equals(Email, other.Email);
        }

        public override int GetHashCode()
        {
            // Generate a hash code based on the properties used in Equals.
            int hash = 17;
            hash = hash * 23 + (FirstName != null ? FirstName.GetHashCode() : 0);
            hash = hash * 23 + (LastName != null ? LastName.GetHashCode() : 0);
            hash = hash * 23 + (Email != null ? Email.GetHashCode() : 0);
            return hash;
        }
    }
}
