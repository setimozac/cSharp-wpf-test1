using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string FILEPATH = @"..\..\trips.txt";
        List<Trip> trips = new List<Trip>();
        public MainWindow()
        {
            InitializeComponent();

            ReadFromFile();
            UpdateContent();
        }

        public void ReadFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(FILEPATH))
                {
                    string line = string.Empty;
                    while ((line = sr.ReadLine()) != null)
                    {
                        
                        Trip trip = new Trip(line);
                        trips.Add(trip);
                    }
                }

            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The file was not found");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show($"The directory was not found");
            }
            catch (IOException)
            {
                MessageBox.Show($"The file could not be opened");
            }
            LvGvTrip.Items.Refresh();
        }

        public void UpdateContent()
        {
            BtnDelete.IsEnabled = false;
            BtnUpdate.IsEnabled = false;
            BtnAdd.IsEnabled = true;
            LvGvTrip.ItemsSource = trips;
            LvGvTrip.Items.Refresh();
            TboxDestination.Text = "";
            TboxName.Text = "";
            TboxPassport.Text = "";
            DPickerDeparture.SelectedDate = DateTime.Today;
            DPickerReturn.SelectedDate = DateTime.Today;
            LvGvTrip.SelectedIndex = -1;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            string destination = TboxDestination.Text;
            string name = TboxName.Text;
            string passport = TboxPassport.Text;
            DateTime departure = (DateTime)DPickerDeparture.SelectedDate;
            DateTime retTime = (DateTime)DPickerReturn.SelectedDate;
            if(departure > retTime)
            {
                MessageBox.Show("Departure Date can't be after Return date!");
                return;
            }
            Trip trip = new Trip(destination,name,passport,departure,retTime);
            
            if(!trips.Contains(trip))
            {
                if(trip != null)
                {
                    trips.Add(trip);
                    UpdateContent();
                }
                
            }
            else
            {
                MessageBox.Show("this trip already exists");
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FILEPATH))
                {
                    foreach (Trip trip in trips)
                    {
                        writer.WriteLine(trip.ToString());
                    }

                }

            }
            catch
            {
                MessageBox.Show("There is a problem with writing records to txt file!");
            }
        }

        private void LvGvTrip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LvGvTrip.SelectedIndex != -1)
            {
                Trip selectedItem = (Trip)LvGvTrip.SelectedItem;
                BtnDelete.IsEnabled = true;
                BtnUpdate.IsEnabled = true;
                TboxDestination.Text = selectedItem.Destination;
                TboxName.Text = selectedItem.Name;
                TboxPassport.Text = selectedItem.Passport;
                DPickerDeparture.SelectedDate = selectedItem.Departure;
                DPickerReturn.SelectedDate = selectedItem.Return;
            }
            

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if ((DateTime)DPickerDeparture.SelectedDate > (DateTime)DPickerReturn.SelectedDate)
            {
                MessageBox.Show("Departure Date can't be after Return date!");
                return;
            }
            Trip ItemToUpdate = (Trip)LvGvTrip.SelectedItems[0];
            ItemToUpdate.Destination = TboxDestination.Text;
            ItemToUpdate.Name = TboxName.Text;
            ItemToUpdate.Passport = TboxPassport.Text;
            ItemToUpdate.Departure = (DateTime)DPickerDeparture.SelectedDate;
            ItemToUpdate.Return = (DateTime)DPickerReturn.SelectedDate;
            UpdateContent();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            Trip itemToDelete = (Trip)LvGvTrip.SelectedItems[0];
            MessageBoxResult result = MessageBox.Show($"Are You sure? \n Deleting trip to {itemToDelete.Destination}!", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                trips.Remove(itemToDelete);
                UpdateContent();
            }

                
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var tt = LvGvTrip.SelectedItems;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "TRIPS (*.trips)|*.trips";
            saveFileDialog.FileName = "trips";
            saveFileDialog.Title = "Saving all the records";
            if (saveFileDialog.ShowDialog() == true)
            {
                

                try
                {
                    using (var writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        foreach (Trip trip in tt)
                        {
                            writer.WriteLine(trip.ToString());
                        }
                    }

                }
                catch
                {
                    MessageBox.Show("Something went wrong with writing to the file!");
                }
                
            }


        }

        
    }
}
