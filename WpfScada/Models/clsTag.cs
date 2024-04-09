using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfScada.Models
{
    public class clsTag : INotifyPropertyChanged
    {
        private int id;
        public int ID
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }

            }
        }

        private string connection;
        public string Connection
        {
            get => connection;
            set
            {
                if (connection != value)
                {
                    connection = value;
                    OnPropertyChanged();
                }
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string address;
        public string Address
        {
            get => address;
            set
            {
                if (address != value)
                {
                    address = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _value;
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string arg = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(arg));
        }
    }
}
