using System;
using System.ComponentModel;
using Ucla.Common.BaseClasses;
using Ucla.Common.ImageUtility;

namespace TripLog.Domain
{
    public class Photo : AggregateRootBase, INotifyPropertyChanged
    {
        #region Fields

        private int _photoId;
        private int? _tripId;
       // private int? _waypointId;
        private string _name = String.Empty;
        private string _description = String.Empty;
        private string _location = String.Empty;
        private string _photoFormat = String.Empty;
        private string _exportFileName = String.Empty;
        private string _internalFileName = String.Empty;
        private byte[] _thumbnail;
        private byte[] _photoBytes;
        private double? _latitude;
        private double? _longitude;
        private DateTime? _photoDate;

        //for GUI
        private int _index;
        private string _tripName = String.Empty;

        #endregion

        #region Properties

        public int PhotoId
        {
            get { return _photoId; }
            set 
            {
                if (_photoId == value) return;
                _photoId = value;
                OnPropertyChanged();
            }
        }

        public int? TripId
        {
            get { return _tripId; }
            set 
            {
                if (_tripId == value) return;
                _tripId = value;
                OnPropertyChanged();
            }
        }

        

        public string Name
        {
            get { return _name; }
            set 
            {
                var val = value ?? String.Empty;
                if (_name == val) return;
                _name = val;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set 
            {
                var val = value ?? String.Empty;
                if (_description == val) return;
                _description = val;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get { return _location; }
            set 
            {
                var val = value ?? String.Empty;
                if (_location == val) return;
                _location = val;
                OnPropertyChanged();
            }
        }
        
        public string PhotoFormat
        {
            get { return _photoFormat; }
            set 
            {
                var val = value ?? String.Empty;
                if (_photoFormat == val) return;
                _photoFormat = val;
                OnPropertyChanged();
            }
        }
        
        public string ExportFileName
        {
            get { return _exportFileName; }
            set 
            {
                var val = value ?? String.Empty;
                if (_exportFileName == val) return;
                _exportFileName = val;
                OnPropertyChanged();
            }
        }
        

        public string InternalFileName
        {
            get { return _internalFileName; }
            set 
            {
                var val = value ?? String.Empty;
                if (_internalFileName == val) return;
                _internalFileName = val;
                OnPropertyChanged();
            }
        }
        
        public byte[] Thumbnail
        {
            get { return _thumbnail; }
            set 
            { 
                if(_thumbnail == value) return;
                _thumbnail = value;
                OnPropertyChanged();
            }
        }
       
        public double? Latitude
        {
            get { return _latitude; }
            set 
            {
                if (_latitude == value) return;
                _latitude = value;
                OnPropertyChanged();
            }
        }
        
        public double? Longitude
        {
            get { return _longitude; }
            set 
            {
                if (_longitude == value) return;
                _longitude = value;
                OnPropertyChanged();
            }
        }
        
        public DateTime? PhotoDate
        {
            get { return _photoDate; }
            set 
            {
                if (_photoDate == value) return;
                _photoDate = value;
                OnPropertyChanged();
            }
        }

        public byte[] PhotoBytes
        {
            get { return _photoBytes; }
            set
            {
                if (_photoBytes == value) return;
                _photoBytes = value;
                //Thumbnail = ImageLib.ResizeImageBytes(_photoBytes, 256, 256);
                OnPropertyChanged();
                OnPropertyChanged("Thumbnail");
            }
        }

        public int Index
        {
            get { return _index; }
            set
            {
                if (_index == value) return;
                _index = value;
            }
        }

        public string TripName
        {
            get { return _tripName; }
            set
            {
                var val = value ?? String.Empty;
                if (_tripName == val) return;
                _tripName = val;

            }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
