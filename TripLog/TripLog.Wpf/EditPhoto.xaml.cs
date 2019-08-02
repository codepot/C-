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
using Microsoft.Win32;
using System.IO;

namespace TripLog.Wpf
{
    /// <summary>
    /// Interaction logic for EditPhoto.xaml
    /// </summary>
    public partial class EditPhoto : Window
    {
        Photo photo;
        PhotoRepository photoRepository;
        Converters.AttachmentToImageSourceConverter converter = new Converters.AttachmentToImageSourceConverter();

        public EditPhoto(Photo _photo)
        {
            InitializeComponent();
            photo = _photo;
            image_tripPhoto.Source = (ImageSource)converter.Convert(photo, null, null, null);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            photoRepository = new PhotoRepository();
            this.DataContext = photo;

            label_tripName.Content = photo.TripName;
            TextBox_Caption.Focus();

        }

        private void button_updatePhoto_click(object sender, RoutedEventArgs e) {
            try {
                photoRepository.UpdatePhoto(photo);

                MainWindow owner = this.Owner as MainWindow;
                owner.updatePhoto(photo);
                MessageBox.Show("Photo was updated.");
                CloseWindow();
            }
            catch(System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

    }
        private void CloseWindow()
        {
            this.Close();
            this.Owner.Focus();
        }

        private void button_cancel_click(object sender, RoutedEventArgs e)
        {            
            int index = photo.Index;
            Photo photoInDB = photoRepository.Fetch(photo.PhotoId).FirstOrDefault<Photo>();
            MainWindow mainWindow = Owner as MainWindow;
            Trip trip = mainWindow.getTrip(photoInDB.TripId.Value);
            trip.Photos.RemoveAt(index);
            trip.Photos.Insert(index, photoInDB);
            mainWindow.listBox_photos.SelectedIndex = index;
            CloseWindow();            
        }

        private void button_changePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            // Set filter options and filter index.
            dlg.Filter = "Image Files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            dlg.FilterIndex = 1;
            if (dlg.ShowDialog() == true)
            {
                Photo newPhoto = LoadPhotoFromFile(dlg.FileName);
                newPhoto.TripId = photo.TripId;
                newPhoto.TripName = photo.TripName;
                newPhoto.Name = photo.Name;
                newPhoto.Description = photo.Description;
                this.DataContext = null;
                image_tripPhoto.Source = (ImageSource)converter.Convert(newPhoto, null, null, null);
                this.DataContext = newPhoto;
                this.photo = newPhoto;

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

            //((Trip)this.DataContext).Photos.Add(photo);
            return photo;
        }

        private void button_deletePhoto_click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MainWindow owner = this.Owner as MainWindow;
                photoRepository.DeletePhoto(photo);
                owner.RemovePhoto(photo);
                MessageBox.Show("Photo was deleted.");
                CloseWindow();
            }
        }
    }
}
