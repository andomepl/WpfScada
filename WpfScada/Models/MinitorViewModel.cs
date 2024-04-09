using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfScada.Models
{
    public class MinitorViewModel : INotifyPropertyChanged
    {



        private int _singleValue = 0;

        public int SingleValue
        {
            get => _singleValue;
            set
            {
                if (value == _singleValue) return;

                _singleValue = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<string> minitorComboxItems = new ObservableCollection<string>();

        public ObservableCollection<string> MinitorComboxItems
        {
            get=> minitorComboxItems;
        }

        private int minitorItemIndex = 0;

        public int MinitorItemIndex
        {
            get => minitorItemIndex;
            set
            {
                if (value == minitorItemIndex) return;

                minitorItemIndex = value;
                OnPropertyChanged();
            }
        }

        private string deviceName;

        public string DeviceName
        {
            get => deviceName;
            set
            {
                if(value == deviceName) return; 

                deviceName = value;
                OnPropertyChanged();
            }
        }


        private string iPAddress;
        public string IPAddress
        {
            get => iPAddress;
            set
            {
                if (value == iPAddress) return;

                iPAddress = value;
                OnPropertyChanged();
            }
        }

        private string registerAddress;


        public string RegisterAddress
        {
            get => registerAddress;
            set
            {
                if (value == registerAddress) return;

                registerAddress = value;
                OnPropertyChanged();
            }
        }



        public SeriesCollection SeriesCollection { get; set; }=new SeriesCollection();


        public SeriesCollection StepSeriesCollection { get; set; }=new SeriesCollection();


        public ChartValues<int> charts { get; set;} = new ChartValues<int>();
        public MinitorViewModel()
        {

            charts.CollectionChanged += Charts_CollectionChanged;


            SeriesCollection.Add(
                new LineSeries()
                {
                    Title="MinitorTag",
                    Values= charts,
                    PointGeometry = DefaultGeometries.Circle,
                    LineSmoothness=0,
                    PointGeometrySize=12
                });

            StepSeriesCollection.Add(
                new StepLineSeries()
                { 
                    Values=charts,
                    PointGeometry = DefaultGeometries.Diamond,
                    StrokeThickness=3
                }
                );


        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        }

        private void Charts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && ((ChartValues<int>)sender).Count > 10)
            {
                ((ChartValues<int>)sender).RemoveAt(0);
            }

        }
    }
}
