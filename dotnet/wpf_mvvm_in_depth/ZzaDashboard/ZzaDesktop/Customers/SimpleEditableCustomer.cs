using System;
using System.ComponentModel.DataAnnotations;

namespace ZzaDesktop.Customers
{
    internal class SimpleEditableCustomer : ValidatableBindableBase
    {
        private string _email;

        private string _firstName;

        private Guid _id;

        private string _lastName;

        private string _phone;

        [EmailAddress]
        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                SetProperty(ref _email, value);
            }
        }

        [Required]
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                SetProperty(ref _firstName, value);
            }
        }

        public Guid Id
        {
            get
            {
                return _id;
            }

            set
            {
                SetProperty(ref _id, value);
            }
        }

        [Required]
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        [Phone]
        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                SetProperty(ref _phone, value);
            }
        }
    }
}