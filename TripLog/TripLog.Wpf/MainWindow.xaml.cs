using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TripLog.DataAccess.Ado;
using TripLog.Domain;
using Ucla.Common.ImageUtility;

namespace TripLog.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TripRepository tripRepository;
        ObservableCollection<Trip> trips;
        public MainWindow()
        {
            InitializeComponent();
            setVisibility(Visibility.Hidden);
        }

        public Trip getTrip(int tripId)
        {
            return trips.Where(t => t.TripId == tripId).Single<Trip>();
        }

        public void insertTrip(int index, Trip trip)
        {
            trips.Insert(index, trip);
        }

        public void removeTrip(int index)
        {
            trips.RemoveAt(index);
        }

        internal void addWaypoint(Waypoint waypoint)
        {
            getTrip(waypoint.TripId).Waypoints.Add(waypoint);
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tripRepository = new TripRepository();
            var list = tripRepository.Fetch();
            trips = new ObservableCollection<Trip>(list);
            this.DataContext = trips;
        }

        internal void updatePhoto(Photo photo)
        {
           // Trip trip = trips.Where(t => t.TripId == photo.TripId).Single<Trip>();
            Trip trip = getTrip(photo.TripId.Value);
            int tripIndex = trips.IndexOf(trip);
            int photoIndex = photo.Index;

            trips[tripIndex].Photos.RemoveAt(photoIndex);
            trips[tripIndex].Photos.Insert(photoIndex, photo);

            this.listBox_trips.SelectedIndex = tripIndex;
            this.listBox_photos.SelectedIndex = photoIndex;
        }

        internal void RemoveTrip(Trip currentTrip)
        {
            trips.Remove(currentTrip);
        }

        private void btn_editTrip_click(object sender, RoutedEventArgs e)
        {
            Trip currentTrip = listBox_trips.SelectedItem as Trip;            
            if (currentTrip != null)
            {
                currentTrip.Index = listBox_trips.SelectedIndex;
                EditTrip editTrip = new EditTrip(currentTrip);
                editTrip.Owner = this;
                var blur = new BlurEffect();
                blur.Radius = 8;
                this.Effect = blur;
                editTrip.ShowDialog();
                this.Effect = null;
            }
            else
            {
                MessageBox.Show("Please select a trip.");
            }

        }

        internal void RemoveWayPoint(Waypoint currentWaypoint)
        {
            getTrip(currentWaypoint.TripId).Waypoints.Remove(currentWaypoint);
        }

        
        internal void updateWaypoint(Waypoint currentWaypoint)
        {
            
            Trip trip = trips.Where(t => t.TripId == currentWaypoint.TripId).Single<Trip>();
            int tripIndex = trips.IndexOf(trip);
            int waypointIndex = currentWaypoint.Index;

            trips[tripIndex].Waypoints.RemoveAt(waypointIndex);
            trips[tripIndex].Waypoints.Insert(waypointIndex, currentWaypoint);

            this.listBox_trips.SelectedIndex = tripIndex;
            this.listBox_waypoints.SelectedIndex = waypointIndex;
        }

        internal void RemovePhoto(Photo photo)
        {
            getTrip(photo.TripId.Value).Photos.Remove(photo);
        }

        internal void addPhoto(Photo photo)
        {
            getTrip(photo.TripId.Value).Photos.Add(photo);
        }

        internal void updateTrip(Trip currentTrip)
        {
            int index = currentTrip.Index;
            trips.RemoveAt(index);
            trips.Insert(index, currentTrip);
            this.listBox_trips.SelectedIndex = index;
        }

        private void button_addTrip_click(object sender, RoutedEventArgs e)
        {
            AddTrip addTrip = new AddTrip();
            addTrip.Owner = this;
            var blur = new BlurEffect();
            blur.Radius = 8;
            this.Effect = blur;
            addTrip.ShowDialog();
            this.Effect = null;
        }


        public void addTrip(Trip aTrip)
        {
            trips.Add(aTrip);
            listBox_trips.SelectedItem = aTrip;
        }

        private void btn_addWaypoint_click(object sender, RoutedEventArgs e)
        {
            int index = listBox_trips.SelectedIndex;
            if (index >= 0)
            {                
                Trip selectedTrip = listBox_trips.SelectedItem as Trip;
                AddWaypoint addWaypoint = new AddWaypoint(selectedTrip);
                addWaypoint.Owner = this;
                var blur = new BlurEffect();
                blur.Radius = 8;
                this.Effect = blur;
                addWaypoint.ShowDialog();
                this.Effect = null;

            }
            else
            {
                MessageBox.Show("Please select a Trip that the new waypoint belongs to.");
            }
        }

        private void listBox_waypoints_doubleClick(object sender, MouseButtonEventArgs e)
        {
            Waypoint selectedWayPoint = listBox_waypoints.SelectedItem as Waypoint;
            if (selectedWayPoint != null)
            {

                selectedWayPoint.Index = listBox_waypoints.SelectedIndex;
                string tripName = getTrip(selectedWayPoint.TripId).Name;
                EditWaypoint editWaypoint = new EditWaypoint(selectedWayPoint, tripName);
                editWaypoint.Owner = this;

                var blur = new BlurEffect();
                blur.Radius = 8;
                this.Effect = blur;

                editWaypoint.ShowDialog();

                this.Effect = null;
            }
        }

        private void listBox_trips_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.listBox_trips.SelectedIndex >= 0) {
                Trip trip = listBox_trips.SelectedItem as Trip;
            setVisibility(Visibility.Visible);
            

        }
            else
                setVisibility(Visibility.Hidden);
            
        }

        private void setVisibility(Visibility visiblily)
        {
            this.button_editTrip.Visibility = visiblily;
            this.button_addWaypoint.Visibility = visiblily;
            this.button_addPhoto.Visibility = visiblily;
        }

        private void btn_addPhoto_click(object sender, RoutedEventArgs e)
        {
            if (this.listBox_trips.SelectedIndex >= 0)
            {
                Trip trip = listBox_trips.SelectedItem as Trip;
                var blur = new BlurEffect();
                blur.Radius = 8;
                this.Effect = blur;
                OpenFileDialog dlg = new OpenFileDialog();

                // Set filter options and filter index.
                dlg.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
                dlg.FilterIndex = 1;
                if (dlg.ShowDialog() == true)
                {
                    Photo photo = LoadPhotoFromFile(dlg.FileName);
                    photo.TripName = trip.Name;
                    photo.TripId = trip.TripId;
                    AddPhoto addPhoto = new AddPhoto(photo);
                    addPhoto.Owner = this;
                    addPhoto.ShowDialog();
                }
                this.Effect = null;
            }
        }


        private Photo LoadPhotoFromFile(string fileName)
        {
            var photo = new Photo
            {
                ExportFileName = System.IO.Path.GetFileNameWithoutExtension(fileName),
                PhotoFormat = System.IO.Path.GetExtension(fileName).Replace(".", ""),
                PhotoBytes = File.ReadAllBytes(fileName),
                
            InternalFileName = Guid.NewGuid().ToString()
            };
            photo.Thumbnail = ImageLib.ResizeImageBytes(photo.PhotoBytes, 256, 256);
            return photo;
        }

        private void btn_editPhoto_click(object sender, RoutedEventArgs e)
        {
            if(listBox_photos.SelectedIndex >= 0) {
            Photo selectedPhoto = listBox_photos.SelectedItem as Photo;
            if (selectedPhoto != null)
            {

                    selectedPhoto.Index = listBox_photos.SelectedIndex;
                    selectedPhoto.TripName = this.label_tripName.Content.ToString();
                    EditPhoto editPhoto = new EditPhoto(selectedPhoto);
                    editPhoto.Owner = this;

                var blur = new BlurEffect();
                blur.Radius = 8;
                this.Effect = blur;

                    editPhoto.ShowDialog();

                this.Effect = null;
            }
            }
        }

        private void button_editWaypoint_click(object sender, RoutedEventArgs e)
        {
            Waypoint selectedWayPoint = listBox_waypoints.SelectedItem as Waypoint;
            if (selectedWayPoint != null)
            {

                selectedWayPoint.Index = listBox_waypoints.SelectedIndex;
                string tripName = getTrip(selectedWayPoint.TripId).Name;
                EditWaypoint editWaypoint = new EditWaypoint(selectedWayPoint, tripName);
                editWaypoint.Owner = this;

                var blur = new BlurEffect();
                blur.Radius = 8;
                this.Effect = blur;

                editWaypoint.ShowDialog();

                this.Effect = null;
            }
        }

        private void listBox_photos_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenPhoto();

        }

        private void OpenPhoto()
        {
            try
            {
                Photo photo = listBox_photos.SelectedItem as Photo;
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var fullPath = System.IO.Path.Combine(path, photo.ExportFileName) + "." + photo.PhotoFormat;
                File.WriteAllBytes(fullPath, photo.PhotoBytes);
                Process.Start(fullPath);

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error opening file",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Other exception type",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
