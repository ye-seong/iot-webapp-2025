﻿using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        public string Langauge {  get; set; }

        public float Level { get; set; }
    }
}
