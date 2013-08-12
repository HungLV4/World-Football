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
    public class League : INotifyPropertyChanged, INotifyPropertyChanging
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

        private string _name;
        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private EntitySet<Team> _teams;
        [Association(Storage = "_teams", ThisKey = "Id", OtherKey = "_leagueId")]
        public EntitySet<Team> Teams
        {
            get { return this._teams; }
            set
            {
                this._teams.Assign(value);
            }
        }

        public League()
        {
            _teams = new EntitySet<Team>(
                new Action<Team>(attach_team),
                new Action<Team>(detach_team)
                );
        }

        private void attach_team(Team obj)
        {
            NotifyPropertyChanging("Teams");
            obj.League = this;
        }

        private void detach_team(Team obj)
        {
            NotifyPropertyChanging("Teams");
            obj.League = null;
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
