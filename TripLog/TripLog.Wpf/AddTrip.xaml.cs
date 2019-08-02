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

    public partial class AddTrip : Window
    {
        Trip trip;
        TripRepository tripRepository;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            trip = new Trip();
            tripRepository = new TripRepository();
            //trip = tripRepository.Fetch().First();
            this.DataContext = trip;
        }

        public AddTrip()
        {
            InitializeComponent();
            this.textBox_tripName.Text = String.Empty;
            this.textBox_tripDescription.Text = String.Empty;
            this.textBox_tripName.Focus();
        }

        private void button_addTrip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //trip.IsDirty = true;
                
                tripRepository.Persist(trip);                
                var owner = this.Owner as MainWindow;
                owner.addTrip(trip);
                MessageBox.Show("Trip to "+trip.Name + " was created.");
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
