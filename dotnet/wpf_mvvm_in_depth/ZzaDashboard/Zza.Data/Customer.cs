﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zza.Data
{
    public class Customer : INotifyPropertyChanged
    {
        private string _firstName;

        public Customer()
        {
            Orders = new List<Order>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string City { get; set; }

        public string Email { get; set; }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FirstName)));
                }
            }
        }

        public string FullName { get { return FirstName + " " + LastName; } }

        [Key]
        public Guid Id { get; set; }

        public string LastName { get; set; }

        public List<Order> Orders { get; set; }

        public string Phone { get; set; }

        public string State { get; set; }

        public Guid? StoreId { get; set; }

        public string Street { get; set; }

        public string Zip { get; set; }
    }
}