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
    
    public partial class AddWaypoint : Window
    {
        Waypoint waypoint;
        WaypointRepository waypointRepository;
        int tripId;
        public AddWaypoint(Trip trip)
        {
            InitializeComponent();
            tripId = trip.TripId;
            label_TripName.Content = trip.Name;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            waypoint = new Waypoint();
            waypointRepository = new WaypointRepository();
            waypoint.TripId = tripId;
            this.DataContext = waypoint;
        }

      
        private void button_addWaypoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                waypointRepository.Persist(waypoint);
                var owner = this.Owner as MainWindow;
                owner.addWaypoint(waypoint);
                MessageBox.Show("Waypoint was inserted");
                CloseWindow();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_cancel_click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            this.Close();
            this.Owner.Focus();
        }
    }
}
