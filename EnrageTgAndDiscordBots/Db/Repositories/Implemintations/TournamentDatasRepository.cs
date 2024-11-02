using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrageTgBotILovePchel.Db.Models;
using EnrageTgBotILovePchel.Db.Repositories.Interfaces;
using EnrageTgBotILovePchel.Db.DbConnector;
using Microsoft.EntityFrameworkCore;

namespace EnrageTgBotILovePchel.Db.Repositories.Implemintations
{
    public class TournamentDatasRepository : ITournamentDatasRepository
    {
        private EnrageBotVovodyaDbContext _dbContext;

        public TournamentDatasRepository(EnrageBotVovodyaDbContext db)
        {
            _dbContext = db;
        }

        public TournamentsDatum GetTournamentsData()
        {
            return _dbContext.TournamentsData.FirstOrDefault();
        }

        public void AddTournament(string tournamentRules)
        {
            _dbContext.TournamentsData.Add(new TournamentsDatum()
            {
                TournamentsRules = tournamentRules
            });

            _dbContext.SaveChanges();
        }

        public void UpdateTournamentRules(int id, string rules)
        {
            TournamentsDatum datum = _dbContext.TournamentsData.Where(x => x.TournId == id).FirstOrDefault();
            datum.TournamentsRules = rules;

            _dbContext.Update(datum);
            _dbContext.SaveChanges();
        }

        public void DeleteTournament(int id)
        {
            TournamentsDatum datum = _dbContext.TournamentsData.Where(x => x.TournId == id).FirstOrDefault();

            _dbContext.Remove(datum);
            _dbContext.SaveChanges();
        }

        public void AddTournamentsRules(string tournamentRules)
        {
            throw new NotImplementedException();
        }
    }
}
