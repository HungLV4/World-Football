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
    public class Team : INotifyPropertyChanged, INotifyPropertyChanging
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

        private string _stadium;
        [Column]
        public string Stadium
        {
            get { return _stadium; }
            set
            {
                if (_stadium != value)
                {
                    NotifyPropertyChanging("Stadium");
                    _stadium = value;
                    NotifyPropertyChanged("Stadium");
                }
            }
        }

        [Column]
        internal int _leagueId;

        private EntityRef<League> _league;
        [Association(Storage = "_league", OtherKey = "Id", ThisKey = "_leagueId", IsForeignKey = true)]
        public League League
        {
            get { return _league.Entity; }
            set
            {
                NotifyPropertyChanging("League");
                _league.Entity = value;
                if (value != null)
                {
                    _leagueId = value.Id;
                }
                NotifyPropertyChanged("League");
            }
        }

        /// <summary>
        /// Home matches
        /// </summary>
        private EntitySet<Match> _matchesH;
        [Association(Storage = "_matchesH", ThisKey = "Id", OtherKey = "_teamHId")]
        public EntitySet<Match> MatchesH
        {
            get
            {
                return this._matchesH;
            }
            set
            {
                this._matchesH.Assign(value);
            }
        }

        /// <summary>
        /// Away matches
        /// </summary>
        private EntitySet<Match> _matchesA;
        [Association(Storage = "_matchesA", ThisKey = "Id", OtherKey = "_teamAId")]
        public EntitySet<Match> MatchesA
        {
            get
            {
                return this._matchesA;
            }
            set
            {
                this._matchesA.Assign(value);
            }
        }

        public Team()
        {
            _matchesA = new EntitySet<Match>(
                new Action<Match>(attach_matchA),
                new Action<Match>(detach_matchA)
                );
            _matchesH = new EntitySet<Match>(
                new Action<Match>(attach_matchH),
                new Action<Match>(detach_matchH)
                );
        }

        private void attach_matchH(Match obj)
        {
            NotifyPropertyChanging("MatchesH");
            obj.TeamH = this;
        }

        private void detach_matchH(Match obj)
        {
            NotifyPropertyChanging("MatchesH");
            obj.TeamH = null;
        }

        private void attach_matchA(Match obj)
        {
            NotifyPropertyChanging("MatchesA");
            obj.TeamA = this;
        }

        private void detach_matchA(Match obj)
        {
            NotifyPropertyChanging("MatchesA");
            obj.TeamA = null;
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
