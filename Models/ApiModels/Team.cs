using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Models.ApiModels
{
    public class Team : EntityBase
    {
        public Team()
        {
            Member = new HashSet<Member>();
            TeamLottery = new HashSet<TeamLottery>();
        }
        public string Id { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public virtual ICollection<Member> Member { get; set; }
        public virtual ICollection<TeamLottery> TeamLottery { get; set; }
    }
}