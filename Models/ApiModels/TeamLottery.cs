using System;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Models.ApiModels
{
    public class TeamLottery : EntityBase
    {
        public string Id { get; set; }
        public string TeamId { get; set; }
        public int Number { get; set; }

        public virtual Team Team { get; set; }
    }
}