using EasyModbus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfScada.Models
{
    public class clsConnection : INotifyPropertyChanged
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

        private string type;
        public string Type
        {
            get => type;
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }
        }

        private string ip;
        public string IP
        {
            get => ip;
            set
            {
                if (ip != value)
                {
                    ip = value;
                    OnPropertyChanged();
                }
            }
        }

        private string port;
        public string Port
        {
            get => port;
            set
            {
                if (port != value)
                {
                    port = value;
                    OnPropertyChanged();
                }
            }
        }

        private string unit;
        public string Unit
        {
            get => unit;
            set
            {
                if (unit != value)
                {
                    unit = value;
                    OnPropertyChanged();
                }
            }
        }





        //public Modbus Modbus
        //{
        //    get;
        //    set;
        //}

        public ModbusClient ModbusClient { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }


}
