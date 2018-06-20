using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Models.ApiModels
{
    public class ChooseNumberApiModel
    {
        public string TeamId { get; set; }
        public int Number { get; set; }
    }
}