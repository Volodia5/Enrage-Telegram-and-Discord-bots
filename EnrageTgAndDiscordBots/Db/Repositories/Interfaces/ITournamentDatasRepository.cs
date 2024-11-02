using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrageTgBotILovePchel.Db.Models;

namespace EnrageTgBotILovePchel.Db.Repositories.Interfaces
{
    public interface ITournamentDatasRepository
    {
        TournamentsDatum GetTournamentsData();
        void AddTournament(string tournamentRules);
        void AddTournamentsRules(string tournamentRules); //только добавление, потому что внутри будут сслыки на вк, а правила меняются уже внутри.
        void UpdateTournamentRules(int id, string rules);
        void DeleteTournament(int id);
    }
}
