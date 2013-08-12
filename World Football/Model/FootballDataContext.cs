using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World_Football.Model
{
    public class FootballDataContext : DataContext
    {
        public FootballDataContext(string connectionString) : base(connectionString) { }
        public Table<Team> Teams;
        public Table<League> Leagues;
        public Table<Match> Matches;
    }
}
