using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World_Football.Model
{
    [Table]
    public class Match : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private int _id;
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    NotifyPropertyChanging("Id");
                    _id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        private DateTime _date;
        [Column]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date != value)
                {
                    NotifyPropertyChanging("Date");
                    _date = value;
                    NotifyPropertyChanged("Date");
                }
            }
        }

        /// <summary>
        /// Away team
        /// </summary>
        [Column]
        internal int _teamAId;
        private EntityRef<Team> _teamA;
        [Association(Storage = "_teamA", OtherKey = "Id", ThisKey = "_teamAId", IsForeignKey = true)]
        public Team TeamA
        {
            get { return _teamA.Entity; }
            set
            {
                NotifyPropertyChanging("TeamA");
                _teamA.Entity = value;
                if (value != null)
                {
                    _teamAId = value.Id;
                }
                NotifyPropertyChanged("TeamA");
            }
        }

        /// <summary>
        /// Home team
        /// </summary>
        [Column]
        internal int _teamHId;
        private EntityRef<Team> _teamH;
        [Association(Storage = "_teamH", OtherKey = "Id", ThisKey = "_teamHId", IsForeignKey = true)]
        public Team TeamH
        {
            get { return _teamH.Entity; }
            set
            {
                NotifyPropertyChanging("TeamH");
                _teamH.Entity = value;
                if (value != null)
                {
                    _teamHId = value.Id;
                }
                NotifyPropertyChanged("TeamH");
            }
        }

        #region notify event
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }
        #endregion
    }
}
