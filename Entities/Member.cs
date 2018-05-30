using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Entities
{
    public class Member
    {
        public Member()
        {
            Lottery = new HashSet<Lottery>();
        }
        public string Id { get; set; }
        public string TeamId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<Lottery> Lottery { get; set; }

    }
}