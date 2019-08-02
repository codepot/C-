using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ucla.Common.BaseClasses;

namespace TripLog.Domain
{
    public class Trip : AggregateRootBase, INotifyPropertyChanged
    {
        #region Constructor

        public Trip()
        {
            _waypoints = new ObservableCollection<Waypoint>();
            _photos = new ObservableCollection<Photo>();
            StartDate = System.DateTime.Today;
            EndDate = System.DateTime.Today;
        }

        #endregion

        #region Fields

        private int _tripId;
        private string _name = String.Empty;
        private string _description = String.Empty;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private ObservableCollection<Waypoint> _waypoints;
        private ObservableCollection<Photo> _photos;

        //for WPF
        private int _index;
        #endregion

        #region Properties

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

        public ObservableCollection<Waypoint> Waypoints
        {
            get { return _waypoints;  }
            set { _waypoints = value;  }
        }

        public ObservableCollection<Photo> Photos
        {
            get { return _photos; }
            set { _photos = value; }
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return TripId + ": " + Name + " - " + Description + "("+StartDate + " to "+ EndDate + ")";
        }

        public string Dates
        {
            get
            {
                return ((StartDate != null) ? StartDate.Value.ToString("MM/dd/yyyy") : "n/a date") + " - "
                    + ((EndDate != null) ? EndDate.Value.ToString("MM/dd/yyyy") : "n/a date");
            }
            set {; }
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

       

        protected override bool GetGraphDirty()
        {
            return base.GetGraphDirty()
                || Waypoints.Any(o => o.IsDirty)
                || Waypoints.Any(o => o.IsMarkedForDeletion)
                || Photos.Any(o => o.IsDirty)
                || Photos.Any(o => o.IsMarkedForDeletion);
        }

        public override string Validate(string propertyName = null)
        {
            List<string> errors = new List<string>();
            string err;
            switch (propertyName)
            {
                case "Name":
                    if (String.IsNullOrEmpty(Name))
                        errors.Add("Name is required.");
                    break;
                case "StartDate":
                    if(StartDate < new DateTime(2000,1,1)
                        || StartDate > new DateTime(2099,12,31))
                    {
                        errors.Add("Date must be between 1/1/2000 and 12/31/2099");
                    }
                    break;
                 case "EndDate":
                    if(EndDate < new DateTime(2000,1,1)
                        || EndDate > new DateTime(2099,12,31))
                    {
                        errors.Add("Date must be between 1/1/2000 and 12/31/2099");
                    }
                    break;
                case null:
                    err = Validate("Name");
                    if (err != null) errors.Add(err);
                    err = Validate("StartDate");
                    if (err != null) errors.Add(err);
                    err = Validate("EndDate");
                    if (err != null) errors.Add(err);
                    if(StartDate > EndDate)
                    {
                        errors.Add("End Date must be after Start Date.");
                    }
                    break;
                default:
                    return null;
            }
            return errors.Count == 0 ? null : String.Join("\r\n", errors);
        }

        

        #endregion
    }
}
