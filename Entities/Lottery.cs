using System;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Entities
{
    public class Lottery
    {
        public string Id { get; set; }
        public string MemberId { get; set; }
        public int Number { get; set; }

        public virtual Member Member { get; set; }

    }
}