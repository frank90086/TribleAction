using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Models.ApiModels
{
    public class Member : EntityBase
    {
        public Member()
        {
            Lottery = new HashSet<MemberLottery>();
        }
        public string Id { get; set; }
        public string TeamId { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<MemberLottery> Lottery { get; set; }

    }
}