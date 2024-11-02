using System;
using System.Collections.Generic;

namespace EnrageTgBotILovePchel.Db.Models;

public partial class TournamentsDatum
{
    public string TournamentsRules { get; set; } = null!;

    public int TournId { get; set; }
}
