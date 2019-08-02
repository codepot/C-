using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TripLog.Domain;
using TripLog.DataAccess.Ado;

namespace TripLog.Wpf
{

    public partial class EditTrip : Window
    {
        TripRepository tripRepository;
        Trip currentTrip;
        public EditTrip(Trip trip)
        {
            currentTrip = trip;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tripRepository = new TripRepository();
            this.DataContext = currentTrip;
        }

        private void button_cancel_click(object sender, RoutedEventArgs e)
        {
            int index = currentTrip.Index;
            Trip tripInDB = tripRepository.Fetch(currentTrip.TripId).FirstOrDefault<Trip>();
            currentTrip = tripInDB;
            MainWindow mainWindow = Owner as MainWindow;
            mainWindow.removeTrip(index);
            mainWindow.insertTrip(index, tripInDB);
            mainWindow.listBox_trips.SelectedIndex = index;
            CloseWindow();
        }

        private void CloseWindow()
        {
            this.Close();
            this.Owner.Focus();
        }

        private void button_updateTrip_click(object sender, RoutedEventArgs e)
        {
            try
            {
                tripRepository.UpdateTrip(currentTrip);
                MainWindow owner = this.Owner as MainWindow;
                owner.updateTrip(currentTrip);
                MessageBox.Show("Trip was updated.");
                CloseWindow();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button_deleteTrip_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MainWindow owner = this.Owner as MainWindow;
                tripRepository.DeleteWaypoint(currentTrip);
                owner.RemoveTrip(currentTrip);
                MessageBox.Show("Trip was deleted.");
                CloseWindow();
            }
        }
    }
}
