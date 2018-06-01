using System;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Models.ApiModels
{
    public class MemberLottery : EntityBase
    {
        public string Id { get; set; }
        public string MemberId { get; set; }
        public int Number { get; set; }

        public virtual Member Member { get; set; }

    }
}