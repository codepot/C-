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
    public partial class EditWaypoint : Window
    {
        Waypoint currentWaypoint;
        WaypointRepository waypointRepository;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            waypointRepository = new WaypointRepository();
            this.DataContext = currentWaypoint;
        }

        public EditWaypoint(Waypoint _currentWaypoint, String streetName)
        {
            currentWaypoint = _currentWaypoint;
            InitializeComponent();
            this.label_TripName.Content = streetName;
        }

        private void button_cancel_click(object sender, RoutedEventArgs e)
        {
            int index = currentWaypoint.Index;
            Waypoint waypoint = waypointRepository.Fetch(currentWaypoint.WaypointId).FirstOrDefault<Waypoint>();
            MainWindow mainWindow = Owner as MainWindow;
            Trip trip = mainWindow.getTrip(currentWaypoint.TripId);
            trip.Waypoints.RemoveAt(index);
            trip.Waypoints.Insert(index, waypoint);
            mainWindow.listBox_waypoints.SelectedIndex = index;
            CloseWindow();
        }

        private void CloseWindow()
        {
            this.Close();
            this.Owner.Focus();
        }

        private void button_updateWaypoint_click(object sender, RoutedEventArgs e)
        {
            try
            {
                waypointRepository.UpdateWaypoint(currentWaypoint);
                MainWindow owner = this.Owner as MainWindow;
                owner.updateWaypoint(currentWaypoint);
                MessageBox.Show("Waypoint was updated.");
                CloseWindow();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_deleteWaypoint_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MainWindow owner = this.Owner as MainWindow;
                waypointRepository.DeleteWaypoint(currentWaypoint);
                owner.RemoveWayPoint(currentWaypoint);
                MessageBox.Show("Waypoint was deleted.");
                CloseWindow();
            }
        }
    }
}
