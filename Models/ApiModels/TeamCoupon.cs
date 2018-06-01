using System;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Models.ApiModels
{
    public class TeamCoupon : EntityBase
    {
        public string Id { get; set; }
        public string TeamId { get; set; }
        public string CouponId { get; set; }

        public virtual Team Team { get; set; }
        public virtual Coupon Coupon { get; set; }
    }
}