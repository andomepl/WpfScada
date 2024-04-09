using EasyModbus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfScada.Models;

namespace WpfScada
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        private ObservableCollection<clsConnection> myConnections = new ObservableCollection<clsConnection>();
        public ObservableCollection<clsConnection> MyConnections
        {
            get => myConnections;
        }

        private ObservableCollection<clsTag> myTags = new ObservableCollection<clsTag>();
        public ObservableCollection<clsTag> MyTags
        {
            get => myTags;
        }



        public MinitorViewModel MinitorViewModel = new MinitorViewModel();


        public MainWindow()
        {
            InitializeComponent();


            Graphicminitor.DataContext = MinitorViewModel;
            dgConnection.ItemsSource = MyConnections;
            dgTags.ItemsSource = MyTags;
        }

        private void btnConAdd_Click(object sender, RoutedEventArgs e)
        {
            txtConMode.Text = "Add";
            txtConId.Text = MyConnections.Count.ToString();
            txtConName.Text = "";
            grdConEdit.Visibility = Visibility.Visible;
        }

        private void btnConEdit_Click(object sender, RoutedEventArgs e)
        {
            int idx = dgConnection.SelectedIndex;

            if (idx >= 0 && MyConnections.Count >= idx)
            {
                txtConMode.Text = "Edit";
                txtConId.Text = MyConnections[idx].ID.ToString();
                txtConName.Text = MyConnections[idx].Name;
                cboConType.Text = MyConnections[idx].Type;
                txtConIp.Text = MyConnections[idx].IP;
                txtConPort.Text = MyConnections[idx].Port;
                txtConUid.Text = MyConnections[idx].Unit;
                grdConEdit.Visibility = Visibility.Visible;

            }
        }

        private void btnConDelete_Click(object sender, RoutedEventArgs e)
        {
            btnConEdit_Click(null, null);

            txtConMode.Text = "Delete";
        }

        private void dgConnection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = dgConnection.SelectedIndex;

            if (idx >= 0)
            {
                btnConEdit.IsEnabled = true;
                btnConDelete.IsEnabled = true;
            }
            else
            {
                btnConEdit.IsEnabled = false;
                btnConDelete.IsEnabled = false;
            }
        }

        private void btnCon_Click(object sender, RoutedEventArgs e)
        {
            if (txtConMode.Text == "Add")
            {
                clsConnection newcon = new clsConnection();

                newcon.ID = int.Parse(txtConId.Text);
                newcon.Name = txtConName.Text;
                newcon.Type = cboConType.Text;
                newcon.IP = txtConIp.Text;
                newcon.Port = txtConPort.Text;
                newcon.Unit = txtConUid.Text;


                newcon.ModbusClient = new ModbusClient(newcon.IP, int.Parse(newcon.Port));
             

                MyConnections.Add(newcon);

            }
            else if (txtConMode.Text == "Edit")
            {
                int idx = int.Parse(txtConId.Text);

                MyConnections[idx].Name = txtConName.Text;
                MyConnections[idx].Type = cboConType.Text;

                MyConnections[idx].IP = txtConIp.Text;
                MyConnections[idx].Port = txtConPort.Text;

                MyConnections[idx].Unit = txtConUid.Text;
            }
            else if (txtConMode.Text == "Delete")
            {
                int idx = int.Parse(txtConId.Text);

                MyConnections.RemoveAt(idx);

            }
            grdConEdit.Visibility = Visibility.Collapsed;
        }
        private void btnConCancel_Click(object sender, RoutedEventArgs e)
        {
            grdConEdit.Visibility = Visibility.Collapsed;
        }




        private void btnTagAdd_Click(object sender, RoutedEventArgs e)
        {
            txtTagMode.Text = "Add";
            txtTagId.Text = MyTags.Count.ToString();
            txtTagName.Text = "";

            cboTagCon.Items.Clear();
            foreach (clsConnection con in MyConnections)
            {
                cboTagCon.Items.Add(con.Name);
            }
            grdTagEdit.Visibility = Visibility.Visible;
        }

        private void btnTagEdit_Click(object sender, RoutedEventArgs e)
        {
            int idx = dgTags.SelectedIndex;

            if (idx >= 0 && MyTags.Count >= idx)
            {
                txtTagMode.Text = "Edit";
                txtTagId.Text = MyTags[idx].ID.ToString();
                txtTagName.Text = MyTags[idx].Name;
                cboTagCon.Items.Clear();

                foreach (clsConnection con in MyConnections)
                {
                    cboTagCon.Items.Add(con.Name);
                }

                cboTagCon.Text = MyTags[idx].Connection;
                txtTagAddress.Text = myTags[idx].Address;
                grdTagEdit.Visibility = Visibility.Visible;

            }
        }

        private void btnTagDelete_Click(object sender, RoutedEventArgs e)
        {
            btnTagEdit_Click(null, null);

            txtTagMode.Text = "Delete";
        }
        private void dgTags_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idx = dgTags.SelectedIndex;

            if (idx >= 0)
            {
                btnTagEdit.IsEnabled = true;
                btnTagDelete.IsEnabled = true;
            }
            else
            {
                btnTagEdit.IsEnabled = false;
                btnTagDelete.IsEnabled = false;
            }
        }

        private void btnTagOk_Click(object sender, RoutedEventArgs e)
        {
            if (txtTagMode.Text == "Add")
            {
                clsTag newTag = new clsTag();

                newTag.ID = int.Parse(txtTagId.Text);
                newTag.Name = txtTagName.Text;
                newTag.Connection = cboTagCon.Text;
                newTag.Address = txtTagAddress.Text;
                newTag.Value = "";
                MyTags.Add(newTag);

            }
            else if (txtTagMode.Text == "Edit")
            {
                int idx = int.Parse(txtTagId.Text);
                MyTags[idx].Name = txtTagName.Text;
                MyTags[idx].Name = txtTagName.Text;
                myTags[idx].Address = txtTagAddress.Text;


            }
            else if (txtTagMode.Text == "Delete")
            {
                int idx = int.Parse(txtTagId.Text);

                MyTags.RemoveAt(idx);

            }
            grdTagEdit.Visibility = Visibility.Collapsed;
        }

        private void btnTagCancel_Click(object sender, RoutedEventArgs e)
        {
            grdTagEdit.Visibility = Visibility.Collapsed;
        }


        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {

            CancellationToken cancellationToken = cancellationTokenSource.Token;

            MinitorViewModel.MinitorComboxItems.Clear();
            foreach (var tag in MyTags)
            {
                MinitorViewModel.MinitorComboxItems.Add(tag.Name);
            }
            MinitorViewModel.MinitorItemIndex = 0;


            ClientOpValues(cancellationToken);


            StartBtn.Visibility = Visibility.Collapsed;
            StopBtn.Visibility = Visibility.Visible;
        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {

            cancellationTokenSource.Cancel();

            cancellationTokenSource = new CancellationTokenSource();

            StartBtn.Visibility = Visibility.Visible;
            StopBtn.Visibility = Visibility.Collapsed;
        }

        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool isChecked = false;
        private void ClientOpValues(CancellationToken token)
        {
            Task.Run(async () =>
            {

                while (true)
                {
                    await Task.Delay(1000);
                    if (token.IsCancellationRequested)
                    { break; }


                    if (isChecked)
                    {
                        foreach (var tag in MyTags)
                        {

                            var modbusConnection = MyConnections.Single(t => t.Name == tag.Connection);


                            if (modbusConnection.ModbusClient == null || modbusConnection.ModbusClient.Connected == false)
                            {
                                try
                                {
                                    modbusConnection.ModbusClient?.Connect();
                                }
                                catch
                                {
                                    MessageBox.Show("Connection Error");
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        StopBtn_Click(null, null);
                                    });
                                    break;
                                }
                            }

                            modbusConnection.ModbusClient.WriteSingleRegister(int.Parse(tag.Address), int.Parse(tag.Value));

                        }

                    }
                    else if (!isChecked)
                    {
                        foreach (var tag in MyTags)
                        {

                            var modbusConnection = MyConnections.Single(t => t.Name == tag.Connection);


                            if (modbusConnection.ModbusClient == null || modbusConnection.ModbusClient.Connected == false)
                            {
                                try
                                {
                                    modbusConnection.ModbusClient?.Connect();
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message);
                                    Application.Current.Dispatcher.Invoke(() =>
                                    {
                                        StopBtn_Click(null, null);
                                    });

                                    break;
                                }
                            }

                            int[] value = modbusConnection.ModbusClient.ReadHoldingRegisters(int.Parse(tag.Address), 1);

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                tag.Value = value[0].ToString();
                            });
                        }
                    }

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        HandleGraphData();
                    });



                }
            }, token).ContinueWith(t =>
            {
                foreach (var tag in MyTags)
                {

                    var modbusConnection = MyConnections.Single(_t => _t.Name == tag.Connection);
                    if (modbusConnection.ModbusClient != null)
                    {
                        modbusConnection.ModbusClient.Disconnect();
                    }

                }

            });
        }

        private void btnTagChanged_Checked(object sender, RoutedEventArgs e)=> isChecked = true;
        private void btnTagChanged_Unchecked(object sender, RoutedEventArgs e)=>isChecked = false;


        private void HandleGraphData()
        {

            if (MinitorViewModel.MinitorItemIndex < 0)
                return;

            if (MyTags[MinitorViewModel.MinitorItemIndex] == null)
                return;

            var tagModel = MyTags[MinitorViewModel.MinitorItemIndex];

            string minitorTags = tagModel.Value;

            int value= int.Parse(minitorTags);

            MinitorViewModel.SingleValue = value;

            MinitorViewModel.charts.Add(value);


            MinitorViewModel.DeviceName = tagModel.Connection;
            MinitorViewModel.RegisterAddress = tagModel.Address;

            MinitorViewModel.IPAddress = MyConnections.FirstOrDefault(c => c.Name == tagModel.Connection)?.IP;

        }


    }
}
