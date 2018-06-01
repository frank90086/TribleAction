using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Models.ApiModels
{
    public class EntityBase
    {
        public EntityBase()
        {
            CreatorId = "System";
            CreatedDate = DateTimeOffset.UtcNow;
            ModifierId = "System";
            ModifiedDate = DateTimeOffset.UtcNow;
        }
        public string CreatorId { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string ModifierId { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}