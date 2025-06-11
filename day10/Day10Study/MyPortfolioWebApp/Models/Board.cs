﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPortfolioWebApp.Models;

public class Board
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Email { get; set; }

    public string? Writer { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Contents { get; set; }

    public DateTime? PostDate { get; set; }

    public int? ReadCount { get; set; }
}
