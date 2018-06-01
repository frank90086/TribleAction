using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Models.ApiModels
{
    public class Coupon : EntityBase
    {
        public Coupon()
        {
            TeamCoupon = new HashSet<TeamCoupon>();
        }

        public string Id { get; set; }
        public int Score { get; set; }
        public int Count { get; set; }

        public virtual ICollection<TeamCoupon> TeamCoupon { get; set; }
    }
}