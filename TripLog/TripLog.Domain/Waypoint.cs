using System;
using Ucla.Common.BaseClasses;

namespace TripLog.Domain
{
    public class Waypoint : DomainBase
    {
        #region Fields

        private int _waypointId;
        private string _name = String.Empty;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private string _description = String.Empty;
        private string _street = String.Empty;
        private string _city = String.Empty;
        private string _state = String.Empty;
        private string _country = String.Empty;
        private double? _latitude;
        private double? _longitude;
        private int _tripId;


        // for GUI
        private int _index;
        #endregion

        #region Properties

        public int WaypointId
        {
            get { return _waypointId; }
            set
            {
                if (_waypointId == value) return;
                _waypointId = value;
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
                OnPropertyChanged("Name");
            }
        }

        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate == value) return;
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate == value) return;
                _endDate = value;
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

        public string Street
        {
            get { return _street; }
            set
            {
                var val = value ?? String.Empty;
                if (_street == val) return;
                _street = val;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                var val = value ?? String.Empty;
                if (_city == val) return;
                _city = val;
                OnPropertyChanged();
            }
        }

        public string State
        {
            get { return _state; }
            set
            {
                var val = value ?? String.Empty;
                if (_state == val) return;
                _state = val;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                var val = value ?? String.Empty;
                if (_country == val) return;
                _country = val;
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

        public string Period
        {
            get
            {
                return "[" + ((StartDate != null) ? StartDate.Value.ToString("MM/dd/yy") : "n/a date" )+ "-"
                    + ((EndDate != null) ? EndDate.Value.ToString("MM/dd/yy") : "n/a date" )+ "]";
            }
            set {; }
        }

        public string Location
        {
            get
            {
                return State+", " +Country;
            }
            set {; }
        }

        public int TripId
        {
            get { return _tripId; }
            set
            {
                if (_tripId == value) return;
                _tripId = value;
                OnPropertyChanged();
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
        #endregion

        #region Overrides

        public override string ToString()
        {
            return Name + ", " + Description;
        }

        #endregion

        public Waypoint ShallowCopy()
        {
            return (Waypoint)this.MemberwiseClone();
        }
    }
}
