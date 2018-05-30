using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Entities
{
    public class Team
    {
        public Team()
        {
            Member = new HashSet<Member>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public virtual ICollection<Member> Member { get; set; }
    }
}